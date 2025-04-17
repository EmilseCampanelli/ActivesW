using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIAUTH.Server.Controllers
{
    public class CategoriaController : GenericController<ICategoriaService, CategoriaDto>
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService) : base(categoriaService)
        {
            _categoriaService = categoriaService;
        }
    }
}
