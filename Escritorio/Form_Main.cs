using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;


namespace Escritorio
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();
            
            // Validación antes de cerrar el formulario
            this.FormClosing += Form_Main_FormClosing;
            
            // Cuando se cierre Form_Main, cerrar la aplicación completa
            this.FormClosed += (s, e) => Application.Exit();
        }

        private void Form_Main_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Solo mostrar confirmación si el usuario está cerrando manualmente
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show(
                    "¿Está seguro que desea cerrar la aplicación?\n\nSe cerrará la sesión actual.",
                    "Confirmar cierre",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (result == DialogResult.No)
                {
                    e.Cancel = true; // Cancelar el cierre
                }
            }
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            await ConfigurarPermisos();
        }

        private async Task ConfigurarPermisos()
        {
            var authService = AuthServiceProvider.Instance;

            // Verificar permisos
            btn_Productos.Enabled = await authService.HasPermissionAsync("productos.leer");
            btn_Categorias.Enabled = await authService.HasPermissionAsync("categorias.leer");
            btn_Clientes.Enabled = await authService.HasPermissionAsync("clientes.leer");
            btn_Eventos.Enabled = await authService.HasPermissionAsync("eventos.leer");
            btn_Reservas.Enabled = await authService.HasPermissionAsync("reservas.leer");

            // Aplicar estilo visual a botones deshabilitados
            AplicarEstiloSinPermiso(btn_Productos);
            AplicarEstiloSinPermiso(btn_Categorias);
            AplicarEstiloSinPermiso(btn_Clientes);
            AplicarEstiloSinPermiso(btn_Eventos);
            AplicarEstiloSinPermiso(btn_Reservas);
        }

        private void AplicarEstiloSinPermiso(Button button)
        {
            if (!button.Enabled)
            {
                button.ForeColor = Color.Gray;
                button.Cursor = Cursors.No;
                button.Text = "🔒 " + button.Text;
            }
            else
            {
                button.Cursor = Cursors.Hand;
            }
        }

        private void buttonProducts_Click(object sender, EventArgs e)
        {
            var formProducts = Program.ServiceProvider.GetRequiredService<Form_Productos>();
            formProducts.Show();
        }
        
        private void btnCategorias_Click(object sender, EventArgs e)
        {
            var formCategorias = Program.ServiceProvider.GetRequiredService<Form_Categorias>();
            formCategorias.Show();
        }

        private void btnEventos_Click(object sender, EventArgs e)
        {
            var formEventos = Program.ServiceProvider.GetRequiredService<Form_Eventos>();
            formEventos.Show();
        }

        private void btnReservas_Click(object sender, EventArgs e)
        {
            var formReservas = Program.ServiceProvider.GetRequiredService<Form_Reserva>();
            formReservas.Show();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            var formClientes = Program.ServiceProvider.GetRequiredService<Form_Clientes>();
            formClientes.Show();
        }

    }
}
