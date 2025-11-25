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
            categoriasInMemoryBindingSource = new BindingSource(components);
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            panel_Superior = new Panel();
            lbl_Titulo = new Label();
            txt_Buscar = new TextBox();
            btn_Refrescar = new Button();
            GrdVw_Categoria = new DataGridView();
            panel_Inferior = new Panel();
            lbl_TotalCategorias = new Label();
            panel_Formulario = new Panel();
            lbl_FormularioTitulo = new Label();
            txt_ID = new TextBox();
            txt_Name = new TextBox();
            panel_Botones = new Panel();
            btn_Limpiar = new Button();
            btn_agregar = new Button();
            btn_Editar = new Button();
            btn_Borrar = new Button();
            ((System.ComponentModel.ISupportInitialize)categoriasInMemoryBindingSource).BeginInit();
            panel_Superior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GrdVw_Categoria).BeginInit();
            panel_Inferior.SuspendLayout();
            panel_Formulario.SuspendLayout();
            panel_Botones.SuspendLayout();
            SuspendLayout();
            // 
            // categoriasInMemoryBindingSource
            // 
            categoriasInMemoryBindingSource.CurrentChanged += productsInMemoryBindingSource_CurrentChanged;
            // 
            // panel_Superior
            // 
            panel_Superior.Controls.Add(lbl_Titulo);
            panel_Superior.Controls.Add(txt_Buscar);
            panel_Superior.Controls.Add(btn_Refrescar);
            panel_Superior.Dock = DockStyle.Top;
            panel_Superior.Location = new Point(0, 0);
            panel_Superior.Name = "panel_Superior";
            panel_Superior.Padding = new Padding(10);
            panel_Superior.Size = new Size(1084, 70);
            panel_Superior.TabIndex = 0;
            // 
            // lbl_Titulo
            // 
            lbl_Titulo.AutoSize = true;
            lbl_Titulo.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lbl_Titulo.Location = new Point(13, 13);
            lbl_Titulo.Name = "lbl_Titulo";
            lbl_Titulo.Size = new Size(171, 21);
            lbl_Titulo.TabIndex = 0;
            lbl_Titulo.Text = "Gestión de Categorías";
            // 
            // txt_Buscar
            // 
            txt_Buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_Buscar.Location = new Point(734, 20);
            txt_Buscar.Name = "txt_Buscar";
            txt_Buscar.PlaceholderText = "Buscar categorías...";
            txt_Buscar.Size = new Size(250, 23);
            txt_Buscar.TabIndex = 1;
            // 
            // btn_Refrescar
            // 
            btn_Refrescar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_Refrescar.Location = new Point(994, 17);
            btn_Refrescar.Name = "btn_Refrescar";
            btn_Refrescar.Size = new Size(80, 30);
            btn_Refrescar.TabIndex = 2;
            btn_Refrescar.Text = "Refrescar";
            btn_Refrescar.UseVisualStyleBackColor = true;
            btn_Refrescar.Click += btnRefrescar_Click;
            // 
            // GrdVw_Categoria
            // 
            GrdVw_Categoria.AllowUserToAddRows = false;
            GrdVw_Categoria.AllowUserToDeleteRows = false;
            GrdVw_Categoria.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GrdVw_Categoria.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GrdVw_Categoria.Dock = DockStyle.Fill;
            GrdVw_Categoria.Location = new Point(0, 70);
            GrdVw_Categoria.MultiSelect = false;
            GrdVw_Categoria.Name = "GrdVw_Categoria";
            GrdVw_Categoria.ReadOnly = true;
            GrdVw_Categoria.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GrdVw_Categoria.Size = new Size(684, 411);
            GrdVw_Categoria.TabIndex = 3;
            GrdVw_Categoria.CellContentClick += dataGridView1_CellContentClick;
            GrdVw_Categoria.CellDoubleClick += GrdVw_Categoria_CellDoubleClick;
            // 
            // panel_Inferior
            // 
            panel_Inferior.Controls.Add(lbl_TotalCategorias);
            panel_Inferior.Dock = DockStyle.Bottom;
            panel_Inferior.Location = new Point(0, 481);
            panel_Inferior.Name = "panel_Inferior";
            panel_Inferior.Padding = new Padding(10);
            panel_Inferior.Size = new Size(684, 40);
            panel_Inferior.TabIndex = 4;
            // 
            // lbl_TotalCategorias
            // 
            lbl_TotalCategorias.AutoSize = true;
            lbl_TotalCategorias.Location = new Point(13, 12);
            lbl_TotalCategorias.Name = "lbl_TotalCategorias";
            lbl_TotalCategorias.Size = new Size(109, 15);
            lbl_TotalCategorias.TabIndex = 0;
            lbl_TotalCategorias.Text = "Total: 0 categoría(s)";
            // 
            // panel_Formulario
            // 
            panel_Formulario.BorderStyle = BorderStyle.FixedSingle;
            panel_Formulario.Controls.Add(lbl_FormularioTitulo);
            panel_Formulario.Controls.Add(txt_ID);
            panel_Formulario.Controls.Add(txt_Name);
            panel_Formulario.Controls.Add(panel_Botones);
            panel_Formulario.Dock = DockStyle.Right;
            panel_Formulario.Location = new Point(684, 70);
            panel_Formulario.Name = "panel_Formulario";
            panel_Formulario.Padding = new Padding(15);
            panel_Formulario.Size = new Size(400, 451);
            panel_Formulario.TabIndex = 5;
            // 
            // lbl_FormularioTitulo
            // 
            lbl_FormularioTitulo.AutoSize = true;
            lbl_FormularioTitulo.Location = new Point(18, 15);
            lbl_FormularioTitulo.Name = "lbl_FormularioTitulo";
            lbl_FormularioTitulo.Size = new Size(119, 15);
            lbl_FormularioTitulo.TabIndex = 0;
            lbl_FormularioTitulo.Text = "Datos de la Categoría";
            // 
            // txt_ID
            // 
            txt_ID.Location = new Point(18, 50);
            txt_ID.Name = "txt_ID";
            txt_ID.ReadOnly = true;
            txt_ID.Size = new Size(360, 23);
            txt_ID.TabIndex = 1;
            // 
            // txt_Name
            // 
            txt_Name.Location = new Point(18, 85);
            txt_Name.Name = "txt_Name";
            txt_Name.Size = new Size(360, 23);
            txt_Name.TabIndex = 2;
            txt_Name.TextChanged += textBox1_TextChanged;
            // 
            // panel_Botones
            // 
            panel_Botones.Controls.Add(btn_Limpiar);
            panel_Botones.Controls.Add(btn_agregar);
            panel_Botones.Controls.Add(btn_Editar);
            panel_Botones.Controls.Add(btn_Borrar);
            panel_Botones.Dock = DockStyle.Bottom;
            panel_Botones.Location = new Point(15, 261);
            panel_Botones.Name = "panel_Botones";
            panel_Botones.Size = new Size(368, 173);
            panel_Botones.TabIndex = 7;
            // 
            // btn_Limpiar
            // 
            btn_Limpiar.Dock = DockStyle.Top;
            btn_Limpiar.Location = new Point(0, 120);
            btn_Limpiar.Margin = new Padding(0, 0, 0, 5);
            btn_Limpiar.Name = "btn_Limpiar";
            btn_Limpiar.Size = new Size(368, 40);
            btn_Limpiar.TabIndex = 10;
            btn_Limpiar.Text = "Limpiar Campos";
            btn_Limpiar.UseVisualStyleBackColor = true;
            btn_Limpiar.Click += btnLimpiar_Click;
            // 
            // btn_agregar
            // 
            btn_agregar.Dock = DockStyle.Top;
            btn_agregar.Location = new Point(0, 80);
            btn_agregar.Margin = new Padding(0, 5, 0, 5);
            btn_agregar.Name = "btn_agregar";
            btn_agregar.Size = new Size(368, 40);
            btn_agregar.TabIndex = 7;
            btn_agregar.Text = "Agregar Categoría";
            btn_agregar.UseVisualStyleBackColor = true;
            btn_agregar.Click += button1_Click;
            // 
            // btn_Editar
            // 
            btn_Editar.Dock = DockStyle.Top;
            btn_Editar.Location = new Point(0, 40);
            btn_Editar.Margin = new Padding(0, 5, 0, 5);
            btn_Editar.Name = "btn_Editar";
            btn_Editar.Size = new Size(368, 40);
            btn_Editar.TabIndex = 8;
            btn_Editar.Text = "Editar Categoría";
            btn_Editar.UseVisualStyleBackColor = true;
            btn_Editar.Click += btnEditar_Click;
            // 
            // btn_Borrar
            // 
            btn_Borrar.Dock = DockStyle.Top;
            btn_Borrar.Location = new Point(0, 0);
            btn_Borrar.Margin = new Padding(0, 5, 0, 0);
            btn_Borrar.Name = "btn_Borrar";
            btn_Borrar.Size = new Size(368, 40);
            btn_Borrar.TabIndex = 9;
            btn_Borrar.Text = "Eliminar Categoría";
            btn_Borrar.UseVisualStyleBackColor = true;
            btn_Borrar.Click += btnEliminar_Click;
            // 
            // Form_Categorias
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 521);
            Controls.Add(GrdVw_Categoria);
            Controls.Add(panel_Inferior);
            Controls.Add(panel_Formulario);
            Controls.Add(panel_Superior);
            MinimumSize = new Size(1100, 560);
            Name = "Form_Categorias";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Control de Stock - Categorías";
            Load += FormProducts_Load;
            ((System.ComponentModel.ISupportInitialize)categoriasInMemoryBindingSource).EndInit();
            panel_Superior.ResumeLayout(false);
            panel_Superior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GrdVw_Categoria).EndInit();
            panel_Inferior.ResumeLayout(false);
            panel_Inferior.PerformLayout();
            panel_Formulario.ResumeLayout(false);
            panel_Formulario.PerformLayout();
            panel_Botones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private BindingSource categoriasInMemoryBindingSource;
        
        // Panel Superior
        private Panel panel_Superior;
        private Label lbl_Titulo;
        private TextBox txt_Buscar;
        private Button btn_Refrescar;
        
        // DataGridView
        private DataGridView GrdVw_Categoria;
        
        // Panel Inferior
        private Panel panel_Inferior;
        private Label lbl_TotalCategorias;
        
        // Panel Formulario
        private Panel panel_Formulario;
        private Label lbl_FormularioTitulo;
        private TextBox txt_ID;
        private TextBox txt_Name;
        
        // Panel Botones
        private Panel panel_Botones;
        private Button btn_Limpiar;
        private Button btn_agregar;
        private Button btn_Editar;
        private Button btn_Borrar;
    }
}