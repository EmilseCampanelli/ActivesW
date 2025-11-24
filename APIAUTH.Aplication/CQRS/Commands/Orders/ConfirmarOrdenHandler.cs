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

        private readonly IPaymentService _paymentService;

        public ConfirmarOrdenHandler(IRepository<Orden> ordenRepo, INotificationService notificationService, IPaymentService paymentService)
        {
            _ordenRepo = ordenRepo;
            _notificationService = notificationService;
            _paymentService = paymentService;
        }

        public async Task<string> Handle(ConfirmOrdenCommand request, CancellationToken cancellationToken)
        {
            var orden = await _ordenRepo
                .GetFiltered(o => o.UserId == request.UserId && o.OrdenState == OrdenState.PendienteCompra)
                .FirstOrDefaultAsync(cancellationToken);

            if (orden == null)
                throw new Exception("No ha seleccionado productos para iniciar la compra.");


            await _notificationService.NotificationByConfirmOrder(orden.Id);

            return await _paymentService.CreatePaymentPreferenceAsync(orden, request.CostTracking);
        }
    }

}
