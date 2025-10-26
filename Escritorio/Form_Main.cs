using Microsoft.Extensions.DependencyInjection;


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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form_registro = Program.ServiceProvider.GetRequiredService<FormProducts>();
            form_registro.Show();
        }
    }
}
