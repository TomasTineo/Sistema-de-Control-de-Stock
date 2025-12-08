using API.Clients;
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
            txt_Name.PlaceholderText = "Nombre de la Categoría";
            txt_Buscar.PlaceholderText = "🔍 Buscar categoría por nombre...";

            // Evento de búsqueda
            txt_Buscar.TextChanged += txt_Buscar_TextChanged;

            // Configurar columnas del DataGridView
            ConfigurarDataGridView();

            // Cargar categorias desde la API
            await cargarCategoriaAsync();

            // Aplicar permisos
            await ConfigurarPermisos();
        }

        private async Task ConfigurarPermisos()
        {
            var authService = AuthServiceProvider.Instance;

            // Verificar permisos 
            btn_agregar.Enabled = await authService.HasPermissionAsync("categorias.agregar");
            btn_Editar.Enabled = await authService.HasPermissionAsync("categorias.actualizar");
            btn_Borrar.Enabled = await authService.HasPermissionAsync("categorias.eliminar");

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

        private void ConfigurarDataGridView()
        {
            GrdVw_Categoria.AutoGenerateColumns = false;
            GrdVw_Categoria.Columns.Clear();

            var colId = new DataGridViewTextBoxColumn();
            colId.HeaderText = "ID";
            colId.DataPropertyName = "Id";
            colId.Width = 50;
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
                lbl_TotalCategorias.Text = $"Total: {_categorias.Count} categoría(s)";
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Sesión Expirada", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar categorías: {ex.Message}", 
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Búsqueda en tiempo real
        private void txt_Buscar_TextChanged(object sender, EventArgs e)
        {
            FiltrarCategorias();
        }

        private void FiltrarCategorias()
        {
            string filtro = txt_Buscar.Text.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(filtro))
            {
                GrdVw_Categoria.DataSource = null;
                GrdVw_Categoria.DataSource = _categorias;
                lbl_TotalCategorias.Text = $"Total: {_categorias.Count} categoría(s)";
            }
            else
            {
                var categoriasFiltradas = _categorias.Where(c =>
                    c.Nombre.ToLower().Contains(filtro)
                ).ToList();

                GrdVw_Categoria.DataSource = null;
                GrdVw_Categoria.DataSource = categoriasFiltradas;
                lbl_TotalCategorias.Text = $"Mostrando: {categoriasFiltradas.Count} de {_categorias.Count} categoría(s)";
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var authService = Program.ServiceProvider.GetRequiredService<IAuthService>();
            if (!await authService.HasPermissionAsync("categorias.agregar"))
            {
                MessageBox.Show("No tiene permisos para crear categorías.",
                               "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_Name.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre de la categoría.", 
                              "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var createRequest = new CreateCategoriaRequest
                {
                    Nombre = txt_Name.Text
                };

                await _categoriaApiClient.CreateAsync(createRequest);

                MessageBox.Show("Categoría creada exitosamente.", "Éxito", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
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
                MessageBox.Show($"Error al crear categoría: {ex.Message}", 
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                MessageBox.Show("Por favor, seleccione una categoría válida para editar.", 
                              "ID requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var updateRequest = new CategoriaDTO
                {
                    Id = id,
                    Nombre = txt_Name.Text
                };

                bool resultado = await _categoriaApiClient.UpdateAsync(updateRequest);

                if (resultado)
                {
                    MessageBox.Show("Categoría actualizada exitosamente.", "Éxito", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LimpiarCampos();
                    await cargarCategoriaAsync();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la categoría.", "Error", 
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
                MessageBox.Show($"Error al actualizar categoría: {ex.Message}", 
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txt_ID.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Por favor, seleccione una categoría válida para eliminar.", 
                              "ID requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("¿Está seguro que desea eliminar esta categoría?", 
                                       "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool resultado = await _categoriaApiClient.DeleteAsync(id);

                    if (resultado)
                    {
                        MessageBox.Show("Categoría eliminada exitosamente.", "Éxito", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LimpiarCampos();
                        await cargarCategoriaAsync();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la categoría.", "Error", 
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
                    MessageBox.Show($"Error al eliminar categoría: {ex.Message}", 
                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnRefrescar_Click(object sender, EventArgs e)
        {
            await cargarCategoriaAsync();
            txt_Buscar.Clear();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txt_ID.Clear();
            txt_Name.Clear();
            txt_Name.Focus();
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
