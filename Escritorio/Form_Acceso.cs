
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace Escritorio
{
    public partial class Form_Acceso : Form
    {
        public Form_Acceso()
        {
            InitializeComponent();
        }

        private void btn_register_click(object sender, EventArgs e)
        {
            var form_registro = Program.ServiceProvider.GetRequiredService<Form_Registro>();
            form_registro.Show();
        }

        private void btn_login_click(object sender, EventArgs e)
        {
            var form_login = Program.ServiceProvider.GetRequiredService<Form_Login>();
            form_login.Show();
        }               

        private void Form_Acceso_Load(object sender, EventArgs e)
        {
            // Configuraciones iniciales
        }
    }
}
