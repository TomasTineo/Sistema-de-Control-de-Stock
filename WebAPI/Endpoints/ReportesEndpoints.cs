using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DTOs.Reportes;

namespace WebAPI.Endpoints
{
    public static class ReportesEndpoints
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

            // Endpoint GET para el reporte de Top Productos Más Reservados
            group.MapGet("/top-productos-reservados", async (
                [FromServices] IReportesService reporteService,
                [FromQuery] int top = 10)
            =>
            {
                var reporte = await reporteService.GetTopProductosReservadosAsync(top);
                return Results.Ok(reporte);
            })
            .WithDescription("Obtiene los productos más reservados ordenados por cantidad.");
        }
    }
}
