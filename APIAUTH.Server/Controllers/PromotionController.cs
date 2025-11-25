using APIAUTH.Aplication.CQRS.Commands.Promotion.Create;
using APIAUTH.Aplication.CQRS.Commands.Promotion.Update;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePromotionCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
