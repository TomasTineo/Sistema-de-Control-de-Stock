using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using API.Clients;
using DTOs.Eventos;

namespace Escritorio
{
    public partial class Form_Eventos : Form
    {
        private readonly EventoApiClient _eventoApiClient;
        private List<EventoDTO> _eventos = new List<EventoDTO>();

        public Form_Eventos()
        {
            InitializeComponent();
            // Obtener el servicio desde el contenedor DI
            _eventoApiClient = Program.ServiceProvider.GetRequiredService<EventoApiClient>();
        }

        private async void Form_Eventos_Load(object sender, EventArgs e)
        {
            // TextBox placeholders
            txt_ID.PlaceholderText = "Identificación";
            txt_Name.PlaceholderText = "Nombre";

            // Configurar columnas del DataGridView
            ConfigurarDataGridView();

            // Cargar eventos desde la API
            await CargarEventosAsync();
        }

        private void ConfigurarDataGridView()
        {
            GrdVw_Evento.AutoGenerateColumns = false;
            GrdVw_Evento.Columns.Clear();

            var colId = new DataGridViewTextBoxColumn();
            colId.HeaderText = "ID";
            colId.DataPropertyName = "Id";
            GrdVw_Evento.Columns.Add(colId);

            var colName = new DataGridViewTextBoxColumn();
            colName.HeaderText = "NombreEvento";
            colName.DataPropertyName = "NombreEvento";
            GrdVw_Evento.Columns.Add(colName);

            GrdVw_Evento.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private async Task CargarEventosAsync()
        {
            try
            {
                _eventos = (await _eventoApiClient.GetAllAsync()).ToList();
                GrdVw_Evento.DataSource = null;
                GrdVw_Evento.DataSource = _eventos;
            }
            catch (UnauthorizedAccessException ex)
            {

                Console.WriteLine("Error de auth");
                MessageBox.Show(ex.Message, "No puede ingresar por falta de autorización.",
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
                    $"Error al cargar eventos:\n\n" +
                    $"Tipo: {ex.GetType().Name}\n" +
                    $"Mensaje: {ex.Message}\n\n" +
                    $"Ver consola para más detalles.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void btn_agregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Name.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos (excepto ID).",
                              "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            try
            {
                var createRequest = new CreateEventoRequest
                {
                    NombreEvento = txt_Name.Text,
                    FechaEvento = DateTime.Now,                               // TIENE QUE IR LA FECHA INGRESADA POR EL CALENDARIO

                };

                await _eventoApiClient.CreateAsync(createRequest);

                MessageBox.Show("Eventoo creado exitosamente.", "Éxito",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar campos
                LimpiarCampos();

                // Recargar eventos
                await CargarEventosAsync();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Sesión Expirada",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear evento: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txt_ID.Clear();
            txt_Name.Clear();
        }

        private void GrdVw_Evento_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var evento = (EventoDTO)GrdVw_Evento.Rows[e.RowIndex].DataBoundItem;
                txt_ID.Text = evento.Id.ToString();
                txt_Name.Text = evento.NombreEvento;
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txt_ID.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Por favor, seleccione un evento válido para editar.",
                              "ID requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var updateRequest = new EventoDTO
                {
                    Id = id,
                    NombreEvento = txt_Name.Text

                };

                bool resultado = await _eventoApiClient.UpdateAsync(updateRequest);

                if (resultado)
                {
                    MessageBox.Show("Eventoo actualizado exitosamente.", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarCampos();
                    await CargarEventosAsync();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el evento.", "Error",
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
                MessageBox.Show($"Error al actualizar evento: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txt_ID.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Por favor, seleccione un evento válido para eliminar.",
                              "ID requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("¿Está seguro que desea eliminar este evento?",
                                       "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool resultado = await _eventoApiClient.DeleteAsync(id);

                    if (resultado)
                    {
                        MessageBox.Show("Eventoo eliminado exitosamente.", "Éxito",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LimpiarCampos();
                        await CargarEventosAsync();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el evento.", "Error",
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
                    MessageBox.Show($"Error al eliminar evento: {ex.Message}",
                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Eventos existentes que no necesitan cambios
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged_1(object sender, EventArgs e) { }
        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e) { }
        private void eventosInMemoryBindingSource_CurrentChanged(object sender, EventArgs e) { }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void txt_Price_TextChanged(object sender, EventArgs e) { }
        private void txt_Name_TextChanged(object sender, EventArgs e) { }

    }
}
