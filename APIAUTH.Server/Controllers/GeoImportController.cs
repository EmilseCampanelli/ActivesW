using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIAUTH.Server.Controllers
{
    [ApiController]
    [Route("api/geo-import")]
    public class GeoImportController : ControllerBase
    {
        private readonly IGeoImportService _svc;
        private readonly IHostEnvironment _env;
        private readonly IConfiguration _cfg;

        public GeoImportController(IGeoImportService svc, IHostEnvironment env, IConfiguration cfg)
        {
            _svc = svc; _env = env; _cfg = cfg;
        }

        [HttpPost]
        public async Task<IActionResult> Run()
        {
            var folder = _cfg["GeoImport:Folder"] ?? "Data";
            var abs = Path.Combine(_env.ContentRootPath, folder);
            await _svc.ImportAsync(abs);
            return Ok(new
            {
                folder = abs,
                countries = await HttpContext.RequestServices.GetRequiredService<ActivesWContext>().Countries.CountAsync(),
                states = await HttpContext.RequestServices.GetRequiredService<ActivesWContext>().Provinces.CountAsync(),
                cities = await HttpContext.RequestServices.GetRequiredService<ActivesWContext>().Cities.CountAsync()
            });
        }
    }
}
