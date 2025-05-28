using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Categoria.CreateCategoria
{
    public class CreateCategoriaHandler : IRequestHandler<CreateCategoriaCommand, int>
    {
        private readonly ICategoryService _categoriaService;
        private readonly IMapper _mapper;

        public CreateCategoriaHandler(ICategoryService categoriaService, IMapper mapper)
        {
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCategoriaCommand request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<CategoryDto>(request);
            var result = await _categoriaService.Save(dto);
            return result.Id;
        }
    }
}
