namespace Escritorio
{
    partial class FormProducts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            productsInMemoryBindingSource = new BindingSource(components);
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            panel_Superior = new Panel();
            lbl_Titulo = new Label();
            txt_Buscar = new TextBox();
            btn_Refrescar = new Button();
            GrdVw_Product = new DataGridView();
            panel_Inferior = new Panel();
            lbl_TotalProductos = new Label();
            panel_Formulario = new Panel();
            lbl_FormularioTitulo = new Label();
            txt_ID = new TextBox();
            txt_Name = new TextBox();
            txt_Description = new TextBox();
            txt_Price = new TextBox();
            txt_Stock = new TextBox();
            lbl_Categoria = new Label();
            cmb_Categoria = new ComboBox();
            panel_Botones = new Panel();
            btn_Limpiar = new Button();
            btn_agregar = new Button();
            btn_Editar = new Button();
            btn_Borrar = new Button();
            ((System.ComponentModel.ISupportInitialize)productsInMemoryBindingSource).BeginInit();
            panel_Superior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GrdVw_Product).BeginInit();
            panel_Inferior.SuspendLayout();
            panel_Formulario.SuspendLayout();
            panel_Botones.SuspendLayout();
            SuspendLayout();
            // 
            // productsInMemoryBindingSource
            // 
            productsInMemoryBindingSource.DataSource = typeof(Data.ProductsInMemory);
            productsInMemoryBindingSource.CurrentChanged += productsInMemoryBindingSource_CurrentChanged;
            // 
            // panel_Superior
            // 
            panel_Superior.BackColor = Color.FromArgb(41, 128, 185);
            panel_Superior.Controls.Add(lbl_Titulo);
            panel_Superior.Controls.Add(txt_Buscar);
            panel_Superior.Controls.Add(btn_Refrescar);
            panel_Superior.Dock = DockStyle.Top;
            panel_Superior.Location = new Point(0, 0);
            panel_Superior.Name = "panel_Superior";
            panel_Superior.Padding = new Padding(10);
            panel_Superior.Size = new Size(1100, 70);
            panel_Superior.TabIndex = 0;
            // 
            // lbl_Titulo
            // 
            lbl_Titulo.AutoSize = true;
            lbl_Titulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lbl_Titulo.ForeColor = Color.White;
            lbl_Titulo.Location = new Point(13, 13);
            lbl_Titulo.Name = "lbl_Titulo";
            lbl_Titulo.Size = new Size(236, 30);
            lbl_Titulo.TabIndex = 0;
            lbl_Titulo.Text = "Gestión de Productos";
            // 
            // txt_Buscar
            // 
            txt_Buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_Buscar.Font = new Font("Segoe UI", 10F);
            txt_Buscar.Location = new Point(750, 20);
            txt_Buscar.Name = "txt_Buscar";
            txt_Buscar.Size = new Size(250, 25);
            txt_Buscar.TabIndex = 1;
            // 
            // btn_Refrescar
            // 
            btn_Refrescar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_Refrescar.BackColor = Color.FromArgb(52, 152, 219);
            btn_Refrescar.FlatStyle = FlatStyle.Flat;
            btn_Refrescar.ForeColor = Color.White;
            btn_Refrescar.Location = new Point(1010, 17);
            btn_Refrescar.Name = "btn_Refrescar";
            btn_Refrescar.Size = new Size(80, 30);
            btn_Refrescar.TabIndex = 2;
            btn_Refrescar.Text = "🔄 Refrescar";
            btn_Refrescar.UseVisualStyleBackColor = false;
            
            // 
            // GrdVw_Product
            // 
            GrdVw_Product.AllowUserToAddRows = false;
            GrdVw_Product.AllowUserToDeleteRows = false;
            GrdVw_Product.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GrdVw_Product.BackgroundColor = Color.White;
            GrdVw_Product.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GrdVw_Product.Dock = DockStyle.Fill;
            GrdVw_Product.Location = new Point(0, 70);
            GrdVw_Product.MultiSelect = false;
            GrdVw_Product.Name = "GrdVw_Product";
            GrdVw_Product.ReadOnly = true;
            GrdVw_Product.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GrdVw_Product.Size = new Size(700, 450);
            GrdVw_Product.TabIndex = 3;
            GrdVw_Product.CellContentClick += dataGridView1_CellContentClick;
            GrdVw_Product.CellDoubleClick += GrdVw_Product_CellDoubleClick;
            // 
            // panel_Inferior
            // 
            panel_Inferior.BackColor = Color.FromArgb(236, 240, 241);
            panel_Inferior.Controls.Add(lbl_TotalProductos);
            panel_Inferior.Dock = DockStyle.Bottom;
            panel_Inferior.Location = new Point(0, 520);
            panel_Inferior.Name = "panel_Inferior";
            panel_Inferior.Padding = new Padding(10);
            panel_Inferior.Size = new Size(700, 40);
            panel_Inferior.TabIndex = 4;
            // 
            // lbl_TotalProductos
            // 
            lbl_TotalProductos.AutoSize = true;
            lbl_TotalProductos.Font = new Font("Segoe UI", 9F);
            lbl_TotalProductos.Location = new Point(13, 12);
            lbl_TotalProductos.Name = "lbl_TotalProductos";
            lbl_TotalProductos.Size = new Size(109, 15);
            lbl_TotalProductos.TabIndex = 0;
            lbl_TotalProductos.Text = "Total: 0 producto(s)";
            // 
            // panel_Formulario
            // 
            panel_Formulario.BackColor = Color.White;
            panel_Formulario.BorderStyle = BorderStyle.FixedSingle;
            panel_Formulario.Controls.Add(lbl_FormularioTitulo);
            panel_Formulario.Controls.Add(txt_ID);
            panel_Formulario.Controls.Add(txt_Name);
            panel_Formulario.Controls.Add(txt_Description);
            panel_Formulario.Controls.Add(txt_Price);
            panel_Formulario.Controls.Add(txt_Stock);
            panel_Formulario.Controls.Add(lbl_Categoria);
            panel_Formulario.Controls.Add(cmb_Categoria);
            panel_Formulario.Controls.Add(panel_Botones);
            panel_Formulario.Dock = DockStyle.Right;
            panel_Formulario.Location = new Point(700, 70);
            panel_Formulario.Name = "panel_Formulario";
            panel_Formulario.Padding = new Padding(15);
            panel_Formulario.Size = new Size(400, 490);
            panel_Formulario.TabIndex = 5;
            // 
            // lbl_FormularioTitulo
            // 
            lbl_FormularioTitulo.AutoSize = true;
            lbl_FormularioTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lbl_FormularioTitulo.Location = new Point(18, 15);
            lbl_FormularioTitulo.Name = "lbl_FormularioTitulo";
            lbl_FormularioTitulo.Size = new Size(156, 21);
            lbl_FormularioTitulo.TabIndex = 0;
            lbl_FormularioTitulo.Text = "Datos del Producto";
            // 
            // txt_ID
            // 
            txt_ID.BackColor = Color.FromArgb(236, 240, 241);
            txt_ID.Font = new Font("Segoe UI", 10F);
            txt_ID.Location = new Point(18, 50);
            txt_ID.Name = "txt_ID";
            txt_ID.ReadOnly = true;
            txt_ID.Size = new Size(360, 25);
            txt_ID.TabIndex = 1;
            // 
            // txt_Name
            // 
            txt_Name.Font = new Font("Segoe UI", 10F);
            txt_Name.Location = new Point(18, 85);
            txt_Name.Name = "txt_Name";
            txt_Name.Size = new Size(360, 25);
            txt_Name.TabIndex = 2;
            txt_Name.TextChanged += txt_Name_TextChanged;
            // 
            // txt_Description
            // 
            txt_Description.Font = new Font("Segoe UI", 10F);
            txt_Description.Location = new Point(18, 120);
            txt_Description.Multiline = true;
            txt_Description.Name = "txt_Description";
            txt_Description.Size = new Size(360, 60);
            txt_Description.TabIndex = 3;
            // 
            // txt_Price
            // 
            txt_Price.Font = new Font("Segoe UI", 10F);
            txt_Price.Location = new Point(18, 190);
            txt_Price.Name = "txt_Price";
            txt_Price.Size = new Size(170, 25);
            txt_Price.TabIndex = 4;
            txt_Price.TextChanged += txt_Price_TextChanged;
            // 
            // txt_Stock
            // 
            txt_Stock.Font = new Font("Segoe UI", 10F);
            txt_Stock.Location = new Point(208, 190);
            txt_Stock.Name = "txt_Stock";
            txt_Stock.Size = new Size(170, 25);
            txt_Stock.TabIndex = 5;
            // 
            // lbl_Categoria
            // 
            lbl_Categoria.AutoSize = true;
            lbl_Categoria.Font = new Font("Segoe UI", 9F);
            lbl_Categoria.Location = new Point(18, 225);
            lbl_Categoria.Name = "lbl_Categoria";
            lbl_Categoria.Size = new Size(61, 15);
            lbl_Categoria.TabIndex = 6;
            lbl_Categoria.Text = "Categoría:";
            // 
            // cmb_Categoria
            // 
            cmb_Categoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_Categoria.Font = new Font("Segoe UI", 10F);
            cmb_Categoria.FormattingEnabled = true;
            cmb_Categoria.Location = new Point(18, 245);
            cmb_Categoria.Name = "cmb_Categoria";
            cmb_Categoria.Size = new Size(360, 25);
            cmb_Categoria.TabIndex = 6;
            // 
            // panel_Botones
            // 
            panel_Botones.Controls.Add(btn_Limpiar);
            panel_Botones.Controls.Add(btn_agregar);
            panel_Botones.Controls.Add(btn_Editar);
            panel_Botones.Controls.Add(btn_Borrar);
            panel_Botones.Dock = DockStyle.Bottom;
            panel_Botones.Location = new Point(15, 300);
            panel_Botones.Name = "panel_Botones";
            panel_Botones.Size = new Size(368, 173);
            panel_Botones.TabIndex = 7;
            // 
            // btn_Limpiar
            // 
            btn_Limpiar.BackColor = Color.FromArgb(149, 165, 166);
            btn_Limpiar.Dock = DockStyle.Top;
            btn_Limpiar.FlatStyle = FlatStyle.Flat;
            btn_Limpiar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn_Limpiar.ForeColor = Color.White;
            btn_Limpiar.Location = new Point(0, 120);
            btn_Limpiar.Margin = new Padding(0, 0, 0, 5);
            btn_Limpiar.Name = "btn_Limpiar";
            btn_Limpiar.Size = new Size(368, 40);
            btn_Limpiar.TabIndex = 10;
            btn_Limpiar.Text = "🗑️ Limpiar Campos";
            btn_Limpiar.UseVisualStyleBackColor = false;
            
            // 
            // btn_agregar
            // 
            btn_agregar.BackColor = Color.FromArgb(39, 174, 96);
            btn_agregar.Dock = DockStyle.Top;
            btn_agregar.FlatStyle = FlatStyle.Flat;
            btn_agregar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn_agregar.ForeColor = Color.White;
            btn_agregar.Location = new Point(0, 80);
            btn_agregar.Margin = new Padding(0, 5, 0, 5);
            btn_agregar.Name = "btn_agregar";
            btn_agregar.Size = new Size(368, 40);
            btn_agregar.TabIndex = 7;
            btn_agregar.Text = "➕ Agregar Producto";
            btn_agregar.UseVisualStyleBackColor = false;
            
            // 
            // btn_Editar
            // 
            btn_Editar.BackColor = Color.FromArgb(241, 196, 15);
            btn_Editar.Dock = DockStyle.Top;
            btn_Editar.FlatStyle = FlatStyle.Flat;
            btn_Editar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn_Editar.ForeColor = Color.White;
            btn_Editar.Location = new Point(0, 40);
            btn_Editar.Margin = new Padding(0, 5, 0, 5);
            btn_Editar.Name = "btn_Editar";
            btn_Editar.Size = new Size(368, 40);
            btn_Editar.TabIndex = 8;
            btn_Editar.Text = "✏️ Editar Producto";
            btn_Editar.UseVisualStyleBackColor = false;
            
            // 
            // btn_Borrar
            // 
            btn_Borrar.BackColor = Color.FromArgb(231, 76, 60);
            btn_Borrar.Dock = DockStyle.Top;
            btn_Borrar.FlatStyle = FlatStyle.Flat;
            btn_Borrar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn_Borrar.ForeColor = Color.White;
            btn_Borrar.Location = new Point(0, 0);
            btn_Borrar.Margin = new Padding(0, 5, 0, 0);
            btn_Borrar.Name = "btn_Borrar";
            btn_Borrar.Size = new Size(368, 40);
            btn_Borrar.TabIndex = 9;
            btn_Borrar.Text = "❌ Eliminar Producto";
            btn_Borrar.UseVisualStyleBackColor = false;
            
            // 
            // FormProducts
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 560);
            Controls.Add(GrdVw_Product);
            Controls.Add(panel_Inferior);
            Controls.Add(panel_Formulario);
            Controls.Add(panel_Superior);
            MinimumSize = new Size(1100, 560);
            Name = "FormProducts";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Control de Stock - Productos";
            Load += FormProducts_Load;
            ((System.ComponentModel.ISupportInitialize)productsInMemoryBindingSource).EndInit();
            panel_Superior.ResumeLayout(false);
            panel_Superior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GrdVw_Product).EndInit();
            panel_Inferior.ResumeLayout(false);
            panel_Inferior.PerformLayout();
            panel_Formulario.ResumeLayout(false);
            panel_Formulario.PerformLayout();
            panel_Botones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private BindingSource productsInMemoryBindingSource;
        
        // Panel Superior
        private Panel panel_Superior;
        private Label lbl_Titulo;
        private TextBox txt_Buscar;
        private Button btn_Refrescar;
        
        // DataGridView
        private DataGridView GrdVw_Product;
        
        // Panel Inferior
        private Panel panel_Inferior;
        private Label lbl_TotalProductos;
        
        // Panel Formulario
        private Panel panel_Formulario;
        private Label lbl_FormularioTitulo;
        private TextBox txt_ID;
        private TextBox txt_Name;
        private TextBox txt_Description;
        private TextBox txt_Price;
        private TextBox txt_Stock;
        private Label lbl_Categoria;
        private ComboBox cmb_Categoria;
        
        // Panel Botones
        private Panel panel_Botones;
        private Button btn_Limpiar;
        private Button btn_agregar;
        private Button btn_Editar;
        private Button btn_Borrar;
    }
}