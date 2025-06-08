using APIAUTH.Aplication.CQRS.Commands.Favorites;
using APIAUTH.Aplication.CQRS.Queries.Favorites;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APIAUTH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {

        private readonly IMediator _mediator;

        public FavoriteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("toggle")]
        public async Task<IActionResult> Toggle([FromBody] ToggleFavoriteCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetByUser([FromQuery] int userId)
        {
            var result = await _mediator.Send(new GetFavoritesByUserQuery(userId));
            return Ok(result);
        }

    }
}
