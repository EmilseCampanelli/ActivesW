using APIAUTH.Aplication.CQRS.Commands.Categoria.CreateCategoria;
using APIAUTH.Aplication.CQRS.Commands.Categoria.UpdateCategoria;
using APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser;
using APIAUTH.Aplication.CQRS.Commands.Usuario.UpdateUser;
using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APIAUTH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> Create([FromBody] CreateCategoriaCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoriaCommand command)
        {
            if (id != command.Id)
                return BadRequest("El ID de la ruta no coincide con el del cuerpo");

            var idCategoria = await _mediator.Send(command);

            return Ok(idCategoria);
        }
    }
}
