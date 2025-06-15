using APIAUTH.Aplication.CQRS.Commands.Orders;
using APIAUTH.Aplication.CQRS.Queries.Orders;
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
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("changeState")]
        public async Task<IActionResult> CambiarEstado([FromBody] ChangeStateOrdenCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var orden = await _mediator.Send(new GetOrdenByIdQuery(id));
                return Ok(orden);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
