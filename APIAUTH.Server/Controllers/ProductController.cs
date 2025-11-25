using APIAUTH.Aplication.CQRS.Commands.Producto;
using APIAUTH.Aplication.CQRS.Commands.Producto.CreateProducto;
using APIAUTH.Aplication.CQRS.Commands.Producto.UpdateProducto;
using APIAUTH.Aplication.CQRS.Queries.Products;
using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Shared.Parameters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace APIAUTH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProductService _productoService;

        public ProductController(IMediator mediator, IProductService productoService)
        {
            _mediator = mediator;
            _productoService = productoService;
        }

        [HttpPost("Create")]
        [Authorize(Policy = "UserAndAdmin")]
        public async Task<IActionResult> Create([FromBody] CreateProductoCommand command)
        {
            try
            {
                var slug = await _mediator.Send(command);
                return Ok(slug);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("")]
        [Authorize(Policy = "UserAndAdmin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductoCommand command)
        {
            try
            {
                if (id != command.Id)
                    return BadRequest("El ID de la ruta no coincide con el del cuerpo");

                var success = await _mediator.Send(command);

                return Ok(success);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("idUser")?.Value ?? "0");
                var producto = await _productoService.Get(id, userId);
                return Ok(producto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddOrRemoveStock")]
        [Authorize(Policy = "UserAndAdmin")]
        public async Task<IActionResult> AddStock(int productoId, int stock, string operation)
        {
            try
            {
                var product = new ProductDto();
                if (operation == "ADD")
                {
                    product = await _productoService.AddStock(productoId, stock);
                }
                else
                {
                    product = await _productoService.RemoveStock(productoId, stock);
                }
                return Ok(product);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] ProductQueryParameters parameters)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("idUser")?.Value ?? "0"); 

                parameters.UserId = userId;
                var result = await _mediator.Send(new GetProductsQuery(parameters));
                return Ok(result);

            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
            
        }

        [HttpDelete("")]
        [Authorize(Policy = "UserAndAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _mediator.Send(new DeleteProductoCommand(id));
                if (!success)
                    return NotFound("Producto no encontrado");

                return Ok("Producto eliminado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
