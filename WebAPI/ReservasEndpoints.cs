using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class ReservasEndpoints
    {
        public static void MapReservasEndpoints(this WebApplication app)
        {
            var reservas = app.MapGroup("/api/reservas")
                .WithTags("Reservas");

            // GET /api/reservas - Listar todas las reservas
            reservas.MapGet("/", async (IReservaService reservaService) =>
            {
                var reservasLista = await reservaService.GetAllAsync();
                return Results.Ok(reservasLista);
            })
            .WithName("GetReservas")
            .RequireAuthorization("ReservasLeer")
            .Produces<IEnumerable<ReservaDTO>>(StatusCodes.Status200OK);

            // GET /api/reservas/{id} - Obtener reserva por ID
            reservas.MapGet("/{id:int}", async (int id, IReservaService reservaService) =>
            {
                var reserva = await reservaService.GetAsync(id);
                return reserva == null ? Results.NotFound() : Results.Ok(reserva);
            })
            .WithName("GetReserva")
            .RequireAuthorization("ReservasLeer")
            .Produces<ReservaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            // GET /api/reservas/cliente/{clienteId} - Obtener reservas por cliente
            reservas.MapGet("/cliente/{clienteId:int}", async (int clienteId, IReservaService reservaService) =>
            {
                var reservasCliente = await reservaService.GetByClienteAsync(clienteId);
                return Results.Ok(reservasCliente);
            })
            .WithName("GetReservasByCliente")
            .RequireAuthorization("ReservasLeer")
            .Produces<IEnumerable<ReservaDTO>>(StatusCodes.Status200OK);

            // GET /api/reservas/evento/{eventoId} - Obtener reservas por evento
            reservas.MapGet("/evento/{eventoId:int}", async (int eventoId, IReservaService reservaService) =>
            {
                var reservasEvento = await reservaService.GetByEventoAsync(eventoId);
                return Results.Ok(reservasEvento);
            })
            .WithName("GetReservasByEvento")
            .RequireAuthorization("ReservasLeer")
            .Produces<IEnumerable<ReservaDTO>>(StatusCodes.Status200OK);

            // GET /api/reservas/estado/{estado} - Obtener reservas por estado
            reservas.MapGet("/estado/{estado}", async (string estado, IReservaService reservaService) =>
            {
                var reservasEstado = await reservaService.GetByEstadoAsync(estado);
                return Results.Ok(reservasEstado);
            })
            .WithName("GetReservasByEstado")
            .RequireAuthorization("ReservasLeer")
            .Produces<IEnumerable<ReservaDTO>>(StatusCodes.Status200OK);

            // GET /api/reservas/rango?fechaInicio=...&fechaFin=... - Obtener reservas por rango de fechas
            reservas.MapGet("/rango", async (DateTime fechaInicio, DateTime fechaFin, IReservaService reservaService) =>
            {
                var reservasRango = await reservaService.GetByFechaRangoAsync(fechaInicio, fechaFin);
                return Results.Ok(reservasRango);
            })
            .WithName("GetReservasByFechaRango")
            .RequireAuthorization("ReservasLeer")
            .Produces<IEnumerable<ReservaDTO>>(StatusCodes.Status200OK);

            // POST /api/reservas - Crear nueva reserva
            reservas.MapPost("/", async (CreateReservaRequest request, IReservaService reservaService) =>
            {
                try
                {
                    var reserva = await reservaService.CreateAsync(request);
                    return Results.CreatedAtRoute("GetReserva", new { id = reserva.Id }, reserva);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("CreateReserva")
            .RequireAuthorization("ReservasAgregar")
            .Produces<ReservaDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            // PUT /api/reservas/{id} - Actualizar reserva
            reservas.MapPut("/{id:int}", async (int id, UpdateReservaRequest request, IReservaService reservaService) =>
            {
                if (id != request.Id)
                    return Results.BadRequest(new { error = "El ID de la URL no coincide con el ID del cuerpo" });

                try
                {
                    var result = await reservaService.UpdateAsync(request);
                    return result ? Results.NoContent() : Results.NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdateReserva")
            .RequireAuthorization("ReservasActualizar")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

            // DELETE /api/reservas/{id} - Eliminar reserva
            reservas.MapDelete("/{id:int}", async (int id, IReservaService reservaService) =>
            {
                var result = await reservaService.DeleteAsync(id);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteReserva")
            .RequireAuthorization("ReservasEliminar")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

            // PATCH /api/reservas/{id}/cancelar - Cancelar reserva
            reservas.MapPatch("/{id:int}/cancelar", async (int id, IReservaService reservaService) =>
            {
                try
                {
                    var result = await reservaService.CancelarReservaAsync(id);
                    return result ? Results.NoContent() : Results.NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("CancelarReserva")
            .RequireAuthorization("ReservasActualizar")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

            // PATCH /api/reservas/{id}/confirmar - Confirmar reserva
            reservas.MapPatch("/{id:int}/confirmar", async (int id, IReservaService reservaService) =>
            {
                try
                {
                    var result = await reservaService.ConfirmarReservaAsync(id);
                    return result ? Results.NoContent() : Results.NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("ConfirmarReserva")
            .RequireAuthorization("ReservasActualizar")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}
