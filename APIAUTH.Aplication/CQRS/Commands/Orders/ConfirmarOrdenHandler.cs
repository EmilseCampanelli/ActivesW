using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using APIAUTH.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APIAUTH.Aplication.CQRS.Commands.Orders
{
    public class ConfirmarOrdenHandler : IRequestHandler<ConfirmOrdenCommand, bool>
    {
        private readonly IRepository<Orden> _ordenRepo;

        public ConfirmarOrdenHandler(IRepository<Orden> ordenRepo)
        {
            _ordenRepo = ordenRepo;
        }

        public async Task<bool> Handle(ConfirmOrdenCommand request, CancellationToken cancellationToken)
        {
            var orden = await _ordenRepo
                .GetFiltered(o => o.UserId == request.UserId && o.OrdenState == OrdenState.PendienteCompra)
                .FirstOrDefaultAsync(cancellationToken);

            if (orden == null)
                throw new Exception("No ha seleccionado productos para iniciar la compra.");

            orden.OrdenState = OrdenState.Pendiente;
            orden.OrdenDate = DateTime.UtcNow;

            await _ordenRepo.Update(orden);

            return true;
        }
    }

}
