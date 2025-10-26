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

        private void buttonProducts_Click(object sender, EventArgs e)
        {
            var formProducts = Program.ServiceProvider.GetRequiredService<FormProducts>();
            formProducts.Show();
        }
        private void buttonCategorias_Click(object sender, EventArgs e)
        {
            var formEventos = Program.ServiceProvider.GetRequiredService<Form_Eventos>();
            formEventos.Show();
        }
        



    }
}
