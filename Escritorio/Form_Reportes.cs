using API.Clients;
using DTOs.Reportes;
using Microsoft.Extensions.DependencyInjection;
using ScottPlot;
using ScottPlot.WinForms;
using System.Data;
using WinFormsLabel = System.Windows.Forms.Label;
using DrawingColor = System.Drawing.Color;
using DrawingFont = System.Drawing.Font;
using DrawingFontStyle = System.Drawing.FontStyle;

namespace Escritorio
{
    public partial class Form_Reportes : Form
    {
        private readonly ReportesApiClient _reportesApiClient;
        
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
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Configuración del formulario
            this.ClientSize = new Size(1200, 700);
            this.Text = "Reportes y Estadísticas";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = DrawingColor.White;
            
            // TabControl principal
            tabControl = new TabControl
            {
                Location = new Point(10, 10),
                Size = new Size(1165, 640),
                Font = new DrawingFont("Segoe UI", 10F)
            };
            
            // Tabs
            tabProductosStock = new TabPage("Productos con Bajo Stock");
            tabTopProductos = new TabPage("Top Productos Más Reservados");
            
            ConfigurarTabProductosStock();
            ConfigurarTabTopProductos();
            
            tabControl.TabPages.Add(tabProductosStock);
            tabControl.TabPages.Add(tabTopProductos);
            
            this.Controls.Add(tabControl);
            this.ResumeLayout(false);
        }

        private void ConfigurarFormulario()
        {
            // Configuración inicial
            numStockMinimo.Value = 10;
            numTopProductos.Value = 5;
        }

        #region Tab 1: Productos con Bajo Stock

        private void ConfigurarTabProductosStock()
        {
            // Panel superior con controles
            Panel panelControles = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(1130, 60),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = DrawingColor.FromArgb(240, 240, 240)
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
                Font = new DrawingFont("Segoe UI", 9F, DrawingFontStyle.Bold)
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

            // DataGridView
            dgvProductosStock = new DataGridView
            {
                Location = new Point(10, 80),
                Size = new Size(1130, 500),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = DrawingColor.White,
                BorderStyle = BorderStyle.Fixed3D
            };

            ConfigurarDataGridViewProductosStock();

            tabProductosStock.Controls.Add(panelControles);
            tabProductosStock.Controls.Add(dgvProductosStock);
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
                Width = 300
            });

            dgvProductosStock.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StockActual",
                HeaderText = "Stock Actual",
                Name = "StockActual",
                Width = 100
            });
        }

        private async void BtnGenerarReporte1_Click(object? sender, EventArgs e)
        {
            try
            {
                btnGenerarReporte1.Enabled = false;
                Cursor = Cursors.WaitCursor;

                int stockMinimo = (int)numStockMinimo.Value;
                var productos = await _reportesApiClient.GetProductosBajoStockAsync(stockMinimo);

                dgvProductosStock.DataSource = productos.ToList();
                lblTotalProductos.Text = $"Total productos: {productos.Count()}";

                // Aplicar formato de alerta
                foreach (DataGridViewRow row in dgvProductosStock.Rows)
                {
                    int stock = Convert.ToInt32(row.Cells["StockActual"].Value);
                    if (stock < stockMinimo / 2)
                    {
                        row.DefaultCellStyle.BackColor = DrawingColor.FromArgb(255, 220, 220);
                        row.DefaultCellStyle.ForeColor = DrawingColor.DarkRed;
                    }
                }

                MessageBox.Show($"Reporte generado exitosamente.\n{productos.Count()} productos encontrados.",
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
                Location = new Point(10, 10),
                Size = new Size(1130, 60),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = DrawingColor.FromArgb(240, 240, 240)
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
                Font = new DrawingFont("Segoe UI", 9F, DrawingFontStyle.Bold)
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

            // DataGridView
            dgvTopProductos = new DataGridView
            {
                Location = new Point(10, 80),
                Size = new Size(500, 500),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = DrawingColor.White,
                BorderStyle = BorderStyle.Fixed3D
            };

            ConfigurarDataGridViewTopProductos();

            // Gráfico ScottPlot
            plotTopProductos = new FormsPlot
            {
                Location = new Point(520, 80),
                Size = new Size(620, 500),
                BorderStyle = BorderStyle.FixedSingle
            };

            tabTopProductos.Controls.Add(panelControles);
            tabTopProductos.Controls.Add(dgvTopProductos);
            tabTopProductos.Controls.Add(plotTopProductos);
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
                Width = 200
            });

            dgvTopProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CantidadReservada",
                HeaderText = "Cantidad Reservada",
                Name = "CantidadReservada",
                Width = 130
            });

            dgvTopProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NumeroReservas",
                HeaderText = "Nº Reservas",
                Name = "NumeroReservas",
                Width = 100
            });
        }

        private async void BtnGenerarReporte2_Click(object? sender, EventArgs e)
        {
            try
            {
                btnGenerarReporte2.Enabled = false;
                Cursor = Cursors.WaitCursor;

                int cantidadProductos = (int)numTopProductos.Value;
                var topProductos = await _reportesApiClient.GetTopProductosReservadosAsync(cantidadProductos);

                dgvTopProductos.DataSource = topProductos.ToList();
                int totalReservas = topProductos.Sum(p => p.CantidadReservada);
                lblTotalReservasProductos.Text = $"Total reservas: {totalReservas}";

                // Generar gráfico
                GenerarGraficoTopProductos(topProductos);

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
    }
}
