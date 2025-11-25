using APIAUTH.Aplication.Services.Implementacion;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using APIAUTH.Domain.Repository;
using MediatR;
using MercadoPago.Resource.Order;
using Microsoft.EntityFrameworkCore;

namespace APIAUTH.Aplication.CQRS.Commands.Orders
{
    public class ConfirmarOrdenHandler : IRequestHandler<ConfirmOrdenCommand, string>
    {
        private readonly IRepository<Orden> _ordenRepo;
        private INotificationService _notificationService;

        private readonly IPromotionEngine _promotionEngine;
        private readonly IPaymentService _paymentService;

        public ConfirmarOrdenHandler(IRepository<Orden> ordenRepo, INotificationService notificationService, IPaymentService paymentService, IPromotionEngine promotionEngine)
        {
            _ordenRepo = ordenRepo;
            _notificationService = notificationService;
            _paymentService = paymentService;
            _promotionEngine = promotionEngine;
        }

        public async Task<string> Handle(ConfirmOrdenCommand request, CancellationToken cancellationToken)
        {
            var orden = await _ordenRepo
        .GetFiltered(o => o.UserId == request.UserId && o.OrdenState == OrdenState.PendienteCompra)
        .Include(o => o.ProductLine)
            .ThenInclude(pl => pl.Product)
                .ThenInclude(p => p.ProductImages)
        .FirstOrDefaultAsync(cancellationToken);

            if (orden == null)
                throw new Exception("No ha seleccionado productos para iniciar la compra.");

            double totalFinal = 0;

            foreach (var line in orden.ProductLine)
            {
                var product = line.Product;

                // promos aplicables
                var promos = _promotionEngine.GetPromotionsForProduct(product);

                // precio final por unidad considerando cantidad
                decimal finalUnitPrice = _promotionEngine.CalculateFinalPrice(product, line.Amount).FinalPrice;

                line.PriceFinal = (double)finalUnitPrice;
                line.SubTotal = Math.Round(line.Amount * line.PriceFinal, 2);
                line.UpdatedDate = DateTime.UtcNow;

                totalFinal += line.SubTotal;
            }

            orden.Total = Math.Round(totalFinal, 2);
            orden.UpdatedDate = DateTime.UtcNow;

            await _ordenRepo.Update(orden);

            await _notificationService.NotificationByConfirmOrder(orden.Id);

            return await _paymentService.CreatePaymentPreferenceAsync(orden, request.CostTracking);
        }
    }

}
