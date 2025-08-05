using Sistema_Stock;
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
    public partial class Form_Acceso : Form
    {
        public Form_Acceso()
        {
            InitializeComponent();
        }



        private void btn_register_click(object sender, EventArgs e)
        {
            Form_Registro form_registro = new Form_Registro();
            form_registro.Show();
            
        }

        private void btn_login_click(object sender, EventArgs e)
        {
            Form_Login form_login = new Form_Login();
            form_login.Show();
            
            
        }

        private void Form_Acceso_Load(object sender, EventArgs e)
        {

        }
    }
}
