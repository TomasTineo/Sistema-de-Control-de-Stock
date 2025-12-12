using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DTOs.Reportes;

namespace WebAPI.Endpoints
{
    public static class ReportesEndpoints
    {
        public static void MapReportesEndpoints(this WebApplication app)
        {
            // Agrupamos el endpoint, lo etiquetamos para Swagger
            // Permitimos acceso anónimo para reportes (o puedes agregar .RequireAuthorization() si deseas protegerlos)
            var group = app.MapGroup("/api/reportes")
                .WithTags("Reportes")
                .AllowAnonymous(); // Permitir acceso sin autenticación

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

            // Endpoint para exportar - Productos Bajo Stock (Excel)
            group.MapGet("/stock-bajo/export", async (
                [FromServices] IReportesService reporteService,
                [FromServices] IReporteExportService exportService,
                [FromQuery] int stockMinimo = 10)
            =>
            {
                var datos = await reporteService.GetProductosBajoStockAsync(stockMinimo);
                var bytes = await exportService.ExportarProductosBajoStockAsync(datos, stockMinimo);
                
                return Results.File(bytes, 
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                    $"ProductosBajoStock_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
            })
            .WithDescription("Exporta el reporte de productos con bajo stock a Excel (.xlsx)");

            // Endpoint para exportar - Top Productos Reservados (Excel)
            group.MapGet("/top-productos-reservados/export", async (
                [FromServices] IReportesService reporteService,
                [FromServices] IReporteExportService exportService,
                [FromQuery] int top = 10)
            =>
            {
                var datos = await reporteService.GetTopProductosReservadosAsync(top);
                var bytes = await exportService.ExportarTopProductosReservadosAsync(datos, top);
                
                return Results.File(bytes, 
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                    $"TopProductosReservados_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
            })
            .WithDescription("Exporta el reporte de top productos más reservados a Excel (.xlsx)");
        }
    }
}
