using Microsoft.AspNetCore.Mvc;
using Application.Services;
using DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            var usuario = await _usuarioService.GetAsync(id);
            
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> PostUsuario([FromBody] CreateUsuarioRequest request)
        {
            try
            {
                var usuario = await _usuarioService.CreateAsync(request);
                return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] UpdateUsuarioRequest request)
        {
            if (id != request.Id)
                return BadRequest("ID mismatch");

            try
            {
                var result = await _usuarioService.UpdateAsync(request);
                
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
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var result = await _usuarioService.DeleteAsync(id);
            
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDTO>> Login([FromBody] LoginRequest request)
        {
            var usuario = await _usuarioService.LoginAsync(request.Username, request.Password);
            
            if (usuario == null)
                return Unauthorized("Usuario o contraseña incorrectos");

            return Ok(usuario);
        }

        [HttpGet("exists/{username}")]
        public async Task<ActionResult<bool>> ExisteUsername(string username)
        {
            var existe = await _usuarioService.ExisteUsernameAsync(username);
            return Ok(existe);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}