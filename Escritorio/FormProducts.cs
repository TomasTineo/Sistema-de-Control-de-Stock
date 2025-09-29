using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using API.Clients;
using DTOs;

namespace Escritorio
{
    public partial class FormProducts : Form
    {
        private readonly IProductoApiClient _productoApiClient;
        private List<ProductoDTO> _productos = new List<ProductoDTO>();

        public FormProducts()
        {
            InitializeComponent();
            // Obtener el servicio desde el contenedor DI
            _productoApiClient = Program.ServiceProvider.GetRequiredService<IProductoApiClient>();
        }

        private async void FormProducts_Load(object sender, EventArgs e)
        {
            // TextBox placeholders
            txt_ID.PlaceholderText = "Identificación";
            txt_Name.PlaceholderText = "Nombre";
            txt_Description.PlaceholderText = "Descripción";
            txt_Price.PlaceholderText = "Precio";
            txt_Stock.PlaceholderText = "Stock actual";

            // Configurar columnas del DataGridView
            ConfigurarDataGridView();

            // Cargar productos desde la API
            await CargarProductosAsync();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", 
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Name.Text) ||
                string.IsNullOrWhiteSpace(txt_Description.Text) || 
                string.IsNullOrWhiteSpace(txt_Price.Text) ||
                string.IsNullOrWhiteSpace(txt_Stock.Text))
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
                    CategoriaId = 1 // Por ahora hardcodeado, después se puede mejorar
                };

                await _productoApiClient.CreateAsync(createRequest);

                MessageBox.Show("Producto creado exitosamente.", "Éxito", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar campos
                LimpiarCampos();

                // Recargar productos
                await CargarProductosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear producto: {ex.Message}", 
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txt_ID.Clear();
            txt_Name.Clear();
            txt_Description.Clear();
            txt_Price.Clear();
            txt_Stock.Clear();
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
                var updateRequest = new UpdateProductoRequest
                {
                    Id = id,
                    Nombre = txt_Name.Text,
                    Precio = decimal.Parse(txt_Price.Text),
                    Descripcion = txt_Description.Text,
                    Stock = int.Parse(txt_Stock.Text),
                    CategoriaId = 1 // Por ahora hardcodeado
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
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar producto: {ex.Message}", 
                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Eventos existentes que no necesitan cambios
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged_1(object sender, EventArgs e) { }
        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e) { }
        private void productsInMemoryBindingSource_CurrentChanged(object sender, EventArgs e) { }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void txt_Price_TextChanged(object sender, EventArgs e) { }
        private void txt_Name_TextChanged(object sender, EventArgs e) { }
    }
}
