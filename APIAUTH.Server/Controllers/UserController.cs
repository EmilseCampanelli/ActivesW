using APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser;
using APIAUTH.Aplication.CQRS.Commands.Usuario.UpdateUser;
using APIAUTH.Aplication.CQRS.Queries.Users;
using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Shared.Parameters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APIAUTH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _usuarioService;
        private readonly IMediator _mediator;

        public UserController(IUserService usuarioService, IMediator mediator)
        {
            _usuarioService = usuarioService;
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id)
                return BadRequest("El ID de la ruta no coincide con el del cuerpo");

            var success = await _mediator.Send(command);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            var usuario = await _usuarioService.Get(id);
            return Ok(usuario);
        }

        //[Authorize(Policy = "UserAndAdmin")] //TODO: Agregar los roles y politicas requeridas
        [HttpPost("PutImages")]
        public async Task<IActionResult> PutImages([FromForm] IFormFile image)
        {
            return Ok(await _usuarioService.PutImage(image));
        }

        [HttpPost("Blocked")]
        public async Task<IActionResult> Blocked(int id)
        {
            await _usuarioService.Blocked(id);
            return Ok();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] UserQueryParameters parameters)
        {
            try
            {
                var result = await _mediator.Send(new GetUsersQuery(parameters));
                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }
    }
}
