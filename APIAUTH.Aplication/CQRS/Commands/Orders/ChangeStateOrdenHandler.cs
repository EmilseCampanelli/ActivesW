using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using APIAUTH.Domain.Repository;
using MediatR;

namespace APIAUTH.Aplication.CQRS.Commands.Orders
{
    public class ChangeStateOrdenHandler : IRequestHandler<ChangeStateOrdenCommand, Unit>
    {
        private readonly IRepository<Orden> _ordenRepo;
        private readonly INotificationService _notificationService;

        public ChangeStateOrdenHandler(IRepository<Orden> ordenRepo, INotificationService notificationService)
        {
            _ordenRepo = ordenRepo;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(ChangeStateOrdenCommand request, CancellationToken cancellationToken)
        {
            var orden = await _ordenRepo.Get(request.OrdenId);

            if (orden == null)
                throw new Exception("Compra no encontrada.");

            orden.OrdenState = (OrdenState)request.newState;

            orden = await _ordenRepo.Update(orden);
            sendNotification(orden);
            return Unit.Value;
        }

        private void sendNotification(Orden order)
        {
            switch (order.OrdenState)
            {
                case OrdenState.Cancelado:
                    _notificationService.NotificationByCancelOrder(order.Id);
                    break;
                case OrdenState.Pagado:
                    _notificationService.NotificationByPayOrder(order.Id);
                    break;
                default:
                    break;
            }
        }
    }

}
