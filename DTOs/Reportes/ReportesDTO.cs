
namespace DTOs.Reportes
{
    // DTO para el Reporte 1: Productos con bajo stock.
    public class ProductoStockDTO
    {
        public string NombreProducto { get; set; } = string.Empty;
        public int StockActual { get; set; }
    }

    // DTO para el Reporte 2: Top Productos Más Reservados
    public class TopProductoReservadoDTO
    {
        public string NombreProducto { get; set; } = string.Empty;
        public int CantidadReservada { get; set; }
        public int NumeroReservas { get; set; }
    }
}
