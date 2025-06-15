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
            try
            {
                return _masterDataService.GetSexo();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("OrdenState")]
        public ActionResult<List<ComboDto>> GetEstadoOrden()
        {
            try
            {
                return _masterDataService.GetEstadoOrden();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ProductState")]
        public ActionResult<List<ComboDto>> GetEstadoProducto()
        {
            try
            {
                return _masterDataService.GetEstadoProducto();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("TypeDocument")]
        public ActionResult<List<ComboDto>> GetTipoDocumento()
        {
            try
            {
                return _masterDataService.GetTipoDocumento();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Roles")]
        public async Task<ActionResult<List<ComboDto>>> GetRoles()
        {
            try
            {
                return await _masterDataService.GetRoles();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Countries")]
        public async Task<ActionResult<List<ComboDto>>> GetPais()
        {
            try
            {
                return await _masterDataService.GetPais();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Categories")]
        public async Task<ActionResult<List<ComboDto>>> GetCategorias()
        {
            try
            {
                return await _masterDataService.GetCategorias();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Provinces")]
        public async Task<ActionResult<List<ComboDto>>> GetProvincias()
        {
            try
            {
                return await _masterDataService.GetProvincias();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
