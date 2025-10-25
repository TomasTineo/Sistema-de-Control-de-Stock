using DTOs.Reportes;

namespace Application.Services.Interfaces
{
    public interface IReportesService
    {
        Task<IEnumerable<ProductoStockDTO>> GetProductosBajoStockAsync(int stockMinimo);
        Task<IEnumerable<ReservasPorMesDTO>> GetReservasPorMesAsync(int anio);
    }
}
