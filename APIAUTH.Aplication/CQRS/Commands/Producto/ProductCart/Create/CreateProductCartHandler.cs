using APIAUTH.Aplication.Services.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Producto.ProductCart.Create
{
    public class CreateProductCartHandler : IRequestHandler<CreateProductCartCommand, int>
    {
        private readonly IProductCartService _productCartService;
        private readonly IMapper _mapper;

        public CreateProductCartHandler(IProductCartService productCartService, IMapper mapper)
        {
            _productCartService = productCartService;
            _mapper = mapper;
        }

        async Task<int> IRequestHandler<CreateProductCartCommand, int>.Handle(CreateProductCartCommand request, CancellationToken cancellationToken)
        {
            return await _productCartService.AddProductCart(request, cancellationToken);
        }
    }
}
