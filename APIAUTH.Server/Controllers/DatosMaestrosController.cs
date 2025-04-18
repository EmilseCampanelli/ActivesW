using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIAUTH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosMaestrosController : ControllerBase
    {
        private readonly IDatosMaestrosService _datosMaestrosService;
        
        public DatosMaestrosController(IDatosMaestrosService datosMaestrosService)
        {
            _datosMaestrosService = datosMaestrosService;
        }

        [HttpGet("sexo")]
        public ActionResult<List<ComboDto>> GetSexo()
        {
            return _datosMaestrosService.GetSexo();
        }

        [HttpGet("estado-carrito")]
        public ActionResult<List<ComboDto>> GetEstadoCarrito()
        {
            return _datosMaestrosService.GetEstadoCarrito();
        }

        [HttpGet("estado-orden")]
        public ActionResult<List<ComboDto>> GetEstadoOrden()
        {
            return _datosMaestrosService.GetEstadoOrden();
        }

        [HttpGet("estado-producto")]
        public ActionResult<List<ComboDto>> GetEstadoProducto()
        {
            return _datosMaestrosService.GetEstadoProducto();
        }

        [HttpGet("tipo-documento")]
        public ActionResult<List<ComboDto>> GetTipoDocumento()
        {
            return _datosMaestrosService.GetTipoDocumento();
        }

        [HttpGet("tipo-usuario")]
        public async Task<ActionResult<List<ComboDto>>> GetTipoUsuario()
        {
            return await _datosMaestrosService.GetTipoUsuario();
        }

        [HttpGet("roles")]
        public async Task<ActionResult<List<ComboDto>>> GetRoles()
        {
            return await _datosMaestrosService.GetRoles();
        }

        [HttpGet("pais")]
        public async Task<ActionResult<List<ComboDto>>> GetPais()
        {
            return await _datosMaestrosService.GetPais();
        }

        [HttpGet("categorias")]
        public async Task<ActionResult<List<ComboDto>>> GetCategorias()
        {
            return await _datosMaestrosService.GetCategorias();
        }

        [HttpGet("provincias")]
        public async Task<ActionResult<List<ComboDto>>> GetProvincias()
        {
            return await _datosMaestrosService.GetProvincias();
        }


    }
}
