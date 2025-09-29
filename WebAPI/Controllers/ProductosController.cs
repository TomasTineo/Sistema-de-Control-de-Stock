using Microsoft.AspNetCore.Mvc;
using Application.Services;
using DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetProductos()
        {
            var productos = await _productoService.GetAllAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> GetProducto(int id)
        {
            var producto = await _productoService.GetAsync(id);
            
            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductoDTO>> PostProducto([FromBody] CreateProductoRequest request)
        {
            try
            {
                var producto = await _productoService.CreateAsync(request);
                return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, [FromBody] UpdateProductoRequest request)
        {
            if (id != request.Id)
                return BadRequest("ID mismatch");

            try
            {
                var result = await _productoService.UpdateAsync(request);
                
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var result = await _productoService.DeleteAsync(id);
            
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetProductosByCategoria(int categoriaId)
        {
            var productos = await _productoService.GetByCategoriaAsync(categoriaId);
            return Ok(productos);
        }
    }
}