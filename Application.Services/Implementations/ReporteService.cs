using DTOs.Reportes;
using Data.Repositories;
using Application.Services.Interfaces;

namespace Application.Services.Implementations
{
    public class ReporteService : IReportesService
    {
        private readonly IReportesRepository _reporteRepository;

        public ReporteService(IReportesRepository reporteRepository)
        {
            _reporteRepository = reporteRepository;
        }

        public async Task<IEnumerable<ProductoStockDTO>> GetProductosBajoStockAsync(int stockMinimo)
        {
            // Logica
            if (stockMinimo < 0)
                throw new ArgumentException("El stock mínimo no puede ser negativo");

            return await _reporteRepository.GetProductosBajoStockAsync(stockMinimo);
        }

        public async Task<IEnumerable<TopProductoReservadoDTO>> GetTopProductosReservadosAsync(int top)
        {
            if (top <= 0)
                throw new ArgumentException("El número de productos debe ser mayor a cero");

            return await _reporteRepository.GetTopProductosReservadosAsync(top);
        }
    }
}
