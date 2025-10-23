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
using API.Clients;
using DTOs;

namespace Escritorio
{
    public partial class Form_Registro : Form
    {
        private readonly UsuarioApiClient _usuarioApiClient;

        public Form_Registro()
        {
            InitializeComponent();
            // Obtener el servicio desde el contenedor DI
            _usuarioApiClient = Program.ServiceProvider.GetRequiredService<UsuarioApiClient>();
        }

        private void Form_Registro_Load(object sender, EventArgs e)
        {
            // Configurar placeholders o validaciones adicionales
        }

        private async void btn_enviarRegistro_Click(object sender, EventArgs e)
        {
            string name = txt_name.Text.Trim();
            string surname = txt_surname.Text.Trim();
            string username = txt_username.Text.Trim();
            string password = txt_password.Text.Trim();
            string email = txt_email.Text.Trim();

            // Validaciones
            if (string.IsNullOrEmpty(name) ||
               string.IsNullOrEmpty(surname) ||
               string.IsNullOrEmpty(username) ||
               string.IsNullOrEmpty(password) ||
               string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Alguno de los campos no está completo", 
                              "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Contains(' '))
            {
                MessageBox.Show("La contraseña no puede tener espacios", 
                              "Contraseña inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!email.Contains("@"))
            {
                MessageBox.Show("Ingrese un email válido", 
                              "Email inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Deshabilitar el botón durante la operación
                btn_enviarRegistro.Enabled = false;
                btn_enviarRegistro.Text = "Registrando...";

                // Verificar si el username ya existe
                bool usernameExiste = await _usuarioApiClient.ExisteUsernameAsync(username);
                if (usernameExiste)
                {
                    MessageBox.Show("El nombre de usuario ya existe", 
                                  "Username duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear el request
                var createRequest = new CreateUsuarioRequest
                {
                    Nombre = name,
                    Apellido = surname,
                    Email = email,
                    Username = username,
                    Password = password
                };

                // Llamada a la API
                var usuarioCreado = await _usuarioApiClient.CreateAsync(createRequest);

                MessageBox.Show("Registro exitoso.", "Éxito", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar los campos
                txt_name.Clear();
                txt_surname.Clear();
                txt_username.Clear();
                txt_password.Clear();
                txt_email.Clear();

                this.Close();
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Error de conexión con el servidor. Verifique que la API esté funcionando.", 
                              "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar usuario: {ex.Message}", 
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Rehabilitar el botón
                btn_enviarRegistro.Enabled = true;
                btn_enviarRegistro.Text = "Registrar";
            }
        }

        private void txt_name_TextChanged(object sender, EventArgs e)
        {
            // Implementar validaciones en tiempo real si es necesario
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
    }
}
