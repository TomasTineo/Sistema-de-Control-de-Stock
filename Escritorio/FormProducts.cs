using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Data;

namespace Escritorio
{
    public partial class FormProducts : Form
    {


        public FormProducts()
        {
            InitializeComponent();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_ID.Text) || string.IsNullOrWhiteSpace(txt_Name.Text) ||
                string.IsNullOrWhiteSpace(txt_Description.Text) || string.IsNullOrWhiteSpace(txt_Price.Text) ||
                string.IsNullOrWhiteSpace(txt_Stock.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(txt_ID.Text, out int id)
                || !decimal.TryParse(txt_Price.Text, out decimal precio)
                || !int.TryParse(txt_Stock.Text, out int stock))
            {
                MessageBox.Show("ID, Precio y Stock deben ser valores numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var nuevoProducto = new Producto(id, txt_Name.Text, precio, txt_Description.Text, stock);
            ProductsInMemory.Productos.Add(nuevoProducto);

            // Refresca el DataGridView
            GrdVw_Product.DataSource = null;
            GrdVw_Product.DataSource = ProductsInMemory.Productos;

            // Limpia los TextBox
            txt_ID.Clear();
            txt_Name.Clear();
            txt_Description.Clear();
            txt_Price.Clear();
            txt_Stock.Clear();

        }

        private void FormProducts_Load(object sender, EventArgs e)
        {

            // TextBox placeholders
            txt_ID.PlaceholderText = "Identificación";
            txt_Name.PlaceholderText = "Nombre";
            txt_Description.PlaceholderText = "Descripción";
            txt_Price.PlaceholderText = "Precio";
            txt_Stock.PlaceholderText = "Stock actual";

            // Columnas del DataGridView

            GrdVw_Product.AutoGenerateColumns = false;
            GrdVw_Product.Columns.Clear();

            var colId = new DataGridViewTextBoxColumn();
            colId.HeaderText = "ID";
            colId.DataPropertyName = "Id";
            GrdVw_Product.Columns.Add(colId);

            var colName = new DataGridViewTextBoxColumn();
            colName.HeaderText = "Nombre";
            colName.DataPropertyName = "Nombre";
            GrdVw_Product.Columns.Add(colName);

            var colDescription = new DataGridViewTextBoxColumn();
            colDescription.HeaderText = "Descripción";
            colDescription.DataPropertyName = "Descripcion";
            GrdVw_Product.Columns.Add(colDescription);

            var colPrice = new DataGridViewTextBoxColumn();
            colPrice.HeaderText = "Precio por unidad";
            colPrice.DataPropertyName = "Precio";
            colPrice.DefaultCellStyle.Format = "C2"; // Formato de moneda
            GrdVw_Product.Columns.Add(colPrice);

            var colStock = new DataGridViewTextBoxColumn();
            colStock.HeaderText = "Stock actual";
            colStock.DataPropertyName = "Stock";
            colStock.DefaultCellStyle.Format = "N0"; // Formato numérico sin decimales
            GrdVw_Product.Columns.Add(colStock);

            GrdVw_Product.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GrdVw_Product.DataSource = ProductsInMemory.Productos;
        }

        // Agrega los datos de la fila en los textbox al hacer doble clic en una fila del DataGridView
        private void GrdVw_Product_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var producto = (Producto)GrdVw_Product.Rows[e.RowIndex].DataBoundItem;
                txt_ID.Text = producto.Id.ToString();
                txt_Name.Text = producto.Nombre;
                txt_Description.Text = producto.Descripcion;
                txt_Price.Text = producto.Precio.ToString();
                txt_Stock.Text = producto.Stock.ToString();
            }
        }

        // Edita el producto seleccionado en los TextBox y actualiza el DataGridView
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txt_ID.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Por favor, ingrese un ID válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var producto = ProductsInMemory.Productos.FirstOrDefault(p => p.Id == id);
            if (producto != null)
            {
                producto.Nombre = txt_Name.Text;
                producto.Descripcion = txt_Description.Text;
                producto.Precio = decimal.TryParse(txt_Price.Text, out decimal precio) ? precio : producto.Precio;
                producto.Stock = int.TryParse(txt_Stock.Text, out int stock) ? stock : producto.Stock;

                GrdVw_Product.DataSource = null;
                GrdVw_Product.DataSource = ProductsInMemory.Productos;

            }
        }

        // Elimina el producto seleccionado en los TextBox y actualiza el DataGridView
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txt_ID.Text, out int id))
            {
                MessageBox.Show("Seleccione un producto válido para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var producto = ProductsInMemory.Productos.FirstOrDefault(p => p.Id == id);
            if (producto != null)
            {
                ProductsInMemory.Productos.Remove(producto);
                GrdVw_Product.DataSource = null;
                GrdVw_Product.DataSource = ProductsInMemory.Productos;
                MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txt_ID.Clear();
                txt_Name.Clear();
                txt_Description.Clear();
                txt_Price.Clear();
                txt_Stock.Clear();

            }
            else
            {
                MessageBox.Show("Producto no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private void productsInMemoryBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_Price_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Name_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
