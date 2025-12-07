using API.Clients;
using Domain.Model;
using DTOs.Clientes;
using DTOs.Productos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Escritorio
{
    public partial class Form_Clientes : Form
    {

        private readonly ClienteApiClient _clienteApiClient;
        private List<ClienteDTO> _clientes = new List<ClienteDTO>();
        private List<ClienteDTO> _clientesFiltrados = new List<ClienteDTO>();


        public Form_Clientes()
        {
            InitializeComponent();
            _clienteApiClient = Program.ServiceProvider.GetRequiredService<ClienteApiClient>();
        }

        private async void Form_Clientes_Load(object sender, EventArgs e)
        {
            //placeholders
            txtId.PlaceholderText = "ID (automático)";
            txtNombre.PlaceholderText = "Nombre del cliente";
            txtApellido.PlaceholderText = "Apellido del cliente";
            txtEmail.PlaceholderText = "email@ejemplo.com";
            txtTelefono.PlaceholderText = "+54 XXX XXXXXXX";
            txtDireccion.PlaceholderText = "Dirección del cliente";
            txtBuscar.PlaceholderText = "Buscar por nombre, apellido o email";

            // busqueda en tiempo real
            txtBuscar.TextChanged += txtBuscar_TextChanged;



            ConfigurarDataGridView();
            await CargarClientes();
        }

        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            BuscarClientes();
        }

        private void ConfigurarDataGridView()
        {
            dgvClientes.AutoGenerateColumns = false;
            dgvClientes.Columns.Clear();

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                Name = "Id",
                Width = 50
            });

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nombre",
                HeaderText = "Nombre",
                Name = "Nombre",
                Width = 150
            });

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Apellido",
                HeaderText = "Apellido",
                Name = "Apellido",
                Width = 150
            });

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email",
                Name = "Email",
                Width = 200
            });

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Telefono",
                HeaderText = "Teléfono",
                Name = "Telefono",
                Width = 120
            });

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Direccion",
                HeaderText = "Dirección",
                Name = "Direccion",
                Width = 250
            });

            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private async Task CargarClientes()
        {
            try
            {
                _clientes = (await _clienteApiClient.GetAllAsync()).ToList();
                _clientesFiltrados = _clientes.ToList();
                ActualizarDataGridView();
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
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarDataGridView()
        {
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = _clientesFiltrados;

            if (dgvClientes.Rows.Count > 0)
            {
                dgvClientes.ClearSelection();
            }
        }

        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var cliente = (ClienteDTO)dgvClientes.Rows[e.RowIndex].DataBoundItem;
                txtId.Text = cliente.Id.ToString();
                txtNombre.Text = cliente.Nombre;
                txtApellido.Text = cliente.Apellido;
                txtEmail.Text = cliente.Email;
                txtTelefono.Text = cliente.Telefono;
                txtDireccion.Text = cliente.Direccion;
            }
        }



        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
                return;
            try
            {
                var request = new CreateClienteRequest
                {
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim()
                };

                await _clienteApiClient.CreateAsync(request);

                MessageBox.Show("Cliente agregado exitosamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarFormulario();
                await CargarClientes();

            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Falta de autorización.",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar cliente: {ex.Message}", "Error",
     MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Seleccione un cliente para modificar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidarFormulario())
                return;
            try
            {
                var confirmResult = MessageBox.Show("¿Está seguro de modificar este cliente?",
                    "Confirmar Modificación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                    return;

                var request = new UpdateClienteRequest
                {
                    Id = int.Parse(txtId.Text),
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim()
                };

                bool resultado = await _clienteApiClient.UpdateAsync(request);

                if (resultado)
                {
                    MessageBox.Show("Cliente modificado exitosamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    await CargarClientes();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el cliente.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Falta de autorización.",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar cliente: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);



            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Seleccione un cliente para eliminar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var confirmResult = MessageBox.Show(
                    $"¿Está seguro que desea eliminar al cliente '{txtNombre.Text} {txtApellido.Text}'?\n\nEsta acción no se puede deshacer.",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmResult != DialogResult.Yes)
                    return;

                int clienteId = int.Parse(txtId.Text);
                bool resultado = await _clienteApiClient.DeleteAsync(clienteId);

                if (resultado)
                {
                    MessageBox.Show("Cliente eliminado exitosamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarFormulario();
                    await CargarClientes();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el cliente. Puede que tenga reservas asociadas.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Falta de autorización.",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar cliente: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarClientes();
        }

        private void BuscarClientes()
        {
            string textoBusqueda = txtBuscar.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                _clientesFiltrados = _clientes.ToList();
            }
            else
            {
                _clientesFiltrados = _clientes
                    .Where(c =>
                        c.Nombre.ToLower().Contains(textoBusqueda) ||
                        c.Apellido.ToLower().Contains(textoBusqueda) ||
                        c.Email.ToLower().Contains(textoBusqueda))
                    .ToList();
            }

            ActualizarDataGridView();

            if (!string.IsNullOrWhiteSpace(textoBusqueda) && _clientesFiltrados.Count == 0)
            {
                MessageBox.Show("No se encontraron clientes que coincidan con la búsqueda.",
                    "Sin Resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LimpiarFormulario()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtBuscar.Clear();
            dgvClientes.ClearSelection();

            txtId.PlaceholderText = "ID (automático)";
            txtNombre.PlaceholderText = "Nombre del cliente";
            txtApellido.PlaceholderText = "Apellido del cliente";
            txtEmail.PlaceholderText = "email@ejemplo.com";
            txtTelefono.PlaceholderText = "+54 XXX XXXXXXX";
            txtDireccion.PlaceholderText = "Dirección del cliente";

            _clientesFiltrados = _clientes.ToList();
            ActualizarDataGridView();

            txtNombre.Focus();
        }

        private bool ValidarFormulario()
        {
            // Validar Nombre
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            // Validar Apellido
            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El apellido es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }

            // Validar Email
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("El email es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("El email no tiene un formato válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Validar Teléfono
            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("El teléfono es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return false;
            }

            // Validar Dirección
            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("La dirección es obligatoria.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccion.Focus();
                return false;
            }

            return true;
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel_Superior_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}