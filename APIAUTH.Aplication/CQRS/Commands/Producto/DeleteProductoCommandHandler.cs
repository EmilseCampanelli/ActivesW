using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using APIAUTH.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Producto
{
    public class DeleteProductoCommandHandler : IRequestHandler<DeleteProductoCommand, bool>
    {
        private readonly IRepository<Product> _repository;

        public DeleteProductoCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteProductoCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.Get(request.Id);

            if (product == null)
                return false;

            product.ProductState = ProductState.Eliminado;

            await _repository.Update(product);
            return true;
        }
    }
}
