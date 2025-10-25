using Application.Services;
using Microsoft.AspNetCore.Mvc;
using DTOs;

namespace WebAPI
{
    public static class ReportesEndpoint
    {
        public static void MapReportesEndpoints(this WebApplication app)
        {
            // Agrupamos el endpoint, lo etiquetamos para Swagger y lo protegemos
            var group = app.MapGroup("/api/reportes")
                .WithTags("Reportes");
            // AGREGAR AUTORIZACION SI ES NECESARIO

            // Endpoint GET para el reporte de Bajo Stock
            group.MapGet("/stock-bajo", async (
                [FromServices] IReportesService reporteService,
                [FromQuery] int stockMinimo = 10)
            =>
            {
                var reporte = await reporteService.GetProductosBajoStockAsync(stockMinimo);
                return Results.Ok(reporte);
            })
            .WithDescription("Obtiene productos cuyo stock es menor al mínimo definido.");

            // Endpoint GET para el reporte de Reservas por Mes
            group.MapGet("/reservas-por-mes/{anio}", async (
                [FromServices] IReportesService reporteService,
                int anio)
            =>
            {
                var reporte = await reporteService.GetReservasPorMesAsync(anio);
                return Results.Ok(reporte);
            })
            .WithDescription("Obtiene el número total de reservas por mes para un año dado.");
        }
    }
}
