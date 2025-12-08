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
            table_Botones.SuspendLayout();
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
            lbl_Bienvenida.Dock = DockStyle.Fill;
            lbl_Bienvenida.Font = new Font("Times New Roman", 28F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lbl_Bienvenida.Location = new Point(0, 0);
            lbl_Bienvenida.Name = "lbl_Bienvenida";
            lbl_Bienvenida.Size = new Size(900, 100);
            lbl_Bienvenida.TabIndex = 0;
            lbl_Bienvenida.Text = "Sistema de Control de Stock";
            lbl_Bienvenida.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl_Subtitulo
            // 
            lbl_Subtitulo.Dock = DockStyle.Top;
            lbl_Subtitulo.Font = new Font("Segoe UI", 12F);
            lbl_Subtitulo.Location = new Point(20, 20);
            lbl_Subtitulo.Name = "lbl_Subtitulo";
            lbl_Subtitulo.Size = new Size(860, 50);
            lbl_Subtitulo.TabIndex = 1;
            lbl_Subtitulo.Text = "Seleccione una opción del menú para comenzar";
            lbl_Subtitulo.TextAlign = ContentAlignment.MiddleCenter;
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
            table_Botones.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            table_Botones.ColumnCount = 2;
            table_Botones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            table_Botones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            table_Botones.Controls.Add(btn_Productos, 0, 0);
            table_Botones.Controls.Add(btn_Categorias, 1, 0);
            table_Botones.Controls.Add(btn_Clientes, 0, 1);
            table_Botones.Controls.Add(btn_Eventos, 1, 1);
            table_Botones.Controls.Add(btn_Reservas, 0, 2);
            table_Botones.Location = new Point(20, 70);
            table_Botones.Name = "table_Botones";
            table_Botones.Padding = new Padding(10);
            table_Botones.RowCount = 3;
            table_Botones.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            table_Botones.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            table_Botones.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
            table_Botones.Size = new Size(860, 360);
            table_Botones.TabIndex = 2;
            // 
            // btn_Productos
            // 
            btn_Productos.Cursor = Cursors.Hand;
            btn_Productos.Dock = DockStyle.Fill;
            btn_Productos.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Productos.Location = new Point(30, 30);
            btn_Productos.Margin = new Padding(20);
            btn_Productos.Name = "btn_Productos";
            btn_Productos.Size = new Size(380, 73);
            btn_Productos.TabIndex = 0;
            btn_Productos.Text = "Gestión de Productos";
            btn_Productos.UseVisualStyleBackColor = true;
            btn_Productos.Click += buttonProducts_Click;
            // 
            // btn_Categorias
            // 
            btn_Categorias.Cursor = Cursors.Hand;
            btn_Categorias.Dock = DockStyle.Fill;
            btn_Categorias.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btn_Categorias.Location = new Point(450, 30);
            btn_Categorias.Margin = new Padding(20);
            btn_Categorias.Name = "btn_Categorias";
            btn_Categorias.Size = new Size(380, 73);
            btn_Categorias.TabIndex = 1;
            btn_Categorias.Text = "Gestión de Categorías";
            btn_Categorias.UseVisualStyleBackColor = true;
            btn_Categorias.Click += btnCategorias_Click;
            // 
            // btn_Clientes
            // 
            btn_Clientes.Cursor = Cursors.Hand;
            btn_Clientes.Dock = DockStyle.Fill;
            btn_Clientes.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btn_Clientes.Location = new Point(30, 143);
            btn_Clientes.Margin = new Padding(20);
            btn_Clientes.Name = "btn_Clientes";
            btn_Clientes.Size = new Size(380, 73);
            btn_Clientes.TabIndex = 2;
            btn_Clientes.Text = "Gestión de Clientes";
            btn_Clientes.UseVisualStyleBackColor = true;
            btn_Clientes.Click += btnClientes_Click;
            // 
            // btn_Eventos
            // 
            btn_Eventos.Cursor = Cursors.Hand;
            btn_Eventos.Dock = DockStyle.Fill;
            btn_Eventos.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btn_Eventos.Location = new Point(450, 143);
            btn_Eventos.Margin = new Padding(20);
            btn_Eventos.Name = "btn_Eventos";
            btn_Eventos.Size = new Size(380, 73);
            btn_Eventos.TabIndex = 3;
            btn_Eventos.Text = "Gestión de Eventos";
            btn_Eventos.UseVisualStyleBackColor = true;
            btn_Eventos.Click += btnEventos_Click;
            // 
            // btn_Reservas
            // 
            table_Botones.SetColumnSpan(btn_Reservas, 2);
            btn_Reservas.Cursor = Cursors.Hand;
            btn_Reservas.Dock = DockStyle.Fill;
            btn_Reservas.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btn_Reservas.Location = new Point(30, 256);
            btn_Reservas.Margin = new Padding(20);
            btn_Reservas.Name = "btn_Reservas";
            btn_Reservas.Size = new Size(800, 74);
            btn_Reservas.TabIndex = 4;
            btn_Reservas.Text = "Gestión de Reservas";
            btn_Reservas.UseVisualStyleBackColor = true;
            btn_Reservas.Click += btnReservas_Click;
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
            panel_Menu.ResumeLayout(false);
            table_Botones.ResumeLayout(false);
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