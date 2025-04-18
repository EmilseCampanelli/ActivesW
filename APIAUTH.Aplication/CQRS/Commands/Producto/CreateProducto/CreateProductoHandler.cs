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
    public class CreateProductoHandler : IRequestHandler<CreateProductoCommand, int>
    {
        private readonly IProductoService _productoService;
        private readonly IMapper _mapper;

        public CreateProductoHandler(IProductoService productoService, IMapper mapper)
        {
            _productoService = productoService;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateProductoCommand request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<ProductoDto>(request);
            var result = await _productoService.Save(dto);
            return result.Id;
        }
    }
}
