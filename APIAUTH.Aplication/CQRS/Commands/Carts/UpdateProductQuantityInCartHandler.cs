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
    public class UpdateProductQuantityInCartHandler : IRequestHandler<UpdateProductQuantityInCartCommand, Unit>
    {
        private readonly IRepository<Orden> _ordenRepo;

        public UpdateProductQuantityInCartHandler(IRepository<Orden> ordenRepo)
        {
            _ordenRepo = ordenRepo;
        }

        public async Task<Unit> Handle(UpdateProductQuantityInCartCommand request, CancellationToken cancellationToken)
        {
            var orden = await _ordenRepo
                             .GetFiltered(p => p.UserId == request.UserId && p.OrdenState == OrdenState.PendienteCompra)
                             .FirstOrDefaultAsync(cancellationToken);
            if (orden == null || orden.OrdenState != OrdenState.PendienteCompra)
                throw new Exception("Orden no válida");

            var productLine = orden.ProductLine.FirstOrDefault(p => p.ProductId == request.ProductId);
            if (productLine == null)
                throw new Exception("Producto no está en el carrito");

            productLine.Amount = request.NewQuantity;

            await _ordenRepo.Update(orden);
            return Unit.Value;
        }
    }

}
