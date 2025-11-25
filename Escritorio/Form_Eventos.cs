using API.Clients;
using DTOs.Eventos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

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
            txt_ID.PlaceholderText = "Identificación";
            txt_Name.PlaceholderText = "Nombre del Evento";
            txt_Buscar.PlaceholderText = "Buscar evento por nombre...";

            // Evento de búsqueda
            txt_Buscar.TextChanged += txt_Buscar_TextChanged;
           

            dtp_FechaEvento.Format = DateTimePickerFormat.Short;
            dtp_FechaEvento.MinDate = DateTime.Today;
            dtp_FechaEvento.MaxDate = DateTime.Today.AddYears(2);
            dtp_FechaEvento.Value = DateTime.Today;

            ConfigurarDataGridView();
            await CargarEventosAsync();
        }

        //  Método de búsqueda
        private void txt_Buscar_TextChanged(object sender, EventArgs e)
        {
            FiltrarEventos();
        }

        private void FiltrarEventos()
        {
            string filtro = txt_Buscar.Text.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(filtro))
            {
                GrdVw_Evento.DataSource = null;
                GrdVw_Evento.DataSource = _eventos;
                lbl_TotalEventoos.Text = $"Total: {_eventos.Count} evento(s)";
            }
            else
            {
                var eventosFiltrados = _eventos.Where(e =>
                    e.NombreEvento.ToLower().Contains(filtro)
                ).ToList();

                GrdVw_Evento.DataSource = null;
                GrdVw_Evento.DataSource = eventosFiltrados;
                lbl_TotalEventoos.Text = $"Mostrando: {eventosFiltrados.Count} de {_eventos.Count} evento(s)";
            }
        }

        private void ConfigurarDataGridView()
        {
            GrdVw_Evento.AutoGenerateColumns = false;
            GrdVw_Evento.Columns.Clear();

            var colId = new DataGridViewTextBoxColumn();
            colId.HeaderText = "ID";
            colId.DataPropertyName = "Id";
            colId.Width = 50;
            GrdVw_Evento.Columns.Add(colId);

            var colName = new DataGridViewTextBoxColumn();
            colName.HeaderText = "Nombre del Evento";
            colName.DataPropertyName = "NombreEvento";
            GrdVw_Evento.Columns.Add(colName);

            // Columna de fecha
            var colFecha = new DataGridViewTextBoxColumn();
            colFecha.HeaderText = "Fecha";
            colFecha.DataPropertyName = "FechaEvento";
            colFecha.DefaultCellStyle.Format = "dd/MM/yyyy";
            colFecha.Width = 100;
            GrdVw_Evento.Columns.Add(colFecha);

            GrdVw_Evento.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private async Task CargarEventosAsync()
        {
            try
            {
                _eventos = (await _eventoApiClient.GetAllAsync()).ToList();
                GrdVw_Evento.DataSource = null;
                GrdVw_Evento.DataSource = _eventos;

                // Actualizar total
                lbl_TotalEventoos.Text = $"Total: {_eventos.Count} evento(s)";
            }
            catch (UnauthorizedAccessException ex)
            {
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
                MessageBox.Show($"Error al cargar eventos: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_agregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Name.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre del evento.",
                              "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var createRequest = new CreateEventoRequest
                {
                    NombreEvento = txt_Name.Text,
                    FechaEvento = dtp_FechaEvento.Value  // USAR LA FECHA SELECCIONADA
                };

                await _eventoApiClient.CreateAsync(createRequest);

                MessageBox.Show("Evento creado exitosamente.", "Éxito",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
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

        private void GrdVw_Evento_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var evento = (EventoDTO)GrdVw_Evento.Rows[e.RowIndex].DataBoundItem;
                txt_ID.Text = evento.Id.ToString();
                txt_Name.Text = evento.NombreEvento;
                dtp_FechaEvento.Value = evento.FechaEvento;  // CARGAR LA FECHA
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
                    NombreEvento = txt_Name.Text,
                    FechaEvento = dtp_FechaEvento.Value  // USAR LA FECHA SELECCIONADA
                };

                bool resultado = await _eventoApiClient.UpdateAsync(updateRequest);

                if (resultado)
                {
                    MessageBox.Show("Evento actualizado exitosamente.", "Éxito",
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
                        MessageBox.Show("Evento eliminado exitosamente.", "Éxito",
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

        private async void btnRefrescar_Click(object sender, EventArgs e)
        {
            await CargarEventosAsync();
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
            txt_Buscar.Clear();  // Limpiar campo de búsqueda
            dtp_FechaEvento.Value = DateTime.Now;  // RESETEAR A HOY
            txt_Name.Focus();
        }

        // Eventos existentes que no necesitan cambios
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void txt_Name_TextChanged(object sender, EventArgs e) { }

        private void dtp_FechaEvento_ValueChanged(object sender, EventArgs e)
        {

        }

     
    }
}
