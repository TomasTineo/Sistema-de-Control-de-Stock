using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using API.Clients;
using DTOs.Categorias;

namespace Escritorio
{
    public partial class Form_Categorias : Form
    {
        private readonly CategoriaApiClient _categoriaApiClient;
        private List<CategoriaDTO> _categorias = new List<CategoriaDTO>();

        public Form_Categorias()
        {
            InitializeComponent();
            // Obtener el servicio desde el contenedor DI
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

            // Configurar columnas del DataGridView
            ConfigurarDataGridView();

            // Cargar productos desde la API
            await cargarCategoriaAsync();
        }

        private void ConfigurarDataGridView()
        {
            GrdVw_Categoria.AutoGenerateColumns = false;
            GrdVw_Categoria.Columns.Clear();

            var colId = new DataGridViewTextBoxColumn();
            colId.HeaderText = "ID";
            colId.DataPropertyName = "Id";
            GrdVw_Categoria.Columns.Add(colId);

            var colName = new DataGridViewTextBoxColumn();
            colName.HeaderText = "Nombre";
            colName.DataPropertyName = "Nombre";
            GrdVw_Categoria.Columns.Add(colName);

            var colDescription = new DataGridViewTextBoxColumn();
            colDescription.HeaderText = "Descripción";
            colDescription.DataPropertyName = "Descripcion";
            GrdVw_Categoria.Columns.Add(colDescription);

            var colPrice = new DataGridViewTextBoxColumn();
            colPrice.HeaderText = "Precio por unidad";
            colPrice.DataPropertyName = "Precio";
            colPrice.DefaultCellStyle.Format = "C2"; // Formato de moneda
            GrdVw_Categoria.Columns.Add(colPrice);

            var colStock = new DataGridViewTextBoxColumn();
            colStock.HeaderText = "Stock actual";
            colStock.DataPropertyName = "Stock";
            colStock.DefaultCellStyle.Format = "N0"; // Formato numérico sin decimales
            GrdVw_Categoria.Columns.Add(colStock);

            GrdVw_Categoria.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private async Task cargarCategoriaAsync()
        {
            try
            {
                _categorias = (await _categoriaApiClient.GetAllAsync()).ToList();
                GrdVw_Categoria.DataSource = null;
                GrdVw_Categoria.DataSource = _categorias;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Sesión Expirada", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
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
                var createRequest = new CreateCategoriaRequest
                {
                    Nombre = txt_Name.Text
                };

                await _categoriaApiClient.CreateAsync(createRequest);

                MessageBox.Show("Categoria creada exitosamente.", "Éxito", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar campos
                LimpiarCampos();

                // Recargar Categorias
                await cargarCategoriaAsync();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Sesión Expirada", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear categoria: {ex.Message}", 
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

        private void GrdVw_Categoria_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var categoria = (CategoriaDTO)GrdVw_Categoria.Rows[e.RowIndex].DataBoundItem;
                txt_ID.Text = categoria.Id.ToString();
                txt_Name.Text = categoria.Nombre;
              //  txt_Description.Text = categoria.Descripcion;
             //   txt_Price.Text = categoria.Precio.ToString();
              //  txt_Stock.Text = categoria.Stock.ToString();
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txt_ID.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Por favor, seleccione una  válido para editar.", 
                              "ID requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var updateRequest = new CategoriaDTO
                {
                    Id = id,
                    Nombre = txt_Name.Text,
                    
                };

                bool resultado = await _categoriaApiClient.UpdateAsync(updateRequest);

                if (resultado)
                {
                    MessageBox.Show("Categoria actualizada exitosamente.", "Éxito", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LimpiarCampos();
                    await cargarCategoriaAsync();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la categoria.", "Error", 
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
                MessageBox.Show($"Error al actualizar categoria: {ex.Message}", 
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txt_ID.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Por favor, seleccione una categoria válido para eliminar.", 
                              "ID requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("¿Está seguro que desea eliminar esta categoria?", 
                                       "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool resultado = await _categoriaApiClient.DeleteAsync(id);

                    if (resultado)
                    {
                        MessageBox.Show("Categoria eliminada exitosamente.", "Éxito", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LimpiarCampos();
                        await cargarCategoriaAsync();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la categoria.", "Error", 
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
                    MessageBox.Show($"Error al eliminar categoria: {ex.Message}", 
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

    }
}
