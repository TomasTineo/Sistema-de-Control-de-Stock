using API.Clients;
using DTOs.Eventos;
using Microsoft.Extensions.DependencyInjection;


namespace Escritorio
{
    public partial class Form_Eventos : Form
    {
        private readonly EventoApiClient _eventoApiClient;
        private List<EventoDTO> _eventos = new List<EventoDTO>();

        public Form_Eventos()
        {
            InitializeComponent();
          
            _eventoApiClient = Program.ServiceProvider.GetRequiredService<EventoApiClient>();
        }

        private async void FormEventos_load(object sender, EventArgs e)
        {
            // TextBox placeholders
            txt_ID.PlaceholderText = "Identificación";
            txt_Name.PlaceholderText = "Nombre";
            
           

            // Configurar columnas del DataGridView
            ConfigurarDataGridView();

            // Cargar productos desde la API
            await cargarEventoAsync();
        }

        private void ConfigurarDataGridView()
        {
            GrdVw_Evento.AutoGenerateColumns = false;
            GrdVw_Evento.Columns.Clear();

            Evento_ID.HeaderText = "ID";
            Evento_ID.DataPropertyName = "Id";
            GrdVw_Evento.Columns.Add(Evento_ID);

            Evento_Name.HeaderText = "Nombre";
            Evento_Name.DataPropertyName = "Nombre";
            GrdVw_Evento.Columns.Add(Evento_Name);

            Evento_Fecha.HeaderText = "Fecha";
            Evento_Fecha.DataPropertyName = "Fecha"; // **ENLACE DE DATOS**
            GrdVw_Evento.Columns.Add(Evento_Fecha);

            GrdVw_Evento.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private async Task cargarEventoAsync()
        {
            try
            {
                _eventos = (await _eventoApiClient.GetAllAsync()).ToList();
                GrdVw_Evento.DataSource = null;
                GrdVw_Evento.DataSource = _eventos;
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
                var createRequest = new CreateEventoRequest
                {
                    NombreEvento = txt_Name.Text
                };

                await _eventoApiClient.CreateAsync(createRequest);

                MessageBox.Show("Evento creada exitosamente.", "Éxito", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar campos
                LimpiarCampos();

                // Recargar Eventos
                await cargarEventoAsync();
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
                MessageBox.Show("Por favor, seleccione una  válido para editar.", 
                              "ID requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var updateRequest = new EventoDTO
                {
                    Id = id,
                    NombreEvento = txt_Name.Text,
                    FechaEvento = DateTime.Now

                };

                bool resultado = await _eventoApiClient.UpdateAsync(updateRequest);

                if (resultado)
                {
                    MessageBox.Show("Evento actualizada exitosamente.", "Éxito", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LimpiarCampos();
                    await cargarEventoAsync();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la evento.", "Error", 
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
                MessageBox.Show("Por favor, seleccione una evento válido para eliminar.", 
                              "ID requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("¿Está seguro que desea eliminar esta evento?", 
                                       "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool resultado = await _eventoApiClient.DeleteAsync(id);

                    if (resultado)
                    {
                        MessageBox.Show("Evento eliminada exitosamente.", "Éxito", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LimpiarCampos();
                        await cargarEventoAsync();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la evento.", "Error", 
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

         //Eventos existentes que no necesitan cambios
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged_1(object sender, EventArgs e) { }
        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e) { }
        private void eventosInMemoryBindingSource_CurrentChanged(object sender, EventArgs e) { }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { } 

    }
}
