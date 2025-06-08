using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using APIAUTH.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Carts
{
    public class RemoveProductFromCartHandler : IRequestHandler<RemoveProductFromCartCommand, Unit>
    {
        private readonly IRepository<Orden> _ordenRepo;

        public RemoveProductFromCartHandler(IRepository<Orden> ordenRepo)
        {
            _ordenRepo = ordenRepo;
        }

        public async Task<Unit> Handle(RemoveProductFromCartCommand request, CancellationToken cancellationToken)
        {
            var orden = await _ordenRepo
                            .GetFiltered(p => p.UserId == request.UserId && p.OrdenState == OrdenState.PendienteCompra)
                            .FirstOrDefaultAsync(cancellationToken);

            if (orden == null)
                throw new Exception("Orden no válida");

            orden.ProductLine.RemoveAll(p => p.ProductId == request.ProductId);

            await _ordenRepo.Update(orden);
            return Unit.Value;
        }
    }

}
