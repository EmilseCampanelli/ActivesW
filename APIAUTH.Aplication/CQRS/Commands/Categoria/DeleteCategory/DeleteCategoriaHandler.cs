using APIAUTH.Aplication.CQRS.Commands.Categoria.CreateCategoria;
using APIAUTH.Aplication.Services.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Categoria.DeleteCategory
{
    public class DeleteCategoriaHandler : IRequestHandler<DeleteCategoriaCommand, string>
    {
        private readonly ICategoryService _categoriaService;

        public DeleteCategoriaHandler(ICategoryService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public async Task<string> Handle(DeleteCategoriaCommand request, CancellationToken cancellationToken)
        {
            return await _categoriaService.DeleteCategory(request.CategoryId);
        }
    }
}
