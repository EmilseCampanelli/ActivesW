using APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser;
using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIAUTH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int usuarioId)
        {
            try
            {
                var domicilios = await _addressService.GetByUsuarioIdAsync(usuarioId);
                return Ok(domicilios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(int usuarioId, [FromBody] AddressAddDto dto)
        {
            try
            {
                var id = await _addressService.AddToUsuarioAsync(usuarioId, dto);
                return CreatedAtAction(nameof(GetAll), new { usuarioId }, null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _addressService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int usuarioId, int id, [FromBody] AddressDto dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest("El ID del domicilio no coincide.");

                await _addressService.UpdateAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
