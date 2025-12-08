using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using API.Clients;
using DTOs.Reservas;
using DTOs.Clientes;
using DTOs.Eventos;
using DTOs.Productos;

namespace Escritorio
{
    public partial class Form_Reserva : Form
    {
        private readonly ReservaApiClient _reservaApiClient;
        private readonly ClienteApiClient _clienteApiClient;
        private readonly EventoApiClient _eventoApiClient;
        private readonly ProductoApiClient _productoApiClient;
        
        private List<ReservaDTO> _reservas = new List<ReservaDTO>();
        private List<ClienteDTO> _clientes = new List<ClienteDTO>();
        private List<EventoDTO> _eventos = new List<EventoDTO>();
        private List<ProductoDTO> _productos = new List<ProductoDTO>();
        
        // Lista temporal de productos para la reserva en edición
        private BindingList<ReservaProductoItem> _productosReserva = new BindingList<ReservaProductoItem>();

        public Form_Reserva()
        {
            InitializeComponent();
            
            // Obtener servicios desde el contenedor DI
            _reservaApiClient = Program.ServiceProvider.GetRequiredService<ReservaApiClient>();
            _clienteApiClient = Program.ServiceProvider.GetRequiredService<ClienteApiClient>();
            _eventoApiClient = Program.ServiceProvider.GetRequiredService<EventoApiClient>();
            _productoApiClient = Program.ServiceProvider.GetRequiredService<ProductoApiClient>();
        }

        private async void Form_Reserva_Load(object sender, EventArgs e)
        {
            // Placeholders
            txt_Buscar.PlaceholderText = "?? Buscar por cliente o evento...";
            
            // Configurar DataGridView de Reservas
            ConfigurarDataGridViewReservas();
            
            // Configurar DataGridView de Productos
            ConfigurarDataGridViewProductos();
            
            // Configurar ComboBox de Estados
            ConfigurarComboEstados();
            
            // Configurar DateTimePicker para fecha de finalización
            dtp_FechaReserva.Value = DateTime.Now.AddDays(1); // Por defecto, un día después
            dtp_FechaReserva.MinDate = DateTime.Now; // No permitir fechas pasadas
            
            // Evento de búsqueda
            txt_Buscar.TextChanged += txt_Buscar_TextChanged;
            
            // Cargar datos
            await CargarClientesAsync();
            await CargarEventosAsync();
            await CargarProductosAsync();
            await CargarReservasAsync();

            // Aplicar permisos
            await ConfigurarPermisos();
        }

        private async Task ConfigurarPermisos()
        {
            var authService = AuthServiceProvider.Instance;

            // Verificar permisos 
            btn_Crear.Enabled = await authService.HasPermissionAsync("reservas.agregar");
            btn_Editar.Enabled = await authService.HasPermissionAsync("reservas.actualizar");
            btn_Eliminar.Enabled = await authService.HasPermissionAsync("reservas.eliminar");

            // Aplicar estilo visual a botones deshabilitados
            if (!btn_Crear.Enabled)
            {
                btn_Crear.Text = "?? " + btn_Crear.Text;
                btn_Crear.ForeColor = Color.Gray;
            }
            
            if (!btn_Editar.Enabled)
            {
                btn_Editar.Text = "?? " + btn_Editar.Text;
                btn_Editar.ForeColor = Color.Gray;
            }
            
            if (!btn_Eliminar.Enabled)
            {
                btn_Eliminar.Text = "?? " + btn_Eliminar.Text;
                btn_Eliminar.ForeColor = Color.Gray;
            }
        }

        private void ConfigurarDataGridViewReservas()
        {
            GrdVw_Reservas.AutoGenerateColumns = false;
            GrdVw_Reservas.Columns.Clear();

            GrdVw_Reservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 50,
                ReadOnly = true
            });

