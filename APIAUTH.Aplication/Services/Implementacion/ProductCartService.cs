using APIAUTH.Aplication.CQRS.Commands.Producto.ProductCart.Create;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using AutoMapper;
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


        public async void AddProductCart(CreateProductCartCommand command)
        {
            var product = await _productRepository.Get(command.ProductId);
            var ordenCurrent = _ordenRepository.GetFiltered(u => u.UserId == command.UserId && u.OrdenState == Domain.Enums.OrdenState.PendienteCompra).FirstOrDefault();

            var newProductLine = new ProductLine();
            newProductLine.ProductId = command.ProductId;
            newProductLine.Amount = command.Quantity;
            newProductLine.Price = product.Price;


            if (ordenCurrent == null)
            {
                var orden = new Orden();
                orden.UserId = command.UserId;
                orden.OrdenState = Domain.Enums.OrdenState.PendienteCompra;
                orden.OrdenDate = DateTime.Now;

                ordenCurrent = await _ordenRepository.Add(orden);
            }

            newProductLine.OrdenId = ordenCurrent.Id;

            await _productLineRepository.Add(newProductLine);

        }
    }
}
