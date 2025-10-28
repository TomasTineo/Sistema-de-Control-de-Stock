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

        // Efectos hover para los botones
        private void btn_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Font = new Font(btn.Font.FontFamily, btn.Font.Size + 1, btn.Font.Style);
            }
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Font = new Font(btn.Font.FontFamily, 14F, FontStyle.Bold);
            }
        }
    }
}
