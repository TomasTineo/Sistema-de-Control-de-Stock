using DTOs.Reportes;

namespace Data.Repositories
{
    // Contrato para los reportes que usan consultas específicas (ADO.NET)
    public interface IReportesRepository
    {
        Task<IEnumerable<ProductoStockDTO>> GetProductosBajoStockAsync(int stockMinimo);
        Task<IEnumerable<TopProductoReservadoDTO>> GetTopProductosReservadosAsync(int top);
    }
}
