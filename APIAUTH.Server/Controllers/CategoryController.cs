using APIAUTH.Aplication.CQRS.Commands.Categoria.CreateCategoria;
using APIAUTH.Aplication.CQRS.Commands.Categoria.DeleteCategory;
using APIAUTH.Aplication.CQRS.Commands.Categoria.UpdateCategoria;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIAUTH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateCategory")]
        [Authorize(Policy = "UserAndAdmin")]
        public async Task<IActionResult> Create([FromBody] CreateCategoriaCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "UserAndAdmin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoriaCommand command)
        {
            try
            {
                if (id != command.Id)
                    return BadRequest("El ID de la ruta no coincide con el del cuerpo");

                var idCategoria = await _mediator.Send(command);

                return Ok(idCategoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Policy = "UserAndAdmin")]
        public async Task<IActionResult> Delete([FromBody] DeleteCategoriaCommand command)
        {
            try
            {
                var idCategoria = await _mediator.Send(command);

                return Ok(idCategoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
