using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using API.Clients;
using DTOs.Productos;
using DTOs.Categorias;

namespace Escritorio
{
    public partial class Form_Productos : Form
    {
        private readonly ProductoApiClient _productoApiClient;
        private readonly CategoriaApiClient _categoriaApiClient;
        private List<ProductoDTO> _productos = new List<ProductoDTO>();
        private List<CategoriaDTO> _categorias = new List<CategoriaDTO>();

        public Form_Productos()
        {
            InitializeComponent();
            // Obtener el servicio desde el contenedor DI
            _productoApiClient = Program.ServiceProvider.GetRequiredService<ProductoApiClient>();
            _categoriaApiClient = Program.ServiceProvider.GetRequiredService<CategoriaApiClient>();
        }

        private async void FormProducts_Load(object sender, EventArgs e)
        {
            // TextBox placeholders
            txt_ID.PlaceholderText = "Identificación";
            txt_Name.PlaceholderText = "Nombre";
            txt_Description.PlaceholderText = "Descripción";
            txt_Price.PlaceholderText = "Precio";
            txt_Stock.PlaceholderText = "Stock actual";
            txt_Buscar.PlaceholderText = "Buscar por nombre o descripción...";

            //Evento de búsqueda en tiempo real
            txt_Buscar.TextChanged += txt_Buscar_TextChanged;

            // Configurar columnas del DataGridView
            ConfigurarDataGridView();

            // Cargar categorías antes de los productos
            await CargarCategoriasAsync();

            // Cargar productos desde la API
            await CargarProductosAsync();

            // Aplicar permisos
            await ConfigurarPermisos();
        }

        private async Task ConfigurarPermisos()
        {
            var authService = AuthServiceProvider.Instance;

            // Verificar permisos con los nombres CORRECTOS de la BD (minúsculas)
            btn_agregar.Enabled = await authService.HasPermissionAsync("productos.agregar");
            btn_Editar.Enabled = await authService.HasPermissionAsync("productos.actualizar");
            btn_Borrar.Enabled = await authService.HasPermissionAsync("productos.eliminar");

            // Aplicar estilo visual a botones deshabilitados
            if (!btn_agregar.Enabled)
            {
                btn_agregar.Text = "🔒 " + btn_agregar.Text;
                btn_agregar.ForeColor = Color.Gray;
            }
            
            if (!btn_Editar.Enabled)
            {
                btn_Editar.Text = "🔒 " + btn_Editar.Text;
                btn_Editar.ForeColor = Color.Gray;
            }
            
            if (!btn_Borrar.Enabled)
            {
                btn_Borrar.Text = "🔒 " + btn_Borrar.Text;
                btn_Borrar.ForeColor = Color.Gray;
            }
        }

        private void txt_Buscar_TextChanged(object sender, EventArgs e)
        {
            FiltrarProductos();
        }

        private void FiltrarProductos()
        {
            string filtro = txt_Buscar.Text.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(filtro))
            {
                // Mostrar todos los productos
                GrdVw_Product.DataSource = null;
                GrdVw_Product.DataSource = _productos;
                lbl_TotalProductos.Text = $"Total: {_productos.Count} producto(s)";
            }
            else
            {
                // Filtrar por nombre o descripción
                var productosFiltrados = _productos.Where(p =>
                    p.Nombre.ToLower().Contains(filtro) ||
                    p.Descripcion.ToLower().Contains(filtro)
                ).ToList();

                GrdVw_Product.DataSource = null;
                GrdVw_Product.DataSource = productosFiltrados;
                lbl_TotalProductos.Text = $"Mostrando: {productosFiltrados.Count} de {_productos.Count} producto(s)";
            }
        }

