using DTOs.Reportes;

namespace Application.Services.Interfaces
{
    public enum FormatoExportacion
    {
        Excel
    }

    public interface IReporteExportService
    {
        Task<byte[]> ExportarProductosBajoStockAsync(IEnumerable<ProductoStockDTO> productos, int stockMinimo);
        Task<byte[]> ExportarTopProductosReservadosAsync(IEnumerable<TopProductoReservadoDTO> productos, int top);
    }
}
