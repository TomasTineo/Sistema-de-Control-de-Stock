using DTOs.Reportes;
using Application.Services.Interfaces;
using ClosedXML.Excel;

namespace Application.Services.Implementations
{
    public class ReporteExportService : IReporteExportService
    {
        public async Task<byte[]> ExportarProductosBajoStockAsync(IEnumerable<ProductoStockDTO> productos, int stockMinimo)
        {
            return await Task.Run(() =>
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Productos Bajo Stock");

                // Título del reporte
                worksheet.Cell(1, 1).Value = "REPORTE: PRODUCTOS CON BAJO STOCK";
                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                worksheet.Range(1, 1, 1, 3).Merge();
                worksheet.Range(1, 1, 1, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Fecha y parámetros
                worksheet.Cell(2, 1).Value = $"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}";
                worksheet.Cell(3, 1).Value = $"Stock Mínimo: {stockMinimo}";
                worksheet.Cell(2, 1).Style.Font.FontSize = 10;
                worksheet.Cell(3, 1).Style.Font.FontSize = 10;

                // Encabezados de columnas
                int headerRow = 5;
                worksheet.Cell(headerRow, 1).Value = "Producto";
                worksheet.Cell(headerRow, 2).Value = "Stock Actual";
                worksheet.Cell(headerRow, 3).Value = "Estado";

                // Estilo de encabezados
                var headerRange = worksheet.Range(headerRow, 1, headerRow, 3);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 120, 212);
                headerRange.Style.Font.FontColor = XLColor.White;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                // Datos
                int currentRow = headerRow + 1;
                foreach (var producto in productos)
                {
                    string estado = producto.StockActual == 0 ? "Sin Stock" :
                                  producto.StockActual < 5 ? "Crítico" : "Bajo";

                    worksheet.Cell(currentRow, 1).Value = producto.NombreProducto;
                    worksheet.Cell(currentRow, 2).Value = producto.StockActual;
                    worksheet.Cell(currentRow, 3).Value = estado;

                    // Colorear según el estado
                    var rowRange = worksheet.Range(currentRow, 1, currentRow, 3);
                    if (producto.StockActual == 0)
                    {
                        rowRange.Style.Fill.BackgroundColor = XLColor.FromArgb(255, 220, 220);
                        rowRange.Style.Font.FontColor = XLColor.DarkRed;
                    }
                    else if (producto.StockActual < 5)
                    {
                        rowRange.Style.Fill.BackgroundColor = XLColor.FromArgb(255, 245, 220);
                        rowRange.Style.Font.FontColor = XLColor.DarkOrange;
                    }

                    rowRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    currentRow++;
                }

                // Total de productos
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = $"Total de productos: {productos.Count()}";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Range(currentRow, 1, currentRow, 3).Merge();

                // Ajustar anchos de columnas
                worksheet.Column(1).Width = 40;
                worksheet.Column(2).Width = 15;
                worksheet.Column(3).Width = 15;

                // Exportar a bytes
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                return stream.ToArray();
            });
        }

        public async Task<byte[]> ExportarTopProductosReservadosAsync(IEnumerable<TopProductoReservadoDTO> productos, int top)
        {
            return await Task.Run(() =>
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Top Productos Reservados");

                // Título del reporte
                worksheet.Cell(1, 1).Value = "REPORTE: TOP PRODUCTOS MÁS RESERVADOS";
                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                worksheet.Range(1, 1, 1, 4).Merge();
                worksheet.Range(1, 1, 1, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Fecha y parámetros
                worksheet.Cell(2, 1).Value = $"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}";
                worksheet.Cell(3, 1).Value = $"Top {top} productos";
                worksheet.Cell(2, 1).Style.Font.FontSize = 10;
                worksheet.Cell(3, 1).Style.Font.FontSize = 10;

                // Encabezados de columnas
                int headerRow = 5;
                worksheet.Cell(headerRow, 1).Value = "Posición";
                worksheet.Cell(headerRow, 2).Value = "Producto";
                worksheet.Cell(headerRow, 3).Value = "Cantidad Reservada";
                worksheet.Cell(headerRow, 4).Value = "Número de Reservas";

                // Estilo de encabezados
                var headerRange = worksheet.Range(headerRow, 1, headerRow, 4);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 120, 212);
                headerRange.Style.Font.FontColor = XLColor.White;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                // Datos
                int currentRow = headerRow + 1;
                int posicion = 1;
                foreach (var producto in productos)
                {
                    worksheet.Cell(currentRow, 1).Value = posicion;
                    worksheet.Cell(currentRow, 2).Value = producto.NombreProducto;
                    worksheet.Cell(currentRow, 3).Value = producto.CantidadReservada;
                    worksheet.Cell(currentRow, 4).Value = producto.NumeroReservas;

                    // Colorear según la posición (top 3)
                    var rowRange = worksheet.Range(currentRow, 1, currentRow, 4);
                    if (posicion <= 3)
                    {
                        rowRange.Style.Fill.BackgroundColor = XLColor.FromArgb(220, 240, 255);
                        rowRange.Style.Font.Bold = true;
                    }

                    rowRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    currentRow++;
                    posicion++;
                }

                // Totales
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = $"Total de productos: {productos.Count()}";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Range(currentRow, 1, currentRow, 2).Merge();

                currentRow++;
                worksheet.Cell(currentRow, 1).Value = $"Total de reservas: {productos.Sum(p => p.CantidadReservada)}";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Range(currentRow, 1, currentRow, 2).Merge();

                // Ajustar anchos de columnas
                worksheet.Column(1).Width = 12;
                worksheet.Column(2).Width = 40;
                worksheet.Column(3).Width = 20;
                worksheet.Column(4).Width = 20;

                // Exportar a bytes
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                return stream.ToArray();
            });
        }
    }
}
