using API.Clients;
using DTOs.Reportes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ScottPlot;
using ScottPlot.WinForms;
using System.Net.Http.Headers;
using WinFormsLabel = System.Windows.Forms.Label;
using DrawingColor = System.Drawing.Color;
using DrawingFont = System.Drawing.Font;
using DrawingFontStyle = System.Drawing.FontStyle;

namespace Escritorio
{
    public partial class Form_Reportes : Form
    {
        private readonly ReportesApiClient _reportesApiClient;
        private readonly IConfiguration _configuration;
        
        // Controles UI
        private TabControl tabControl;
        private TabPage tabProductosStock;
        private TabPage tabTopProductos;
        
        // Tab 1: Productos con Bajo Stock
        private DataGridView dgvProductosStock;
        private NumericUpDown numStockMinimo;
        private Button btnGenerarReporte1;
        private WinFormsLabel lblStockMinimo;
        private WinFormsLabel lblTotalProductos;
        
        // Tab 2: Top Productos Más Reservados (con gráfico)
        private DataGridView dgvTopProductos;
        private FormsPlot plotTopProductos;
        private NumericUpDown numTopProductos;
        private Button btnGenerarReporte2;
        private WinFormsLabel lblTopProductos;
        private WinFormsLabel lblTotalReservasProductos;

        public Form_Reportes()
        {
            _reportesApiClient = Program.ServiceProvider.GetRequiredService<ReportesApiClient>();
            _configuration = Program.ServiceProvider.GetRequiredService<IConfiguration>();
            InitializeComponent();
            ConfigurarFormulario();
            ConfigurarTabProductosStock();
            ConfigurarTabTopProductos();
        }

        private void ConfigurarFormulario()
        {
            // Configuración inicial
            if (numStockMinimo != null)
                numStockMinimo.Value = 10;
            if (numTopProductos != null)
                numTopProductos.Value = 5;
        }

        #region Tab 1: Productos con Bajo Stock

