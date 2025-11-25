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
            
            // Suscribirse al evento de login exitoso
            form_login.LoginExitoso += (s, args) => 
            {
                // Ocultar el formulario de acceso cuando el login sea exitoso
                this.Hide();
            };
            
            form_login.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
