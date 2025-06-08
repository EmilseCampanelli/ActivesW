using APIAUTH.Aplication.CQRS.Commands.Orders;
using APIAUTH.Aplication.CQRS.Commands.Producto.ProductCart.Create;
using APIAUTH.Aplication.CQRS.Queries.Orders;
using APIAUTH.Aplication.CQRS.Queries.Products;
using APIAUTH.Shared.Parameters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APIAUTH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] OrdenQueryParameters parameters)
        {
            try
            {
                var result = await _mediator.Send(new GetOrdersQuery(parameters));
                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }


        [HttpPost("confirm")]
        public async Task<IActionResult> Confirm([FromBody] ConfirmOrdenCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("cambiar-estado")]
        public async Task<IActionResult> CambiarEstado([FromBody] ChangeStateOrdenCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var orden = await _mediator.Send(new GetOrdenByIdQuery(id));
            return Ok(orden);
        }

    }
}
