using Application.Services.Interfaces;
using DTOs.Eventos;

namespace WebAPI.Endpoints
{
    public static class EventosEndpoints
    {
        public static void MapEventosEndpoints(this WebApplication app)
        {
            var eventos = app.MapGroup("/api/eventos")
                .WithTags("Eventos");

            // GET /api/eventos - Requiere permiso de lectura
            eventos.MapGet("/", async (IEventoService eventoService) =>
            {
                var eventosLista = await eventoService.GetAllAsync();
                return Results.Ok(eventosLista);
            })
            .WithName("GetEventos")
            .RequireAuthorization("EventosLeer")
            .Produces<IEnumerable<EventoDTO>>(StatusCodes.Status200OK);

            // GET /api/eventos/{id} - Requiere permiso de lectura
            eventos.MapGet("/{id:int}", async (int id, IEventoService eventoService) =>
            {
                var evento = await eventoService.GetAsync(id);
                return evento == null ? Results.NotFound() : Results.Ok(evento);
            })
            .WithName("GetEvento")
            .RequireAuthorization("EventosLeer")
            .Produces<EventoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            // POST /api/eventos - Requiere permiso de agregar
            eventos.MapPost("/", async (CreateEventoRequest request, IEventoService eventoService) =>
            {
                try
                {
                    var evento = await eventoService.CreateAsync(request);
                    return Results.CreatedAtRoute("GetEvento", new { id = evento.Id }, evento);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("CreateEvento")
            .RequireAuthorization("EventosAgregar")
            .Produces<EventoDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            // PUT /api/eventos/{id} - Requiere permiso de actualizar
            eventos.MapPut("/{id:int}", async (int id, EventoDTO request, IEventoService eventoService) =>
            {
                if (id != request.Id)
                    return Results.BadRequest("ID mismatch");

                try
                {
                    var result = await eventoService.UpdateAsync(request);
                    return result ? Results.NoContent() : Results.NotFound();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("UpdateEvento")
            .RequireAuthorization("EventosActualizar")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

            // DELETE /api/eventos/{id} - Requiere permiso de eliminar
            eventos.MapDelete("/{id:int}", async (int id, IEventoService eventoService) =>
            {
                var result = await eventoService.DeleteAsync(id);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteEvento")
            .RequireAuthorization("EventosEliminar")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

            // GET /api/eventos/rango?fechaInicio=...&fechaFin=... - Requiere permiso de lectura
            eventos.MapGet("/rango", async (DateTime fechaInicio, DateTime fechaFin, IEventoService eventoService) =>
            {
                var eventosRango = await eventoService.GetByFechaRangeAsync(fechaInicio, fechaFin);
                return Results.Ok(eventosRango);
            })
            .WithName("GetEventosByFechaRange")
            .RequireAuthorization("EventosLeer")
            .Produces<IEnumerable<EventoDTO>>(StatusCodes.Status200OK);
        }
    }
}
