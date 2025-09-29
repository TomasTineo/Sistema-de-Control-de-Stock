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

namespace Escritorio
{
    public partial class Form_Login : Form
    {
        private readonly IUsuarioApiClient _usuarioApiClient;

        public Form_Login()
        {
            InitializeComponent();
            // Obtener el servicio desde el contenedor DI
            _usuarioApiClient = Program.ServiceProvider.GetRequiredService<IUsuarioApiClient>();
        }

        private async void btn_enviarLogin_Click(object sender, EventArgs e)
        {
            string username = txt_username.Text.Trim();
            string password = txt_password.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            try
            {
                // Deshabilitar el botón durante la operación
                btn_enviarLogin.Enabled = false;
                btn_enviarLogin.Text = "Iniciando sesión...";

                // Llamada a la API
                var usuario = await _usuarioApiClient.LoginAsync(username, password);
                
                if (usuario != null)
                {
                    MessageBox.Show($"Bienvenido {usuario.Nombre} {usuario.Apellido}!", 
                                  "Login exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Crear y mostrar el formulario de productos
                    var formProducts = Program.ServiceProvider.GetRequiredService<FormProducts>();
                    formProducts.Show();
                    
                    // Ocultar el formulario de login
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", 
                                  "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Error de conexión con el servidor. Verifique que la API esté funcionando.", 
                              "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", 
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Rehabilitar el botón
                btn_enviarLogin.Enabled = true;
                btn_enviarLogin.Text = "Iniciar Sesión";
            }
        }
    }
}
