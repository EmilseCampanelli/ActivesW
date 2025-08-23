using APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser;
using APIAUTH.Aplication.CQRS.Commands.Usuario.UpdateUser;
using APIAUTH.Aplication.CQRS.Queries.Users;
using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Shared.Parameters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
            try
            {
                var token = await _mediator.Send(command);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserCommand command)
        {
            try
            {
                if (id != command.Id)
                    return BadRequest("El ID de la ruta no coincide con el del cuerpo");

                var success = await _mediator.Send(command);
                if (!success) return Ok();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "UserAndAdmin")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            try
            {
                var usuario = await _usuarioService.Get(id);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Policy = "UserAndAdmin")] //TODO: Agregar los roles y politicas requeridas
        [HttpPost("PutImages")]
        [Authorize]
        public async Task<IActionResult> PutImages([FromForm] IFormFile image)
        {
            try
            {
                return Ok(await _usuarioService.PutImage(image));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Blocked")]
        [Authorize(Policy = "UserAndAdmin")]
        public async Task<IActionResult> Blocked(int id)
        {
            try
            {
                await _usuarioService.Blocked(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        [Authorize(Policy = "UserAndAdmin")]
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