        private void ConfigurarDataGridView()
        {
            GrdVw_Product.AutoGenerateColumns = false;
            GrdVw_Product.Columns.Clear();

            var colId = new DataGridViewTextBoxColumn();
            colId.HeaderText = "ID";
            colId.DataPropertyName = "Id";
            GrdVw_Product.Columns.Add(colId);

            var colName = new DataGridViewTextBoxColumn();
            colName.HeaderText = "Nombre";
            colName.DataPropertyName = "Nombre";
            GrdVw_Product.Columns.Add(colName);

            var colDescription = new DataGridViewTextBoxColumn();
            colDescription.HeaderText = "Descripción";
            colDescription.DataPropertyName = "Descripcion";
            GrdVw_Product.Columns.Add(colDescription);

            var colPrice = new DataGridViewTextBoxColumn();
            colPrice.HeaderText = "Precio por unidad";
            colPrice.DataPropertyName = "Precio";
            colPrice.DefaultCellStyle.Format = "C2"; // Formato de moneda
            GrdVw_Product.Columns.Add(colPrice);

            var colStock = new DataGridViewTextBoxColumn();
            colStock.HeaderText = "Stock actual";
            colStock.DataPropertyName = "Stock";
            colStock.DefaultCellStyle.Format = "N0"; // Formato numérico sin decimales
            GrdVw_Product.Columns.Add(colStock);

            GrdVw_Product.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private async Task CargarProductosAsync()
        {
            try
            {
                _productos = (await _productoApiClient.GetAllAsync()).ToList();
                GrdVw_Product.DataSource = null;
                GrdVw_Product.DataSource = _productos;
                lbl_TotalProductos.Text = $"Total: {_productos.Count} producto(s)";
            }
            catch (UnauthorizedAccessException ex)
            {

                Console.WriteLine("Error de auth");
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
                Console.WriteLine($"Tipo de excepción: {ex.GetType().Name}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");

                MessageBox.Show(
                    $"Error al cargar productos:\n\n" +
                    $"Tipo: {ex.GetType().Name}\n" +
                    $"Mensaje: {ex.Message}\n\n" +
                    $"Ver consola para más detalles.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task CargarCategoriasAsync()
        {
            try
            {
                _categorias = (await _categoriaApiClient.GetAllAsync()).ToList();

                // Configurar el ComboBox
                cmb_Categoria.DataSource = null;
                cmb_Categoria.DataSource = _categorias;
                cmb_Categoria.DisplayMember = "Nombre";  // Lo que se muestra
                cmb_Categoria.ValueMember = "Id";        // El valor interno

                // Seleccionar el primer elemento por defecto
                if (_categorias.Any())
                {
                    cmb_Categoria.SelectedIndex = 0;
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
                MessageBox.Show($"Error al cargar categorías: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            var authService = Program.ServiceProvider.GetRequiredService<IAuthService>();
            if (!await authService.HasPermissionAsync("productos.agregar"))
            {
                MessageBox.Show("No tiene permisos para crear productos.",
                               "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_Name.Text) ||
                string.IsNullOrWhiteSpace(txt_Description.Text) ||
                string.IsNullOrWhiteSpace(txt_Price.Text) ||
                string.IsNullOrWhiteSpace(txt_Stock.Text) ||
                cmb_Categoria.SelectedValue == null)
            {
                MessageBox.Show("Por favor, complete todos los campos (excepto ID).",
                              "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txt_Price.Text, out decimal precio) || precio < 0)
            {
                MessageBox.Show("El precio debe ser un valor numérico positivo.",
                              "Precio inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txt_Stock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("El stock debe ser un valor numérico positivo.",
                              "Stock inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var createRequest = new CreateProductoRequest
                {
                    Nombre = txt_Name.Text,
                    Precio = precio,
                    Descripcion = txt_Description.Text,
                    Stock = stock,
                    CategoriaId = (int)cmb_Categoria.SelectedValue
                };

                await _productoApiClient.CreateAsync(createRequest);

                MessageBox.Show("Producto creado exitosamente.", "Éxito",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar campos
                LimpiarCampos();

                // Recargar productos
                await CargarProductosAsync();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Sesión Expirada",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear producto: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void GrdVw_Product_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var producto = (ProductoDTO)GrdVw_Product.Rows[e.RowIndex].DataBoundItem;
                txt_ID.Text = producto.Id.ToString();
                txt_Name.Text = producto.Nombre;
                txt_Description.Text = producto.Descripcion;
                txt_Price.Text = producto.Precio.ToString();
                txt_Stock.Text = producto.Stock.ToString();
                cmb_Categoria.SelectedValue = producto.CategoriaId;
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txt_ID.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Por favor, seleccione un producto válido para editar.",
                              "ID requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var updateRequest = new ProductoDTO
                {
                    Id = id,
                    Nombre = txt_Name.Text,
                    Precio = decimal.Parse(txt_Price.Text),
                    Descripcion = txt_Description.Text,
                    Stock = int.Parse(txt_Stock.Text),
                    CategoriaId = (int)cmb_Categoria.SelectedValue
                };

                bool resultado = await _productoApiClient.UpdateAsync(updateRequest);

                if (resultado)
                {
                    MessageBox.Show("Producto actualizado exitosamente.", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarCampos();
                    await CargarProductosAsync();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el producto.", "Error",
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
                MessageBox.Show($"Error al actualizar producto: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txt_ID.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Por favor, seleccione un producto válido para eliminar.",
                              "ID requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("¿Está seguro que desea eliminar este producto?",
                                       "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool resultado = await _productoApiClient.DeleteAsync(id);

                    if (resultado)
                    {
                        MessageBox.Show("Producto eliminado exitosamente.", "Éxito",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LimpiarCampos();
                        await CargarProductosAsync();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el producto.", "Error",
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
                    MessageBox.Show($"Error al eliminar producto: {ex.Message}",
                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        public async void btnRefrescarGrilla_Click(object sender, EventArgs e)
        {
            await CargarProductosAsync();
            txt_Buscar.Clear();
        }



        public async void btnLimpiarCampos_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            await CargarProductosAsync();
        }

        private void LimpiarCampos()
        {
            txt_ID.Clear();
            txt_Name.Clear();
            txt_Description.Clear();
            txt_Price.Clear();
            txt_Stock.Clear();

            txt_ID.PlaceholderText = "Identificación";
            txt_Name.PlaceholderText = "Nombre";
            txt_Description.PlaceholderText = "Descripción";
            txt_Price.PlaceholderText = "Precio";
            txt_Stock.PlaceholderText = "Stock actual";

            if (cmb_Categoria.Items.Count > 0)
            {
                cmb_Categoria.SelectedIndex = 0;
            }

            txt_Name.Focus();
        }


 


        // Eventos existentes que no necesitan cambios
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void productsInMemoryBindingSource_CurrentChanged(object sender, EventArgs e) { }
        private void txt_Price_TextChanged(object sender, EventArgs e) { }
        private void txt_Name_TextChanged(object sender, EventArgs e) { }

      
    }
}
