using System;
using System.Windows.Forms;

namespace Sistema_Stock
{
    public partial class formLogin : Form
    {
        public formLogin()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.background;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click(object sender, EventArgs e)
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
                formBase baseForm = new formBase();
                baseForm.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
                return;
            }



        }

        private void lnkOlvidaPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Es Ud. un usuario muy descuidado, haga memoria",
            "Olvidé mi contraseña",
            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }
    }
}
