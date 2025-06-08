using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Implementacion;
using APIAUTH.Aplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIAUTH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IColorThemeService _colorThemeService;
        private readonly IMenuService _menuService;

        public ConfigurationController(IColorThemeService colorThemeService, IMenuService menuService)
        {
            _colorThemeService = colorThemeService;
            _menuService = menuService;
        }

        /// <summary>
        /// Devuelve la configuración de colores para los modos light y dark.
        /// </summary>
        [HttpGet("colors")]
        public async Task<ActionResult<ColorThemeResponse>> GetColors()
        {
            var result = await _colorThemeService.GetColorsAsync();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMenus()
        {
            var response = await _menuService.GetMenusAsync();
            return Ok(response);
        }
    }
}

