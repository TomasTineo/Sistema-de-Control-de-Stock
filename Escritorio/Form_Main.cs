using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing;
using System.Windows.Forms;


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

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Configuración inicial si es necesaria
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
            MessageBox.Show("Módulo de Reservas en desarrollo.", 
                          "Próximamente", 
                          MessageBoxButtons.OK, 
                          MessageBoxIcon.Information);
        }

    }
}
