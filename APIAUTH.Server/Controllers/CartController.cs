using APIAUTH.Aplication.CQRS.Commands.Producto.ProductCart.Create;
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
    }
}
