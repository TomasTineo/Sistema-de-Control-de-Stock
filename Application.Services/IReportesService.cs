using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{

    public interface IReportesService
    {
        Task<IEnumerable<ProductoStockDTO>> GetProductosBajoStockAsync(int stockMinimo);
        Task<IEnumerable<ReservasPorMesDTO>> GetReservasPorMesAsync(int anio);
    }
    
}
