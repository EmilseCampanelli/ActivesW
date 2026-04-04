using APIAUTH.Aplication.CQRS.Commands.Producto.ProductCart.Create;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using AutoMapper;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Implementacion
{
    public class ProductCartService : IProductCartService
    {

        private readonly IRepository<ProductLine> _productLineRepository;
        private readonly IRepository<Orden> _ordenRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly int ordenIdPending = 1;

        public ProductCartService(IRepository<ProductLine> productLineRepository, IRepository<Orden> ordenRepository, IMapper mapper, IRepository<Product> productRepository)
        {
            _mapper = mapper;
            _productLineRepository = productLineRepository;
            _ordenRepository = ordenRepository;
            _productRepository = productRepository;
        }


        public async Task<int> AddProductCart(CreateProductCartCommand command, CancellationToken ct = default)
        {

            var newProductLine = new ProductLine();
            var product = await _productRepository.Get(command.ProductId);
            var ordenCurrent = _ordenRepository.GetFiltered(u => u.UserId == command.UserId && u.OrdenState == Domain.Enums.OrdenState.PendienteCompra).FirstOrDefault();

            if (ordenCurrent == null)
            {
                var orden = new Orden();
                orden.UserId = command.UserId;
                orden.OrdenState = Domain.Enums.OrdenState.PendienteCompra;
                orden.OrdenDate = DateTime.UtcNow;
                orden.CreatedDate = DateTime.UtcNow;
                orden.Status = Domain.Enums.BaseState.Activo;

                ordenCurrent = await _ordenRepository.Add(orden);
            }

            var productLine = ordenCurrent.ProductLine.FirstOrDefault(p => p.ProductId == command.ProductId && p.Size == command.Size);

            if (productLine == null) {
                newProductLine.ProductId = command.ProductId;
                newProductLine.CreatedDate = DateTime.UtcNow;
                newProductLine.Status = Domain.Enums.BaseState.Activo;
                newProductLine.Price = product.Price;
                newProductLine.OrdenId = ordenCurrent.Id;
                newProductLine.Amount = command.Quantity;
                newProductLine.PriceFinal = product.Price * command.Quantity;
                newProductLine.Size = command.Size;

                await _productLineRepository.Add(newProductLine);
            }
            else
            {
                newProductLine = productLine;
                newProductLine.Amount += command.Quantity;
                newProductLine.PriceFinal = product.Price * newProductLine.Amount;

                await _productLineRepository.Update(newProductLine);
            }
            return newProductLine.Id;

        }
    }
}
