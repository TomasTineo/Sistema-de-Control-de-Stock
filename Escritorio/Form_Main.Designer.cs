namespace Escritorio
{
    partial class Form_Main
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
            panel_Header = new Panel();
            lbl_Bienvenida = new Label();
            lbl_Subtitulo = new Label();
            panel_Menu = new Panel();
            table_Botones = new TableLayoutPanel();
            btn_Productos = new Button();
            btn_Categorias = new Button();
            btn_Clientes = new Button();
            btn_Eventos = new Button();
            btn_Reservas = new Button();
            panel_Header.SuspendLayout();
            panel_Menu.SuspendLayout();
            SuspendLayout();
            // 
            // panel_Header
            // 
            panel_Header.Controls.Add(lbl_Bienvenida);
            panel_Header.Dock = DockStyle.Top;
            panel_Header.Location = new Point(0, 0);
            panel_Header.Name = "panel_Header";
            panel_Header.Size = new Size(900, 100);
            panel_Header.TabIndex = 0;
            // 
            // lbl_Bienvenida
            // 
            lbl_Bienvenida.AutoSize = true;
            lbl_Bienvenida.BorderStyle = BorderStyle.Fixed3D;
            lbl_Bienvenida.Font = new Font("Times New Roman", 28F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lbl_Bienvenida.Location = new Point(150, 24);
            lbl_Bienvenida.Name = "lbl_Bienvenida";
            lbl_Bienvenida.Size = new Size(466, 46);
            lbl_Bienvenida.TabIndex = 0;
            lbl_Bienvenida.Text = "Sistema de Control de Stock";
            lbl_Bienvenida.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl_Subtitulo
            // 
            lbl_Subtitulo.Anchor = AnchorStyles.Top;
            lbl_Subtitulo.AutoSize = true;
            lbl_Subtitulo.Font = new Font("Segoe UI", 12F);
            lbl_Subtitulo.Location = new Point(230, 20);
            lbl_Subtitulo.Name = "lbl_Subtitulo";
            lbl_Subtitulo.Size = new Size(340, 21);
            lbl_Subtitulo.TabIndex = 1;
            lbl_Subtitulo.Text = "Seleccione una opción del menú para comenzar";
            // 
            // panel_Menu
            // 
            panel_Menu.Controls.Add(table_Botones);
            panel_Menu.Controls.Add(lbl_Subtitulo);
            panel_Menu.Dock = DockStyle.Fill;
            panel_Menu.Location = new Point(0, 100);
            panel_Menu.Name = "panel_Menu";
            panel_Menu.Padding = new Padding(20);
            panel_Menu.Size = new Size(900, 450);
            panel_Menu.TabIndex = 1;
            // 
            // table_Botones
            // 
            table_Botones.Anchor = AnchorStyles.Top;
            table_Botones.ColumnCount = 2;
            table_Botones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            table_Botones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            table_Botones.RowCount = 3;
            table_Botones.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            table_Botones.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            table_Botones.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            table_Botones.Location = new Point(100, 60);
            table_Botones.Name = "table_Botones";
            table_Botones.Padding = new Padding(10);
            table_Botones.Size = new Size(700, 320);
            table_Botones.TabIndex = 2;
            // 
            // btn_Productos
            // 
            btn_Productos.Cursor = Cursors.Hand;
            btn_Productos.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Productos.Margin = new Padding(20);
            btn_Productos.Size = new Size(300, 70);
            btn_Productos.TabIndex = 0;
            btn_Productos.Text = "Gestión de Productos";
            btn_Productos.UseVisualStyleBackColor = true;
            btn_Productos.Click += buttonProducts_Click;
            // 
            // btn_Categorias
            // 
            btn_Categorias.Cursor = Cursors.Hand;
            btn_Categorias.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btn_Categorias.Margin = new Padding(20);
            btn_Categorias.Size = new Size(300, 70);
            btn_Categorias.TabIndex = 1;
            btn_Categorias.Text = "Gestión de Categorías";
            btn_Categorias.UseVisualStyleBackColor = true;
            btn_Categorias.Click += btnCategorias_Click;
            // 
            // btn_Clientes
            // 
            btn_Clientes.Cursor = Cursors.Hand;
            btn_Clientes.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btn_Clientes.Margin = new Padding(20);
            btn_Clientes.Size = new Size(300, 70);
            btn_Clientes.TabIndex = 2;
            btn_Clientes.Text = "Gestión de Clientes";
            btn_Clientes.UseVisualStyleBackColor = true;
            btn_Clientes.Click += btnClientes_Click;
            // 
            // btn_Eventos
            // 
            btn_Eventos.Cursor = Cursors.Hand;
            btn_Eventos.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btn_Eventos.Margin = new Padding(20);
            btn_Eventos.Size = new Size(300, 70);
            btn_Eventos.TabIndex = 3;
            btn_Eventos.Text = "Gestión de Eventos";
            btn_Eventos.UseVisualStyleBackColor = true;
            btn_Eventos.Click += btnEventos_Click;
            // 
            // btn_Reservas
            // 
            btn_Reservas.Anchor = AnchorStyles.Top;
            btn_Reservas.Cursor = Cursors.Hand;
            btn_Reservas.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btn_Reservas.Margin = new Padding(20);
            btn_Reservas.Size = new Size(300, 70);
            btn_Reservas.TabIndex = 4;
            btn_Reservas.Text = "Gestión de Reservas";
            btn_Reservas.UseVisualStyleBackColor = true;
            btn_Reservas.Click += btnReservas_Click;
            // 
            // add controls to table
            // 
            table_Botones.Controls.Add(btn_Productos, 0, 0);
            table_Botones.Controls.Add(btn_Categorias, 1, 0);
            table_Botones.Controls.Add(btn_Clientes, 0, 1);
            table_Botones.Controls.Add(btn_Eventos, 1, 1);
            table_Botones.Controls.Add(btn_Reservas, 0, 2);
            table_Botones.SetColumnSpan(btn_Reservas, 2);
            // center reservas in its row
            btn_Reservas.Anchor = AnchorStyles.Top;

            // 
            // Form_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 550);
            Controls.Add(panel_Menu);
            Controls.Add(panel_Header);
            MinimumSize = new Size(916, 589);
            Name = "Form_Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Control de Stock - Menú Principal";
            Load += FormMain_Load;
            panel_Header.ResumeLayout(false);
            panel_Header.PerformLayout();
            panel_Menu.ResumeLayout(false);
            panel_Menu.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel_Header;
        private Label lbl_Subtitulo;
        private Panel panel_Menu;
        private TableLayoutPanel table_Botones;
        private Button btn_Productos;
        private Button btn_Categorias;
        private Button btn_Clientes;
        private Button btn_Eventos;
        private Button btn_Reservas;
        private Label lbl_Bienvenida;
    }
}