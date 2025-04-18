using APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser;
using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIAUTH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomicilioController : ControllerBase
    {
        private readonly IDomicilioService _domicilioService;

        public DomicilioController(IDomicilioService domicilioService)
        {
            _domicilioService = domicilioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int usuarioId)
        {
            var domicilios = await _domicilioService.GetByUsuarioIdAsync(usuarioId);
            return Ok(domicilios);
        }

        [HttpPost]
        public async Task<IActionResult> Post(int usuarioId, [FromBody] CreateDomicilioCommand dto)
        {
            var id = await _domicilioService.AddToUsuarioAsync(usuarioId, dto);
            return CreatedAtAction(nameof(GetAll), new { usuarioId }, null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _domicilioService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int usuarioId, int id, [FromBody] DomicilioDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID del domicilio no coincide.");

            await _domicilioService.UpdateAsync(dto);
            return NoContent();
        }
    }
}
