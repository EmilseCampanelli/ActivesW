using APIAUTH.Aplication.CQRS.Commands.Promotions.Create;
using APIAUTH.Aplication.CQRS.Commands.Promotions.Delete;
using APIAUTH.Aplication.CQRS.Commands.Promotions.Update;
using APIAUTH.Aplication.CQRS.Queries.Promotions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APIAUTH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PromotionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePromotionCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { id });
        }

        [HttpPut("")]
        public async Task<IActionResult> Update(int id, UpdatePromotionCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetPromotionByIdQuery(id));
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _mediator.Send(new GetActivePromotionsQuery());
            return Ok(result);
        }

        [HttpDelete("")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeletePromotionCommand(id));
            return NoContent();
        }


    }
}
