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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            productsInMemoryBindingSource = new BindingSource(components);
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            txt_Description = new TextBox();
            btn_Borrar = new Button();
            txt_Name = new TextBox();
            txt_Price = new TextBox();
            GrdVw_Product = new DataGridView();
            Product_ID = new DataGridViewTextBoxColumn();
            Product_Name = new DataGridViewTextBoxColumn();
            Product_Description = new DataGridViewTextBoxColumn();
            Product_Price = new DataGridViewTextBoxColumn();
            Product_Stock = new DataGridViewTextBoxColumn();
            txt_Stock = new TextBox();
            btn_agregar = new Button();
            txt_ID = new TextBox();
            btn_Editar = new Button();
            ((System.ComponentModel.ISupportInitialize)productsInMemoryBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GrdVw_Product).BeginInit();
            SuspendLayout();
            // 
            // productsInMemoryBindingSource
            // 
            productsInMemoryBindingSource.DataSource = typeof(Data.ProductsInMemory);
            productsInMemoryBindingSource.CurrentChanged += productsInMemoryBindingSource_CurrentChanged;
            // 
            // txt_Description
            // 
            txt_Description.Location = new Point(366, 331);
            txt_Description.Name = "txt_Description";
            txt_Description.Size = new Size(187, 23);
            txt_Description.TabIndex = 4;
            // 
            // btn_Borrar
            // 
            btn_Borrar.Dock = DockStyle.Bottom;
            btn_Borrar.Location = new Point(0, 449);
            btn_Borrar.Name = "btn_Borrar";
            btn_Borrar.Size = new Size(922, 37);
            btn_Borrar.TabIndex = 7;
            btn_Borrar.Text = "Borrar";
            btn_Borrar.UseVisualStyleBackColor = true;
            btn_Borrar.Click += btnEliminar_Click;
            // 
            // txt_Name
            // 
            txt_Name.Location = new Point(191, 331);
            txt_Name.Name = "txt_Name";
            txt_Name.Size = new Size(169, 23);
            txt_Name.TabIndex = 9;
            txt_Name.TextChanged += txt_Name_TextChanged;
            // 
            // txt_Price
            // 
            txt_Price.Location = new Point(559, 331);
            txt_Price.Name = "txt_Price";
            txt_Price.Size = new Size(169, 23);
            txt_Price.TabIndex = 10;
            // 
            // GrdVw_Product
            // 
            GrdVw_Product.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GrdVw_Product.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GrdVw_Product.Columns.AddRange(new DataGridViewColumn[] { Product_ID, Product_Name, Product_Description, Product_Price, Product_Stock });
            GrdVw_Product.Dock = DockStyle.Top;
            GrdVw_Product.Location = new Point(0, 0);
            GrdVw_Product.Name = "GrdVw_Product";
            GrdVw_Product.Size = new Size(922, 241);
            GrdVw_Product.TabIndex = 0;
            GrdVw_Product.CellContentClick += dataGridView1_CellContentClick;
            GrdVw_Product.CellDoubleClick += GrdVw_Product_CellDoubleClick;
            // 
            // Product_ID
            // 
            Product_ID.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = "0";
            Product_ID.DefaultCellStyle = dataGridViewCellStyle1;
            Product_ID.HeaderText = "ID";
            Product_ID.Name = "Product_ID";
            Product_ID.ReadOnly = true;
            Product_ID.Resizable = DataGridViewTriState.True;
            // 
            // Product_Name
            // 
            Product_Name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Product_Name.FillWeight = 61.4562263F;
            Product_Name.HeaderText = "Nombre";
            Product_Name.Name = "Product_Name";
            Product_Name.ReadOnly = true;
            // 
            // Product_Description
            // 
            Product_Description.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Product_Description.FillWeight = 61.8242836F;
            Product_Description.HeaderText = "Descripción";
            Product_Description.Name = "Product_Description";
            Product_Description.ReadOnly = true;
            // 
            // Product_Price
            // 
            Product_Price.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Product_Price.FillWeight = 61.4562645F;
            Product_Price.HeaderText = "Precio";
            Product_Price.Name = "Product_Price";
            Product_Price.ReadOnly = true;
            // 
            // Product_Stock
            // 
            Product_Stock.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = "0";
            Product_Stock.DefaultCellStyle = dataGridViewCellStyle2;
            Product_Stock.FillWeight = 61.45614F;
            Product_Stock.HeaderText = "Stock";
            Product_Stock.Name = "Product_Stock";
            Product_Stock.ReadOnly = true;
            // 
            // txt_Stock
            // 
            txt_Stock.Location = new Point(738, 331);
            txt_Stock.Name = "txt_Stock";
            txt_Stock.Size = new Size(169, 23);
            txt_Stock.TabIndex = 6;
            // 
            // btn_agregar
            // 
            btn_agregar.BackColor = Color.Transparent;
            btn_agregar.Dock = DockStyle.Bottom;
            btn_agregar.Location = new Point(0, 412);
            btn_agregar.Name = "btn_agregar";
            btn_agregar.Size = new Size(922, 37);
            btn_agregar.TabIndex = 1;
            btn_agregar.Text = "Agregar";
            btn_agregar.UseVisualStyleBackColor = false;
            btn_agregar.Click += button1_Click;
            // 
            // txt_ID
            // 
            txt_ID.Location = new Point(16, 331);
            txt_ID.Name = "txt_ID";
            txt_ID.Size = new Size(169, 23);
            txt_ID.TabIndex = 11;
            // 
            // btn_Editar
            // 
            btn_Editar.Dock = DockStyle.Bottom;
            btn_Editar.Location = new Point(0, 375);
            btn_Editar.Name = "btn_Editar";
            btn_Editar.Size = new Size(922, 37);
            btn_Editar.TabIndex = 12;
            btn_Editar.Text = "Editar";
            btn_Editar.UseVisualStyleBackColor = true;
            btn_Editar.Click += btnEditar_Click;
            // 
            // FormProducts
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(922, 486);
            Controls.Add(btn_Editar);
            Controls.Add(txt_ID);
            Controls.Add(txt_Stock);
            Controls.Add(btn_agregar);
            Controls.Add(btn_Borrar);
            Controls.Add(txt_Description);
            Controls.Add(txt_Name);
            Controls.Add(txt_Price);
            Controls.Add(GrdVw_Product);
            Name = "FormProducts";
            Text = "Productos";
            Load += FormProducts_Load;
            ((System.ComponentModel.ISupportInitialize)productsInMemoryBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)GrdVw_Product).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private TextBox txt_Description;
        private BindingSource productsInMemoryBindingSource;
        private Button btn_Borrar;
        private TextBox txt_Name;
        private TextBox txt_Price;
        private DataGridView GrdVw_Product;
        private DataGridViewTextBoxColumn Product_ID;
        private DataGridViewTextBoxColumn Product_Name;
        private DataGridViewTextBoxColumn Product_Description;
        private DataGridViewTextBoxColumn Product_Price;
        private DataGridViewTextBoxColumn Product_Stock;
        private TextBox txt_Stock;
        private Button btn_agregar;
        private TextBox txt_ID;
        private Button btn_Editar;
    }
}