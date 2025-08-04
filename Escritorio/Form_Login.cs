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
    public partial class Form_Login : Form
    {
        public Form_Login()
        {
            InitializeComponent();
        }

        private void btn_enviarLogin_Click(object sender, EventArgs e)
        {

            string username = txt_username.Text.Trim();
            string password = txt_password.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }
            Usuario user = DatosGlobales.Usuarios.Find(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                MessageBox.Show($"Bienvenido {user.Nombre} {user.Apellido}!");
                //formBase baseForm = new formBase();
                //baseForm.Show();

                MessageBox.Show("Bienvenido al sistema de gestión de stock.");
                this.Close();

            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
                return;
            }
        }
    }
}
