using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIAUTH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly IMasterDataService _masterDataService;
        
        public MasterDataController(IMasterDataService masterDataService)
        {
            _masterDataService = masterDataService;
        }

        [HttpGet("Gender")]
        public ActionResult<List<ComboDto>> GetSexo()
        {
            return _masterDataService.GetSexo();
        }


        [HttpGet("OrdenState")]
        public ActionResult<List<ComboDto>> GetEstadoOrden()
        {
            return _masterDataService.GetEstadoOrden();
        }

        [HttpGet("ProductState")]
        public ActionResult<List<ComboDto>> GetEstadoProducto()
        {
            return _masterDataService.GetEstadoProducto();
        }

        [HttpGet("DocumentType")]
        public ActionResult<List<ComboDto>> GetTipoDocumento()
        {
            return _masterDataService.GetTipoDocumento();
        }

        [HttpGet("Roles")]
        public async Task<ActionResult<List<ComboDto>>> GetRoles()
        {
            return await _masterDataService.GetRoles();
        }

        [HttpGet("Countries")]
        public async Task<ActionResult<List<ComboDto>>> GetPais()
        {
            return await _masterDataService.GetPais();
        }

        [HttpGet("Categories")]
        public async Task<ActionResult<List<ComboDto>>> GetCategorias()
        {
            return await _masterDataService.GetCategorias();
        }

        [HttpGet("Provinces")]
        public async Task<ActionResult<List<ComboDto>>> GetProvincias()
        {
            return await _masterDataService.GetProvincias();
        }


    }
}
