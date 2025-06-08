using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using APIAUTH.Domain.Repository;
using MediatR;

namespace APIAUTH.Aplication.CQRS.Commands.Orders
{
    public class ChangeStateOrdenHandler : IRequestHandler<ChangeStateOrdenCommand, Unit>
    {
        private readonly IRepository<Orden> _ordenRepo;

        public ChangeStateOrdenHandler(IRepository<Orden> ordenRepo)
        {
            _ordenRepo = ordenRepo;
        }

        public async Task<Unit> Handle(ChangeStateOrdenCommand request, CancellationToken cancellationToken)
        {
            var orden = await _ordenRepo.Get(request.OrdenId);

            if (orden == null)
                throw new Exception("Compra no encontrada.");

            orden.OrdenState = (OrdenState)request.newState;

            await _ordenRepo.Update(orden);
            return Unit.Value;
        }
    }

}