            GrdVw_Reservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Cliente",
                Name = "ClienteNombre",
                Width = 150,
                ReadOnly = true
            });

            GrdVw_Reservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Evento",
                Name = "EventoNombre",
                Width = 150,
                ReadOnly = true
            });

            GrdVw_Reservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Fecha Creación",
                DataPropertyName = "FechaReserva",
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" },
                Width = 130,
                ReadOnly = true
            });

            GrdVw_Reservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Fecha Finalización",
                DataPropertyName = "FechaFinalizacion",
                DefaultCellStyle = { Format = "dd/MM/yyyy" },
                Width = 130,
                ReadOnly = true
            });

            // Columna de Estado - SOLO LECTURA
            GrdVw_Reservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                Width = 110,
                ReadOnly = true
            });

            GrdVw_Reservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Total",
                DataPropertyName = "TotalReserva",
                DefaultCellStyle = { Format = "C2" },
                Width = 100,
                ReadOnly = true
            });

            GrdVw_Reservas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            // Evento para formatear las celdas de Cliente y Evento
            GrdVw_Reservas.CellFormatting += GrdVw_Reservas_CellFormatting;
        }


        // El DataGridView no soporta navegación de propiedades anidadas cuando pueden ser nulas, las inyectamos con una función usando el nombre del campo
        private void GrdVw_Reservas_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (GrdVw_Reservas.Rows[e.RowIndex].DataBoundItem is ReservaDTO reserva)
            {
                // Columna Cliente
                if (GrdVw_Reservas.Columns[e.ColumnIndex].Name == "ClienteNombre")
                {
                    e.Value = reserva.Cliente?.Nombre ?? "Sin cliente";
                    e.FormattingApplied = true;
                }
                // Columna Evento
                else if (GrdVw_Reservas.Columns[e.ColumnIndex].Name == "EventoNombre")
                {
                    e.Value = reserva.Evento?.NombreEvento ?? "Sin evento";
                    e.FormattingApplied = true;
                }
            }
        }

        private void ConfigurarDataGridViewProductos()
        {
            GrdVw_Productos.AutoGenerateColumns = false;
            GrdVw_Productos.Columns.Clear();

            GrdVw_Productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Producto",
                DataPropertyName = "NombreProducto"
            });

            GrdVw_Productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Cantidad",
                DataPropertyName = "Cantidad",
                Width = 80
            });

            GrdVw_Productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Precio Unit.",
                DataPropertyName = "PrecioUnitario",
                DefaultCellStyle = { Format = "C2" },
                Width = 100
            });

            GrdVw_Productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Subtotal",
                DataPropertyName = "Subtotal",
                DefaultCellStyle = { Format = "C2" },
                Width = 100
            });

            GrdVw_Productos.DataSource = _productosReserva;
            GrdVw_Productos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ConfigurarComboEstados()
        {
            var estados = new List<string> { "Pendiente", "Confirmada", "Entregada", "Cancelada", "Completada" };
            cmb_Estado.DataSource = estados;
            cmb_Estado.SelectedIndex = 0;
        }

        private async Task CargarClientesAsync()
        {
            try
            {
                _clientes = (await _clienteApiClient.GetAllAsync()).ToList();
                cmb_Cliente.DataSource = null;
                cmb_Cliente.DataSource = _clientes;
                cmb_Cliente.DisplayMember = "Nombre";
                cmb_Cliente.ValueMember = "Id";
                
                if (_clientes.Any())
                {
                    cmb_Cliente.SelectedIndex = 0;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Falta de autorización.",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarEventosAsync()
        {
            try
            {
                _eventos = (await _eventoApiClient.GetAllAsync()).ToList();
                cmb_Evento.DataSource = null;
                cmb_Evento.DataSource = _eventos;
                cmb_Evento.DisplayMember = "NombreEvento";
                cmb_Evento.ValueMember = "Id";
                
                if (_eventos.Any())
                {
                    cmb_Evento.SelectedIndex = 0;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Falta de autorización.",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar eventos: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarProductosAsync()
        {
            try
            {
                _productos = (await _productoApiClient.GetAllAsync()).ToList();
                cmb_Producto.DataSource = null;
                cmb_Producto.DataSource = _productos;
                cmb_Producto.DisplayMember = "Nombre";
                cmb_Producto.ValueMember = "Id";
                
                if (_productos.Any())
                {
                    cmb_Producto.SelectedIndex = 0;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Falta de autorización.",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarReservasAsync()
        {
            try
            {
                _reservas = (await _reservaApiClient.GetAllAsync()).ToList();
                GrdVw_Reservas.DataSource = null;
                GrdVw_Reservas.DataSource = _reservas;
                lbl_TotalReservas.Text = $"Total: {_reservas.Count} reserva(s)";
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Falta de autorización.",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"No se puede conectar con el servidor:\n{ex.Message}",
                              "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("La petición ha excedido el tiempo de espera.",
                              "Timeout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar reservas: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_Buscar_TextChanged(object? sender, EventArgs e)
        {
            FiltrarReservas();
        }

        private void FiltrarReservas()
        {
            string filtro = txt_Buscar.Text.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(filtro))
            {
                GrdVw_Reservas.DataSource = null;
                GrdVw_Reservas.DataSource = _reservas;
                lbl_TotalReservas.Text = $"Total: {_reservas.Count} reserva(s)";
            }
            else
            {
                var reservasFiltradas = _reservas.Where(r =>
                    (r.Cliente != null && r.Cliente.Nombre.ToLower().Contains(filtro)) ||
                    (r.Evento != null && r.Evento.NombreEvento.ToLower().Contains(filtro)) ||
                    r.Estado.ToLower().Contains(filtro)
                ).ToList();

                GrdVw_Reservas.DataSource = null;
                GrdVw_Reservas.DataSource = reservasFiltradas;
                lbl_TotalReservas.Text = $"Mostrando: {reservasFiltradas.Count} de {_reservas.Count} reserva(s)";
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (cmb_Producto.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un producto.",
                              "Producto requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int productoId = (int)cmb_Producto.SelectedValue;
            var producto = _productos.FirstOrDefault(p => p.Id == productoId);
            
            if (producto == null) return;

            int cantidad = (int)nud_Cantidad.Value;

            // Verificar si el producto ya está en la lista
            var existente = _productosReserva.FirstOrDefault(p => p.ProductoId == productoId);
            int cantidadTotal = cantidad;
            
            if (existente != null)
            {
                cantidadTotal = existente.Cantidad + cantidad;
            }

            // VALIDAR STOCK DISPONIBLE
            if (producto.Stock < cantidadTotal)
            {
                MessageBox.Show(
                    $"Stock insuficiente para '{producto.Nombre}'.\n\n" +
                    $"Stock disponible: {producto.Stock}\n" +
                    $"Ya en reserva: {existente?.Cantidad ?? 0}\n" +
                    $"Intentando agregar: {cantidad}\n" +
                    $"Total requerido: {cantidadTotal}",
                    "Stock Insuficiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Si pasa la validación, agregar o actualizar
            if (existente != null)
            {
                existente.Cantidad = cantidadTotal;
                existente.Subtotal = existente.Cantidad * existente.PrecioUnitario;
                _productosReserva.ResetBindings();
            }
            else
            {
                _productosReserva.Add(new ReservaProductoItem
                {
                    ProductoId = productoId,
                    NombreProducto = producto.Nombre,
                    PrecioUnitario = producto.Precio,
                    Cantidad = cantidad,
                    Subtotal = producto.Precio * cantidad
                });
            }

            ActualizarTotal();
            nud_Cantidad.Value = 1;
        }

        private void btnQuitarProducto_Click(object sender, EventArgs e)
        {
            if (GrdVw_Productos.SelectedRows.Count > 0)
            {
                var item = (ReservaProductoItem)GrdVw_Productos.SelectedRows[0].DataBoundItem;
                _productosReserva.Remove(item);
                ActualizarTotal();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para quitar.",
                              "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ActualizarTotal()
        {
            decimal total = _productosReserva.Sum(p => p.Subtotal);
            lbl_TotalMonto.Text = total.ToString("C2");
        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            var authService = Program.ServiceProvider.GetRequiredService<IAuthService>();
            if (!await authService.HasPermissionAsync("reservas.agregar"))
            {
                MessageBox.Show("No tiene permisos para crear reservas.",
                               "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCampos()) return;

            if (!_productosReserva.Any())
            {
                MessageBox.Show("Debe agregar al menos un producto a la reserva.",
                              "Productos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que la fecha de finalización sea futura
            if (dtp_FechaReserva.Value <= DateTime.Now)
            {
                MessageBox.Show("La fecha de finalización debe ser posterior a hoy.",
                              "Fecha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var createRequest = new CreateReservaRequest
                {
                    ClienteId = (int)cmb_Cliente.SelectedValue!,
                    EventoId = (int)cmb_Evento.SelectedValue!,
                    FechaFinalizacion = dtp_FechaReserva.Value,
                    Estado = cmb_Estado.SelectedItem?.ToString() ?? "Pendiente",
                    Productos = _productosReserva.Select(p => new CreateReservaProductoRequest
                    {
                        ProductoId = p.ProductoId,
                        CantidadReservada = p.Cantidad
                    }).ToList()
                };

                await _reservaApiClient.CreateAsync(createRequest);

                MessageBox.Show("Reserva creada exitosamente.\n\nFecha de creación: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"), 
                              "Éxito",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
                await CargarReservasAsync();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Sesión Expirada",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear reserva: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txt_ID.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Por favor, seleccione una reserva válida para editar.",
                              "ID requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCampos()) return;

            if (!_productosReserva.Any())
            {
                MessageBox.Show("Debe agregar al menos un producto a la reserva.",
                              "Productos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var updateRequest = new UpdateReservaRequest
                {
                    Id = id,
                    ClienteId = (int)cmb_Cliente.SelectedValue!,
                    EventoId = (int)cmb_Evento.SelectedValue!,
                    FechaFinalizacion = dtp_FechaReserva.Value,
                    Estado = cmb_Estado.SelectedItem?.ToString() ?? "Pendiente",
                    Productos = _productosReserva.Select(p => new UpdateReservaProductoRequest
                    {
                        ProductoId = p.ProductoId,
                        CantidadReservada = p.Cantidad
                    }).ToList()
                };

                bool resultado = await _reservaApiClient.UpdateAsync(updateRequest);

                if (resultado)
                {
                    MessageBox.Show("Reserva actualizada exitosamente.", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarCampos();
                    await CargarReservasAsync();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la reserva.", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Sesión Expirada",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar reserva: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txt_ID.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Por favor, seleccione una reserva válida para eliminar.",
                              "ID requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("¿Está seguro que desea eliminar esta reserva?",
                                       "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool resultado = await _reservaApiClient.DeleteAsync(id);

                    if (resultado)
                    {
                        MessageBox.Show("Reserva eliminada exitosamente.", "Éxito",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LimpiarCampos();
                        await CargarReservasAsync();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la reserva.", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show(ex.Message, "Sesión Expirada",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar reserva: {ex.Message}",
                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void GrdVw_Reservas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var reserva = (ReservaDTO)GrdVw_Reservas.Rows[e.RowIndex].DataBoundItem;
                txt_ID.Text = reserva.Id.ToString();
                cmb_Cliente.SelectedValue = reserva.ClienteId;
                cmb_Evento.SelectedValue = reserva.EventoId;
                dtp_FechaReserva.Value = reserva.FechaFinalizacion;
                cmb_Estado.SelectedItem = reserva.Estado;

                // Cargar productos de la reserva
                _productosReserva.Clear();
                foreach (var prod in reserva.Productos)
                {
                    _productosReserva.Add(new ReservaProductoItem
                    {
                        ProductoId = prod.ProductoId,
                        NombreProducto = prod.NombreProducto,
                        PrecioUnitario = prod.PrecioUnitario,
                        Cantidad = prod.CantidadReservada,
                        Subtotal = prod.SubTotal
                    });
                }

                ActualizarTotal();
            }
        }

        private async void btnRefrescar_Click(object sender, EventArgs e)
        {
            await CargarReservasAsync();
            txt_Buscar.Clear();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private bool ValidarCampos()
        {
            if (cmb_Cliente.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un cliente.",
                              "Cliente requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmb_Evento.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un evento.",
                              "Evento requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmb_Estado.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un estado.",
                              "Estado requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            txt_ID.Clear();
            
            if (cmb_Cliente.Items.Count > 0)
                cmb_Cliente.SelectedIndex = 0;
            
            if (cmb_Evento.Items.Count > 0)
                cmb_Evento.SelectedIndex = 0;
            
            dtp_FechaReserva.Value = DateTime.Now.AddDays(1); // Resetear a mañana
            
            if (cmb_Estado.Items.Count > 0)
                cmb_Estado.SelectedIndex = 0;
            
            _productosReserva.Clear();
            ActualizarTotal();
            
            if (cmb_Producto.Items.Count > 0)
                cmb_Producto.SelectedIndex = 0;
            
            nud_Cantidad.Value = 1;
        }

        // Clase auxiliar para manejar productos en el DataGridView
        private class ReservaProductoItem
        {
            public int ProductoId { get; set; }
            public string NombreProducto { get; set; } = string.Empty;
            public decimal PrecioUnitario { get; set; }
            public int Cantidad { get; set; }
            public decimal Subtotal { get; set; }
        }
    }
}
