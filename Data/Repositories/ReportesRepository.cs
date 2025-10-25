using DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient; // Necesario para ADO.NET
using System.Globalization;


namespace Data.Repositories.Implementations
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
                                NombreProducto = reader["Nombre"].ToString(),
                                StockActual = Convert.ToInt32(reader["Stock"])
                            });
                        }
                    }
                }
            }
            return productos;
        }

        // Implementación ADO.NET para Reporte 2 (Reservas por Mes)
        public async Task<IEnumerable<ReservasPorMesDTO>> GetReservasPorMesAsync(int anio)
        {
            var reservas = new List<ReservasPorMesDTO>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                    SELECT 
                        MONTH(FechaReserva) AS MesNumero, 
                        COUNT(Id) AS TotalReservas
                    FROM Reservas
                    WHERE YEAR(FechaReserva) = @Anio
                    GROUP BY MONTH(FechaReserva)
                    ORDER BY MesNumero ASC";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Anio", anio);
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int mesNum = Convert.ToInt32(reader["MesNumero"]);
                            reservas.Add(new ReservasPorMesDTO
                            {
                                MesNumero = mesNum,
                                NombreMes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mesNum),
                                TotalReservas = Convert.ToInt32(reader["TotalReservas"])
                            });
                        }
                    }
                }
            }
            return reservas;
        }
    }
}
