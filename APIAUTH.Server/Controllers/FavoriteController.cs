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
            try
            {
                var userId = int.Parse(User.FindFirst("idUser")?.Value ?? "0");

                command.UserId = userId;
                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByUser([FromQuery] int userId)
        {
            try
            {
                var result = await _mediator.Send(new GetFavoritesByUserQuery(userId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
