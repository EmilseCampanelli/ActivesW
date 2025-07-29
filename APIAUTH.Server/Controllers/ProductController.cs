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
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateProductoCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return CreatedAtAction(nameof(Get), new { id }, null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            try
            {
                var producto = await _productoService.Get(id);
                return Ok(producto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddOrRemoveStock")]
        [Authorize]
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

        [HttpDelete("{id}")]
        [Authorize]
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
