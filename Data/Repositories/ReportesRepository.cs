using DTOs.Reportes;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient; // Necesario para ADO.NET
using System.Globalization;


namespace Data.Repositories
{
    public class ReporteRepository : IReportesRepository
    {
        private readonly string _connectionString;

        public ReporteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' no encontrada.");
        }

        // Implementación ADO.NET para Reporte 1 (Bajo Stock)
        public async Task<IEnumerable<ProductoStockDTO>> GetProductosBajoStockAsync(int stockMinimo)
        {
            var productos = new List<ProductoStockDTO>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT Nombre, Stock FROM Productos WHERE Stock < @StockMinimo ORDER BY Stock ASC";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@StockMinimo", stockMinimo);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            productos.Add(new ProductoStockDTO
                            {
                                NombreProducto = reader["Nombre"].ToString() ?? string.Empty,
                                StockActual = Convert.ToInt32(reader["Stock"])
                            });
                        }
                    }
                }
            }
            return productos;
        }

        // Implementación ADO.NET para Reporte 2 (Top Productos Más Reservados)
        public async Task<IEnumerable<TopProductoReservadoDTO>> GetTopProductosReservadosAsync(int top)
        {
            var productos = new List<TopProductoReservadoDTO>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                    SELECT TOP (@Top)
                        p.Nombre AS NombreProducto,
                        SUM(rp.CantidadReservada) AS CantidadReservada,
                        COUNT(DISTINCT rp.ReservaId) AS NumeroReservas
                    FROM ReservaProductos rp
                    INNER JOIN Productos p ON rp.ProductoId = p.Id
                    GROUP BY p.Nombre
                    ORDER BY CantidadReservada DESC";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Top", top);
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            productos.Add(new TopProductoReservadoDTO
                            {
                                NombreProducto = reader["NombreProducto"].ToString() ?? string.Empty,
                                CantidadReservada = Convert.ToInt32(reader["CantidadReservada"]),
                                NumeroReservas = Convert.ToInt32(reader["NumeroReservas"])
                            });
                        }
                    }
                }
            }
            return productos;
        }
    }
}
