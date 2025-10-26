namespace Escritorio
{
    partial class Form_Categorias

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
            categoriasInMemoryBindingSource = new BindingSource(components);
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            btn_Borrar = new Button();
            txt_Name = new TextBox();
            GrdVw_Categoria = new DataGridView();
            Categoria_ID = new DataGridViewTextBoxColumn();
            Categoria_Name = new DataGridViewTextBoxColumn();
            btn_agregar = new Button();
            btn_Editar = new Button();
            txt_ID = new TextBox();
            ((System.ComponentModel.ISupportInitialize)categoriasInMemoryBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GrdVw_Categoria).BeginInit();
            SuspendLayout();
            // 
            // categoriasInMemoryBindingSource
            // 
            categoriasInMemoryBindingSource.DataSource = typeof(Data.ProductsInMemory);
            categoriasInMemoryBindingSource.CurrentChanged += productsInMemoryBindingSource_CurrentChanged;
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
            txt_Name.Location = new Point(187, 346);
            txt_Name.Name = "txt_Name";
            txt_Name.Size = new Size(169, 23);
            txt_Name.TabIndex = 9;
            // 
            // GrdVw_Categoria
            // 
            GrdVw_Categoria.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GrdVw_Categoria.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GrdVw_Categoria.Columns.AddRange(new DataGridViewColumn[] { Categoria_ID, Categoria_Name });
            GrdVw_Categoria.Dock = DockStyle.Top;
            GrdVw_Categoria.Location = new Point(0, 0);
            GrdVw_Categoria.Name = "GrdVw_Categoria";
            GrdVw_Categoria.Size = new Size(922, 241);
            GrdVw_Categoria.TabIndex = 0;
            GrdVw_Categoria.CellContentClick += dataGridView1_CellContentClick;
            GrdVw_Categoria.CellDoubleClick += GrdVw_Categoria_CellDoubleClick;
            // 
            // Categoria_ID
            // 
            Categoria_ID.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = "0";
            Categoria_ID.DefaultCellStyle = dataGridViewCellStyle1;
            Categoria_ID.HeaderText = "ID";
            Categoria_ID.Name = "Categoria_ID";
            Categoria_ID.ReadOnly = true;
            Categoria_ID.Resizable = DataGridViewTriState.True;
            // 
            // Categoria_Name
            // 
            Categoria_Name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Categoria_Name.FillWeight = 61.4562263F;
            Categoria_Name.HeaderText = "Nombre";
            Categoria_Name.Name = "Categoria_Name";
            Categoria_Name.ReadOnly = true;
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
            // txt_ID
            // 
            txt_ID.Location = new Point(12, 346);
            txt_ID.Name = "txt_ID";
            txt_ID.Size = new Size(169, 23);
            txt_ID.TabIndex = 9;
            // 
            // Form_Categorias
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(922, 486);
            Controls.Add(btn_Editar);
            Controls.Add(btn_agregar);
            Controls.Add(btn_Borrar);
            Controls.Add(txt_ID);
            Controls.Add(txt_Name);
            Controls.Add(GrdVw_Categoria);
            Name = "Form_Categorias";
            Text = "Productos";
            Load += FormProducts_Load;
            ((System.ComponentModel.ISupportInitialize)categoriasInMemoryBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)GrdVw_Categoria).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private BindingSource categoriasInMemoryBindingSource;
        private Button btn_Borrar;
        private TextBox txt_Name;
        private DataGridView GrdVw_Categoria;
        private DataGridViewTextBoxColumn Categoria_ID;
        private DataGridViewTextBoxColumn Categoria_Name;
     
        private Button btn_agregar;
        private Button btn_Editar;
        private TextBox txt_ID;
    }
}