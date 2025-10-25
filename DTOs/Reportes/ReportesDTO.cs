namespace DTOs.Reportes
{
    // DTO para el Reporte 1: Productos con bajo stock.
    public class ProductoStockDTO
    {
        public string NombreProducto { get; set; } = string.Empty;
        public int StockActual { get; set; }
    }

    // DTO para el Reporte 2: Reservas por mes.
    public class ReservasPorMesDTO
    {
        public int MesNumero { get; set; }
        public string NombreMes { get; set; } = string.Empty;
        public int TotalReservas { get; set; }
    }
}
