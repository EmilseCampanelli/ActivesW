using APIAUTH.Aplication.CQRS.Commands.Producto.CreateProducto;
using APIAUTH.Aplication.CQRS.Commands.Producto.UpdateProducto;
using APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser;
using APIAUTH.Aplication.CQRS.Commands.Usuario.UpdateUser;
using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Implementacion;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using MediatR;
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
        public async Task<IActionResult> Create([FromBody] CreateProductoCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductoCommand command)
        {
            if (id != command.Id)
                return BadRequest("El ID de la ruta no coincide con el del cuerpo");

            var success = await _mediator.Send(command);

            return Ok(success);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var producto = await _productoService.Get(id);
            return Ok(producto);
        }

        [HttpPost("AddOrRemoveStock")]
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

        /*
         * Obtener producto
         * Eliminar Products
         * Listar productos Disponibles
         * Listar productos Disponibles y sin stock
         * Listar productos Eliminadoss
         */
    }
}
