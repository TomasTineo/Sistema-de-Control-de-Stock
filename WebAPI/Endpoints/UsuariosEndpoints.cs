using Application.Services.Interfaces;
using DTOs.Usuarios;
using DTOs.Auth;

namespace WebAPI.Endpoints
{
    public static class UsuariosEndpoints
    {
        public static void MapUsuariosEndpoints(this WebApplication app)
        {
            var usuarios = app.MapGroup("/api/usuarios")
                .WithTags("Usuarios");

            // GET /api/usuarios - Requiere permiso de lectura
            usuarios.MapGet("/", async (IUsuarioService usuarioService) =>
            {
                var usuariosLista = await usuarioService.GetAllAsync();
                return Results.Ok(usuariosLista);
            })
            .WithName("GetUsuarios")
            .RequireAuthorization("UsuariosLeer")
            .Produces<IEnumerable<UsuarioDTO>>(StatusCodes.Status200OK);

            // GET /api/usuarios/{id} - Requiere permiso de lectura
            usuarios.MapGet("/{id:int}", async (int id, IUsuarioService usuarioService) =>
            {
                var usuario = await usuarioService.GetAsync(id);
                return usuario == null ? Results.NotFound() : Results.Ok(usuario);
            })
            .WithName("GetUsuario")
            .RequireAuthorization("UsuariosLeer")
            .Produces<UsuarioDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            // POST /api/usuarios - Registro público de nuevos usuarios
            usuarios.MapPost("/", async (CreateUsuarioRequest request, IUsuarioService usuarioService) =>
            {
                try
                {
                    var usuario = await usuarioService.CreateAsync(request);
                    return Results.CreatedAtRoute("GetUsuario", new { id = usuario.Id }, usuario);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("CreateUsuario")
            .AllowAnonymous()
            .Produces<UsuarioDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            // PUT /api/usuarios/{id} - Requiere permiso de actualizar
            usuarios.MapPut("/{id:int}", async (int id, UpdateUsuarioRequest request, IUsuarioService usuarioService) =>
            {
                if (id != request.Id)
                    return Results.BadRequest("ID mismatch");

                try
                {
                    var result = await usuarioService.UpdateAsync(request);
                    return result ? Results.NoContent() : Results.NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("UpdateUsuario")
            .RequireAuthorization("UsuariosActualizar")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

            // DELETE /api/usuarios/{id} - Requiere permiso de eliminar
            usuarios.MapDelete("/{id:int}", async (int id, IUsuarioService usuarioService) =>
            {
                var result = await usuarioService.DeleteAsync(id);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteUsuario")
            .RequireAuthorization("UsuariosEliminar")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

            // POST /api/usuarios/login (login - público)
            usuarios.MapPost("/login", async (LoginRequest request, IUsuarioService usuarioService) =>
            {
                var usuario = await usuarioService.LoginAsync(request.Username, request.Password);
                
                if (usuario == null)
                    return Results.Unauthorized();

                return Results.Ok(usuario);
            })
            .WithName("LoginUsuario")
            .AllowAnonymous()
            .Produces<UsuarioDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

            // GET /api/usuarios/exists/{username} (público)
            usuarios.MapGet("/exists/{username}", async (string username, IUsuarioService usuarioService) =>
            {
                var existe = await usuarioService.ExisteUsernameAsync(username);
                return Results.Ok(existe);
            })
            .WithName("ExisteUsername")
            .AllowAnonymous()
            .Produces<bool>(StatusCodes.Status200OK);
        }
    }
}
