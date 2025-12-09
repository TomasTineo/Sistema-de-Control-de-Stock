using DTOs.Reportes;

namespace Blazor.Services
{
    public interface IReportesService
    {
        Task<List<ProductoStockDTO>> GetProductosBajoStockAsync(int stockMinimo = 10);
        Task<List<TopProductoReservadoDTO>> GetTopProductosReservadosAsync(int top = 10);
    }
}