        private void ConfigurarTabProductosStock()
        {
            // Panel de exportación en la parte inferior
            Panel panelExportacion = new Panel
            {
                Height = 60,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = DrawingColor.FromArgb(250, 250, 250),
                Dock = DockStyle.Bottom,
                Padding = new Padding(10, 10, 10, 10)
            };

            var lblExportar = new WinFormsLabel
            {
                Text = "Exportar a:",
                Location = new Point(10, 15),
                Size = new Size(80, 25),
                Font = new DrawingFont("Segoe UI", 10F, DrawingFontStyle.Bold)
            };

            var btnExportarExcel = new Button
            {
                Text = "Excel",
                Location = new Point(100, 10),
                Size = new Size(150, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = DrawingColor.FromArgb(23, 162, 184),
                ForeColor = DrawingColor.White,
                Font = new DrawingFont("Segoe UI", 9F, DrawingFontStyle.Bold)
            };
            btnExportarExcel.FlatAppearance.BorderSize = 0;
            btnExportarExcel.Click += async (s, e) => await ExportarReporte1Async();

            panelExportacion.Controls.AddRange(new Control[]
            {
                lblExportar,
                btnExportarExcel
            });

            // Panel superior con controles
            Panel panelControles = new Panel
            {
                Height = 70,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = DrawingColor.FromArgb(240, 240, 240),
                Dock = DockStyle.Top,
                Padding = new Padding(10, 10, 10, 10)
            };

            lblStockMinimo = new WinFormsLabel
            {
                Text = "Stock mínimo:",
                Location = new Point(10, 20),
                Size = new Size(100, 25),
                Font = new DrawingFont("Segoe UI", 10F)
            };

            numStockMinimo = new NumericUpDown
            {
                Location = new Point(110, 18),
                Size = new Size(80, 25),
                Minimum = 1,
                Maximum = 100,
                Value = 10
            };

            btnGenerarReporte1 = new Button
            {
                Text = "Generar Reporte",
                Location = new Point(210, 15),
                Size = new Size(150, 35),
                FlatStyle = FlatStyle.Flat,
                Font = new DrawingFont("Segoe UI", 9F, DrawingFontStyle.Bold),
                BackColor = DrawingColor.FromArgb(0, 120, 212),
                ForeColor = DrawingColor.White
            };
            btnGenerarReporte1.FlatAppearance.BorderSize = 0;
            btnGenerarReporte1.Click += BtnGenerarReporte1_Click;

            lblTotalProductos = new WinFormsLabel
            {
                Text = "Total productos: 0",
                Location = new Point(900, 20),
                Size = new Size(200, 25),
                Font = new DrawingFont("Segoe UI", 10F, DrawingFontStyle.Bold),
                ForeColor = DrawingColor.FromArgb(0, 120, 212)
            };

            panelControles.Controls.AddRange(new Control[] 
            { 
                lblStockMinimo, 
                numStockMinimo, 
                btnGenerarReporte1,
                lblTotalProductos
            });

            // DataGridView (en el centro, llenando el espacio restante)
            dgvProductosStock = new DataGridView
            {
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = DrawingColor.White,
                BorderStyle = BorderStyle.Fixed3D,
                Dock = DockStyle.Fill,
                Margin = new Padding(10),
                ScrollBars = ScrollBars.Both,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
                RowHeadersVisible = true,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                ColumnHeadersHeight = 30
            };

            ConfigurarDataGridViewProductosStock();

            // Agregar controles al tab en el orden correcto (abajo, arriba, centro)
            tabProductosStock.Controls.Add(dgvProductosStock);      // Centro (Fill)
            tabProductosStock.Controls.Add(panelControles);        // Arriba (Top)
            tabProductosStock.Controls.Add(panelExportacion);      // Abajo (Bottom)
        }

        private void ConfigurarDataGridViewProductosStock()
        {
            dgvProductosStock.AutoGenerateColumns = false;
            dgvProductosStock.Columns.Clear();

            dgvProductosStock.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NombreProducto",
                HeaderText = "Producto",
                Name = "NombreProducto",
                Width = 300,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvProductosStock.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StockActual",
                HeaderText = "Stock Actual",
                Name = "StockActual",
                Width = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            dgvProductosStock.RowTemplate.Height = 25;
            dgvProductosStock.AllowUserToResizeRows = true;
        }

        private async void BtnGenerarReporte1_Click(object? sender, EventArgs e)
        {
            try
            {
                btnGenerarReporte1.Enabled = false;
                Cursor = Cursors.WaitCursor;

                int stockMinimo = (int)numStockMinimo.Value;
                var productos = await _reportesApiClient.GetProductosBajoStockAsync(stockMinimo);
                var listaProductos = productos.ToList();
                
                dgvProductosStock.SuspendLayout();
                dgvProductosStock.DataSource = null;
                dgvProductosStock.Rows.Clear();
                
                var bindingList = new System.ComponentModel.BindingList<ProductoStockDTO>(listaProductos);
                dgvProductosStock.DataSource = bindingList;
                
                dgvProductosStock.ResumeLayout();
                dgvProductosStock.Refresh();
                
                lblTotalProductos.Text = $"Total productos: {listaProductos.Count}";

                // Aplicar formato de alerta
                foreach (DataGridViewRow row in dgvProductosStock.Rows)
                {
                    if (row.DataBoundItem != null)
                    {
                        int stock = Convert.ToInt32(row.Cells["StockActual"].Value);
                        if (stock < stockMinimo / 2)
                        {
                            row.DefaultCellStyle.BackColor = DrawingColor.FromArgb(255, 220, 220);
                            row.DefaultCellStyle.ForeColor = DrawingColor.DarkRed;
                        }
                    }
                }

                MessageBox.Show($"Reporte generado exitosamente.\n{listaProductos.Count} productos encontrados.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Acceso Denegado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar reporte: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGenerarReporte1.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Tab 2: Top Productos Más Reservados

        private void ConfigurarTabTopProductos()
        {
            // Panel superior con controles
            Panel panelControles = new Panel
            {
                Height = 60,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = DrawingColor.FromArgb(240, 240, 240),
                Dock = DockStyle.Top
            };

            lblTopProductos = new WinFormsLabel
            {
                Text = "Cantidad de productos:",
                Location = new Point(10, 20),
                Size = new Size(150, 25),
                Font = new DrawingFont("Segoe UI", 10F)
            };

            numTopProductos = new NumericUpDown
            {
                Location = new Point(180, 18),
                Size = new Size(80, 25),
                Minimum = 1,
                Maximum = 100,
                Value = 5
            };

            btnGenerarReporte2 = new Button
            {
                Text = "Generar Reporte",
                Location = new Point(280, 15),
                Size = new Size(150, 35),
                FlatStyle = FlatStyle.Flat,
                Font = new DrawingFont("Segoe UI", 9F, DrawingFontStyle.Bold),
                BackColor = DrawingColor.FromArgb(0, 120, 212),
                ForeColor = DrawingColor.White
            };
            btnGenerarReporte2.FlatAppearance.BorderSize = 0;
            btnGenerarReporte2.Click += BtnGenerarReporte2_Click;

            lblTotalReservasProductos = new WinFormsLabel
            {
                Text = "Total reservas: 0",
                Location = new Point(900, 20),
                Size = new Size(200, 25),
                Font = new DrawingFont("Segoe UI", 10F, DrawingFontStyle.Bold),
                ForeColor = DrawingColor.FromArgb(0, 120, 212)
            };

            panelControles.Controls.AddRange(new Control[]
            {
                lblTopProductos,
                numTopProductos,
                btnGenerarReporte2,
                lblTotalReservasProductos
            });

            // Panel contenedor para DataGridView y gráfico
            Panel panelContenido = new Panel
            {
                Dock = DockStyle.Fill
            };

            // DataGridView
            dgvTopProductos = new DataGridView
            {
                Width = 500,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = DrawingColor.White,
                BorderStyle = BorderStyle.Fixed3D,
                Dock = DockStyle.Left,
                ScrollBars = ScrollBars.Both,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
                RowHeadersVisible = true,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                ColumnHeadersHeight = 30
            };

            ConfigurarDataGridViewTopProductos();

            // Gráfico ScottPlot
            plotTopProductos = new FormsPlot
            {
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill
            };

            panelContenido.Controls.Add(plotTopProductos);
            panelContenido.Controls.Add(dgvTopProductos);

            // Panel de exportación en la parte inferior
            Panel panelExportacion = new Panel
            {
                Height = 50,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = DrawingColor.FromArgb(250, 250, 250),
                Dock = DockStyle.Bottom
            };

            var lblExportar = new WinFormsLabel
            {
                Text = "Exportar a:",
                Location = new Point(10, 15),
                Size = new Size(80, 25),
                Font = new DrawingFont("Segoe UI", 10F, DrawingFontStyle.Bold)
            };

            var btnExportarExcel = new Button
            {
                Text = "Excel",
                Location = new Point(100, 10),
                Size = new Size(150, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = DrawingColor.FromArgb(23, 162, 184),
                ForeColor = DrawingColor.White,
                Font = new DrawingFont("Segoe UI", 9F, DrawingFontStyle.Bold)
            };
            btnExportarExcel.FlatAppearance.BorderSize = 0;
            btnExportarExcel.Click += async (s, e) => await ExportarReporte2Async();

            panelExportacion.Controls.AddRange(new Control[]
            {
                lblExportar,
                btnExportarExcel
            });

            tabTopProductos.Controls.Add(panelExportacion);
            tabTopProductos.Controls.Add(panelContenido);
            tabTopProductos.Controls.Add(panelControles);
        }

        private void ConfigurarDataGridViewTopProductos()
        {
            dgvTopProductos.AutoGenerateColumns = false;
            dgvTopProductos.Columns.Clear();

            dgvTopProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NombreProducto",
                HeaderText = "Producto",
                Name = "NombreProducto",
                Width = 200,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvTopProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CantidadReservada",
                HeaderText = "Cantidad Reservada",
                Name = "CantidadReservada",
                Width = 130,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            dgvTopProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NumeroReservas",
                HeaderText = "Nº Reservas",
                Name = "NumeroReservas",
                Width = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            dgvTopProductos.RowTemplate.Height = 25;
            dgvTopProductos.AllowUserToResizeRows = true;
        }

        private async void BtnGenerarReporte2_Click(object? sender, EventArgs e)
        {
            try
            {
                btnGenerarReporte2.Enabled = false;
                Cursor = Cursors.WaitCursor;

                int cantidadProductos = (int)numTopProductos.Value;
                var topProductos = await _reportesApiClient.GetTopProductosReservadosAsync(cantidadProductos);
                var listaTopProductos = topProductos.ToList();
                
                dgvTopProductos.SuspendLayout();
                dgvTopProductos.DataSource = null;
                dgvTopProductos.Rows.Clear();
                
                var bindingList = new System.ComponentModel.BindingList<TopProductoReservadoDTO>(listaTopProductos);
                dgvTopProductos.DataSource = bindingList;
                
                dgvTopProductos.ResumeLayout();
                dgvTopProductos.Refresh();
                
                int totalReservas = listaTopProductos.Sum(p => p.CantidadReservada);
                lblTotalReservasProductos.Text = $"Total reservas: {totalReservas}";

                // Generar gráfico
                GenerarGraficoTopProductos(listaTopProductos);

                MessageBox.Show($"Reporte generado exitosamente.\nTotal de reservas de los productos más reservados: {totalReservas}",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Acceso Denegado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar reporte: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGenerarReporte2.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void GenerarGraficoTopProductos(IEnumerable<TopProductoReservadoDTO> topProductos)
        {
            plotTopProductos.Plot.Clear();

            var datos = topProductos.OrderByDescending(p => p.CantidadReservada).ToList();
            
            if (!datos.Any())
            {
                plotTopProductos.Plot.Title("No hay datos para mostrar");
                plotTopProductos.Refresh();
                return;
            }

            // Preparar datos para el gráfico
            double[] valores = datos.Select(p => (double)p.CantidadReservada).ToArray();
            string[] etiquetas = datos.Select(p => p.NombreProducto).ToArray();
            double[] posiciones = Enumerable.Range(0, datos.Count).Select(i => (double)i).ToArray();

            // Crear gráfico de barras
            var barPlot = plotTopProductos.Plot.Add.Bars(posiciones, valores);
            
            // Configurar ejes y título
            plotTopProductos.Plot.Title("Top Productos Más Reservados");
            plotTopProductos.Plot.XLabel("Producto");
            plotTopProductos.Plot.YLabel("Cantidad Reservada");
            
            // Etiquetas personalizadas en el eje X
            var ticks = posiciones.Select((pos, idx) => new ScottPlot.Tick(pos, etiquetas[idx])).ToArray();
            plotTopProductos.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);
            
            // Rotar etiquetas 45 grados
            plotTopProductos.Plot.Axes.Bottom.TickLabelStyle.Rotation = 45;
            plotTopProductos.Plot.Axes.Bottom.TickLabelStyle.Alignment = ScottPlot.Alignment.MiddleLeft;
            
            plotTopProductos.Refresh();
        }

        #endregion

        #region Métodos de Exportación

        private async Task<bool> VerificarConexionApiAsync()
        {
            try
            {
                var baseUrl = _configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5239/";
                if (!baseUrl.EndsWith("/"))
                    baseUrl += "/";

                using var httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(5);
                
                var response = await httpClient.GetAsync($"{baseUrl}api/reportes/stock-bajo?stockMinimo=1");
                return response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.Unauthorized;
            }
            catch
            {
                return false;
            }
        }

        private async Task ExportarReporteAsync(int parametro, string tipoReporte, string endpoint)
        {
            try
            {
                if (!await VerificarConexionApiAsync())
                {
                    var baseUrl = _configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5239/";
                    MessageBox.Show($"No se puede conectar con la API.\n\n" +
                                   $"URL: {baseUrl}\n\n" +
                                   $"Asegúrese de que:\n" +
                                   $"1. El proyecto WebAPI esté en ejecución\n" +
                                   $"2. La URL en appsettings.json sea correcta\n" +
                                   $"3. No haya un firewall bloqueando la conexión",
                        "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files (*.xlsx)|*.xlsx",
                    FileName = $"{tipoReporte}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    Title = "Guardar Reporte Excel"
                };

                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    
                    string filePath = saveFileDialog.FileName;
                    
                    var baseUrl = _configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5239/";
                    if (!baseUrl.EndsWith("/"))
                        baseUrl += "/";
                    
                    using var httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(baseUrl);
                    httpClient.Timeout = TimeSpan.FromMinutes(5);
                    
                    var authService = AuthServiceProvider.Instance;
                    await authService.CheckTokenExpirationAsync();
                    var token = await authService.GetTokenAsync();
                    
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = 
                            new AuthenticationHeaderValue("Bearer", token);
                    }
                    
                    var response = await httpClient.GetAsync(endpoint);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        var bytes = await response.Content.ReadAsByteArrayAsync();
                        await File.WriteAllBytesAsync(filePath, bytes);
                        
                        MessageBox.Show($"Reporte exportado exitosamente a:\n{filePath}",
                            "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        MessageBox.Show("Su sesión ha expirado. Por favor, inicie sesión nuevamente.",
                            "Sesión Expirada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable || 
                             response.StatusCode == System.Net.HttpStatusCode.BadGateway)
                    {
                        MessageBox.Show($"No se puede conectar con la API. Verifique que el servidor esté en ejecución.\n\nURL: {baseUrl}",
                            "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al exportar: {response.StatusCode}\n{response.ReasonPhrase}\n\nDetalles: {errorContent}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Acceso Denegado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error al exportar reporte: No se puede establecer una conexión ya que el equipo de destino denegó expresamente dicha conexión.\n\n" +
                               $"Detalles: {ex.Message}\n\n" +
                               $"Asegúrese de que la API esté corriendo.",
                    "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TaskCanceledException ex)
            {
                MessageBox.Show($"La operación se ha cancelado por timeout. El servidor puede estar sobrecargado o no responder.\n\nDetalles: {ex.Message}",
                    "Timeout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar reporte: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async Task ExportarReporte1Async()
        {
            int stockMinimo = (int)numStockMinimo.Value;
            await ExportarReporteAsync(stockMinimo, "ProductosBajoStock", 
                $"api/reportes/stock-bajo/export?stockMinimo={stockMinimo}");
        }

        private async Task ExportarReporte2Async()
        {
            int top = (int)numTopProductos.Value;
            await ExportarReporteAsync(top, "TopProductosReservados", 
                $"api/reportes/top-productos-reservados/export?top={top}");
        }

        #endregion
    }
}
