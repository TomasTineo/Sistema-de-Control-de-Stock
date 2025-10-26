using API.Clients;
using Domain.Model;
using DTOs.Categorias;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
            if (string.IsNullOrWhiteSpace(txt_Name.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos (excepto ID).", 
                              "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            txt_Name.Clear();
        }

        private void GrdVw_Categoria_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var categoria = (CategoriaDTO)GrdVw_Categoria.Rows[e.RowIndex].DataBoundItem;
                txt_ID.Text = categoria.Id.ToString();
                txt_Name.Text = categoria.Nombre;

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

         //Eventos existentes que no necesitan cambios
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged_1(object sender, EventArgs e) { }
        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e) { }
        private void productsInMemoryBindingSource_CurrentChanged(object sender, EventArgs e) { }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { } 

    }
}
