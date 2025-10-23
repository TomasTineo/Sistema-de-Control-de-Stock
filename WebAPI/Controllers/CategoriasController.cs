using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategorias()
        {
            var categorias = await _categoriaService.GetAllAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDTO>> GetCategoria(int id)
        {
            var categoria = await _categoriaService.GetAsync(id);
            
            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> PostCategoria([FromBody] CreateCategoriaRequest request)
        {
            try
            {
                var categoria = await _categoriaService.CreateAsync(request);
                return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoria);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, [FromBody] UpdateCategoriaRequest request)
        {
            if (id != request.Id)
                return BadRequest("ID mismatch");

            try
            {
                var result = await _categoriaService.UpdateAsync(request);
                
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var result = await _categoriaService.DeleteAsync(id);
            
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("exists/{nombre}")]
        public async Task<ActionResult<bool>> ExisteNombre(string nombre)
        {
            var existe = await _categoriaService.ExisteNombreAsync(nombre);
            return Ok(existe);
        }
    }
}