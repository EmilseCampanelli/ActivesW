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
            var id = await _mediator.Send(request);

            return Ok(id);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetPendientes()
        {
            var result = await _mediator.Send(new GetCartsQuery());
            return Ok(result);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveProduct([FromBody] RemoveProductFromCartCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("update-quantity")]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateProductQuantityInCartCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
