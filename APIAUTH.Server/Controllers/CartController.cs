using APIAUTH.Aplication.CQRS.Commands.Carts;
using APIAUTH.Aplication.CQRS.Commands.Producto.ProductCart.Create;
using APIAUTH.Aplication.CQRS.Queries.Carts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APIAUTH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateProductCartCommand request)
        {
            try
            {
                var id = await _mediator.Send(request);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCart")]
        public async Task<IActionResult> GetPendientes()
        {
            try
            {
                var result = await _mediator.Send(new GetCartsQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveProduct([FromBody] RemoveProductFromCartCommand command)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("idUser")?.Value ?? "0");

                command.UserId = userId;
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateQuantity")]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateProductQuantityInCartCommand command)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("idUser")?.Value ?? "0");

                command.UserId = userId;
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
