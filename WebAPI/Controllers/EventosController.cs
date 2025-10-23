using Microsoft.AspNetCore.Mvc;
using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventoDTO>>> GetEventos()
        {
            var eventos = await _eventoService.GetAllAsync();
            return Ok(eventos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventoDTO>> GetEvento(int id)
        {
            var evento = await _eventoService.GetAsync(id);
            
            if (evento == null)
                return NotFound();

            return Ok(evento);
        }

        [HttpPost]
        public async Task<ActionResult<EventoDTO>> PostEvento([FromBody] CreateEventoRequest request)
        {
            try
            {
                var evento = await _eventoService.CreateAsync(request);
                return CreatedAtAction(nameof(GetEvento), new { id = evento.Id }, evento);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvento(int id, [FromBody] UpdateEventoRequest request)
        {
            if (id != request.Id)
                return BadRequest("ID mismatch");

            try
            {
                var result = await _eventoService.UpdateAsync(request);
                
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            var result = await _eventoService.DeleteAsync(id);
            
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("rango")]
        public async Task<ActionResult<IEnumerable<EventoDTO>>> GetEventosByFechaRange([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            var eventos = await _eventoService.GetByFechaRangeAsync(fechaInicio, fechaFin);
            return Ok(eventos);
        }
    }
}