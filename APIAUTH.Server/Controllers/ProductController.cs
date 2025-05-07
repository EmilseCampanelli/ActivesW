using APIAUTH.Aplication.CQRS.Commands.Producto.CreateProducto;
using APIAUTH.Aplication.CQRS.Commands.Producto.UpdateProducto;
using APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser;
using APIAUTH.Aplication.CQRS.Commands.Usuario.UpdateUser;
using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Implementacion;
using APIAUTH.Aplication.Services.Interfaces;
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
        public async Task<ActionResult<ProductoDto>> Get(int id)
        {
            var producto = await _productoService.Get(id);
            return Ok(producto);
        }

        [HttpPost("AddStock")]
        public async Task<IActionResult> AddStock(int productoId, int stock)
        {
            var producto = await _productoService.AddStock(productoId, stock);
            return Ok(producto);
        }

        [HttpPost("RemoveStock")]
        public async Task<IActionResult> RemoveStock(int productoId, int stock)
        {
            var producto = await _productoService.RemoveStock(productoId, stock);
            return Ok(producto);
        }

        /*
         * Agregar producto
         * Editar Products
         * Eliminar Products
         * Agregar stock
         * Quitar stock
         * Obtener un producto
         * Listar productos Disponibles
         * Listar productos Disponibles y sin stock
         * Listar productos Eliminadoss
         */
    }
}
