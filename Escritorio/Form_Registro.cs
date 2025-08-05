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
    public partial class Form_Registro : Form
    {
        public Form_Registro()
        {
            InitializeComponent();
        }

        private void Form_Registro_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btn_enviarRegistro_Click(object sender, EventArgs e)
        {
            string name = txt_username.Text;
            string surname = txt_surname.Text;
            string username = txt_username.Text.Trim();
            string password = txt_password.Text.Trim();
            string email = txt_email.Text;

            if (string.IsNullOrEmpty(name) ||
               string.IsNullOrEmpty(surname) ||
               string.IsNullOrEmpty(username) ||
               string.IsNullOrEmpty(password) ||
               string.IsNullOrEmpty(email)) 
            {
                MessageBox.Show("Alguno de los campos no esta completo");
                return;
            }

            if (password.Contains(' '))
            {
                MessageBox.Show("La contraseña no puede tener espacios");
                return;
            }

            if (DatosGlobales.Usuarios.Any(u => u.Username == username))
            {
                MessageBox.Show("El nombre de usuario ya existe");
                return;
            }

            Usuario newUser = new Usuario
            {
                Id = DatosGlobales.Usuarios.Count + 1, // ----------------- EL ID SE DEBERIA ASIGNAR EN LA BASE DE DATOS
                Nombre = name,
                Apellido = surname,
                Username = username,
                Password = password,
                Email = email
            };

            DatosGlobales.Usuarios.Add(newUser);

            MessageBox.Show("Registro exitoso.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);


            txt_username.Clear();
            txt_surname.Clear();
            txt_username.Clear();
            txt_password.Clear();
            txt_email.Clear();

            this.Close();
        }

        private void txt_name_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
