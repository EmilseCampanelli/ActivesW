using APIAUTH.Aplication.CQRS.Commands.Categoria.CreateCategoria;
using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Categoria.UpdateCategoria
{
    public record UpdateCategoriaHandler : IRequestHandler<UpdateCategoriaCommand, int>
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;

        public UpdateCategoriaHandler(ICategoriaService categoriaService, IMapper mapper)
        {
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateCategoriaCommand request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<CategoriaDto>(request);
            var result = await _categoriaService.Save(dto);
            return result.Id;
        }
    }
}
