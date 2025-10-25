using Application.Services.Interfaces;
using DTOs.Categorias;

namespace WebAPI.Endpoints
{
    public static class CategoriasEndpoints
    {
        public static void MapCategoriasEndpoints(this WebApplication app)
        {
            var categorias = app.MapGroup("/api/categorias")
                .WithTags("Categorias");

            // GET /api/categorias - Requiere permiso de lectura
            categorias.MapGet("/", async (ICategoriaService categoriaService) =>
            {
                var categoriasLista = await categoriaService.GetAllAsync();
                return Results.Ok(categoriasLista);
            })
            .WithName("GetCategorias")
            .RequireAuthorization("CategoriasLeer")
            .Produces<IEnumerable<CategoriaDTO>>(StatusCodes.Status200OK);

            // GET /api/categorias/{id} - Requiere permiso de lectura
            categorias.MapGet("/{id:int}", async (int id, ICategoriaService categoriaService) =>
            {
                var categoria = await categoriaService.GetAsync(id);
                return categoria == null ? Results.NotFound() : Results.Ok(categoria);
            })
            .WithName("GetCategoria")
            .RequireAuthorization("CategoriasLeer")
            .Produces<CategoriaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            // POST /api/categorias - Requiere permiso de agregar
            categorias.MapPost("/", async (CreateCategoriaRequest request, ICategoriaService categoriaService) =>
            {
                try
                {
                    var categoria = await categoriaService.CreateAsync(request);
                    return Results.CreatedAtRoute("GetCategoria", new { id = categoria.Id }, categoria);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("CreateCategoria")
            .RequireAuthorization("CategoriasAgregar")
            .Produces<CategoriaDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            // PUT /api/categorias/{id} - Requiere permiso de actualizar
            categorias.MapPut("/{id:int}", async (int id, CategoriaDTO request, ICategoriaService categoriaService) =>
            {
                if (id != request.Id)
                    return Results.BadRequest("ID mismatch");

                try
                {
                    var result = await categoriaService.UpdateAsync(request);
                    return result ? Results.NoContent() : Results.NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("UpdateCategoria")
            .RequireAuthorization("CategoriasActualizar")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

            // DELETE /api/categorias/{id} - Requiere permiso de eliminar
            categorias.MapDelete("/{id:int}", async (int id, ICategoriaService categoriaService) =>
            {
                var result = await categoriaService.DeleteAsync(id);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteCategoria")
            .RequireAuthorization("CategoriasEliminar")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

            // GET /api/categorias/exists/{nombre} - Requiere permiso de lectura
            categorias.MapGet("/exists/{nombre}", async (string nombre, ICategoriaService categoriaService) =>
            {
                var existe = await categoriaService.ExisteNombreAsync(nombre);
                return Results.Ok(existe);
            })
            .WithName("ExisteNombreCategoria")
            .RequireAuthorization("CategoriasLeer")
            .Produces<bool>(StatusCodes.Status200OK);
        }
    }
}
