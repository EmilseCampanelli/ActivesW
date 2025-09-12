using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Producto.CreateProducto
{
    public class CreateProductoHandler : IRequestHandler<CreateProductoCommand, string>
    {
        private readonly IProductService _productoService;
        private readonly IMapper _mapper;

        public CreateProductoHandler(IProductService productoService, IMapper mapper)
        {
            _productoService = productoService;
            _mapper = mapper;
        }

        public async Task<string> Handle(CreateProductoCommand request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<ProductDto>(request);
            dto.ProductImages = request.ProductImage;
            var result = await _productoService.Save(dto);
            return result.Slug;
        }
    }
}
