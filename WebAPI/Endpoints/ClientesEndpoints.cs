using Application.Services.Interfaces;
using DTOs.Clientes;

namespace WebAPI.Endpoints
{
    public static class ClientesEndpoints
    {
        public static void MapClientesEndpoints(this WebApplication app)
        {
            var clientes = app.MapGroup("/api/clientes")
                .WithTags("Clientes");

            // GET /api/clientes - Requiere permiso de lectura
            clientes.MapGet("/", async (IClienteService clienteService) =>
            {
                var clientesLista = await clienteService.GetAllAsync();
                return Results.Ok(clientesLista);
            })
            .WithName("GetClientes")
            .RequireAuthorization("ClientesLeer")
            .Produces<IEnumerable<ClienteDTO>>(StatusCodes.Status200OK);


            // GET /api/clientes/{id} - Requiere permiso de lectura
            clientes.MapGet("/{id:int}", async (int id, IClienteService clienteService) =>
            {
                var cliente = await clienteService.GetAsync(id);
                return cliente == null ? Results.NotFound() : Results.Ok(cliente);
            })
            .WithName("GetCliente")
            .RequireAuthorization("ClientesLeer")
            .Produces<ClienteDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);


            // POST /api/clientes - Requiere permiso de agregar
            clientes.MapPost("/", async (CreateClienteRequest request, IClienteService clienteService) =>
            {
                try
                {
                    var cliente = await clienteService.CreateAsync(request);
                    return Results.CreatedAtRoute("GetCliente", new { id = cliente.Id }, cliente);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("CreateCliente")
            .RequireAuthorization("ClientesAgregar")
            .Produces<ClienteDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);


            // PUT /api/clientes/{id} - Requiere permiso de actualizar
            clientes.MapPut("/{id:int}", async (int id, UpdateClienteRequest request, IClienteService clienteService) =>
            {
                if (id != request.Id)
                    return Results.BadRequest("ID mismatch");

                try
                {
                    var result = await clienteService.UpdateAsync(request);
                    return result ? Results.NoContent() : Results.NotFound();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("UpdateCliente")
            .RequireAuthorization("ClientesActualizar")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);


            // DELETE /api/clientes/{id} - Requiere permiso de eliminar
            clientes.MapDelete("/{id:int}", async (int id, IClienteService clienteService) =>
            {
                var result = await clienteService.DeleteAsync(id);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteCliente")
            .RequireAuthorization("ClientesEliminar")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}
