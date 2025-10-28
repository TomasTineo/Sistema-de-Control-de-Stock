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
            btn_Productos = new Button();
            btn_Categorias = new Button();
            btn_Eventos = new Button();
            btn_Reservas = new Button();
            panel_Footer = new Panel();
            lbl_Footer = new Label();
            panel_Header.SuspendLayout();
            panel_Menu.SuspendLayout();
            panel_Footer.SuspendLayout();
            SuspendLayout();
            // 
            // panel_Header
            // 
            panel_Header.BackColor = Color.FromArgb(41, 128, 185);
            panel_Header.Controls.Add(lbl_Bienvenida);
            panel_Header.Controls.Add(lbl_Subtitulo);
            panel_Header.Dock = DockStyle.Top;
            panel_Header.Location = new Point(0, 0);
            panel_Header.Name = "panel_Header";
            panel_Header.Size = new Size(900, 120);
            panel_Header.TabIndex = 0;
            // 
            // lbl_Bienvenida
            // 
            lbl_Bienvenida.AutoSize = true;
            lbl_Bienvenida.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lbl_Bienvenida.ForeColor = Color.White;
            lbl_Bienvenida.Location = new Point(30, 25);
            lbl_Bienvenida.Name = "lbl_Bienvenida";
            lbl_Bienvenida.Size = new Size(516, 45);
            lbl_Bienvenida.TabIndex = 0;
            lbl_Bienvenida.Text = "Sistema de Control de Stock";
            // 
            // lbl_Subtitulo
            // 
            lbl_Subtitulo.AutoSize = true;
            lbl_Subtitulo.Font = new Font("Segoe UI", 12F);
            lbl_Subtitulo.ForeColor = Color.White;
            lbl_Subtitulo.Location = new Point(35, 75);
            lbl_Subtitulo.Name = "lbl_Subtitulo";
            lbl_Subtitulo.Size = new Size(347, 21);
            lbl_Subtitulo.TabIndex = 1;
            lbl_Subtitulo.Text = "Seleccione una opción del menú para comenzar";
            // 
            // panel_Menu
            // 
            panel_Menu.BackColor = Color.FromArgb(236, 240, 241);
            panel_Menu.Controls.Add(btn_Productos);
            panel_Menu.Controls.Add(btn_Categorias);
            panel_Menu.Controls.Add(btn_Eventos);
            panel_Menu.Controls.Add(btn_Reservas);
            panel_Menu.Dock = DockStyle.Fill;
            panel_Menu.Location = new Point(0, 120);
            panel_Menu.Name = "panel_Menu";
            panel_Menu.Padding = new Padding(50);
            panel_Menu.Size = new Size(900, 430);
            panel_Menu.TabIndex = 1;
            // 
            // btn_Productos
            // 
            btn_Productos.BackColor = Color.FromArgb(52, 152, 219);
            btn_Productos.Cursor = Cursors.Hand;
            btn_Productos.FlatAppearance.BorderSize = 0;
            btn_Productos.FlatStyle = FlatStyle.Flat;
            btn_Productos.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btn_Productos.ForeColor = Color.White;
            btn_Productos.Location = new Point(100, 70);
            btn_Productos.Name = "btn_Productos";
            btn_Productos.Size = new Size(300, 70);
            btn_Productos.TabIndex = 0;
            btn_Productos.Text = "📦 Gestión de Productos";
            btn_Productos.UseVisualStyleBackColor = false;
            btn_Productos.Click += buttonProducts_Click;
            btn_Productos.MouseEnter += btn_MouseEnter;
            btn_Productos.MouseLeave += btn_MouseLeave;
            // 
            // btn_Categorias
            // 
            btn_Categorias.BackColor = Color.FromArgb(46, 204, 113);
            btn_Categorias.Cursor = Cursors.Hand;
            btn_Categorias.FlatAppearance.BorderSize = 0;
            btn_Categorias.FlatStyle = FlatStyle.Flat;
            btn_Categorias.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btn_Categorias.ForeColor = Color.White;
            btn_Categorias.Location = new Point(500, 70);
            btn_Categorias.Name = "btn_Categorias";
            btn_Categorias.Size = new Size(300, 70);
            btn_Categorias.TabIndex = 1;
            btn_Categorias.Text = "🏷️ Gestión de Categorías";
            btn_Categorias.UseVisualStyleBackColor = false;
            btn_Categorias.Click += btnCategorias_Click;
            btn_Categorias.MouseEnter += btn_MouseEnter;
            btn_Categorias.MouseLeave += btn_MouseLeave;
            // 
            // btn_Eventos
            // 
            btn_Eventos.BackColor = Color.FromArgb(155, 89, 182);
            btn_Eventos.Cursor = Cursors.Hand;
            btn_Eventos.FlatAppearance.BorderSize = 0;
            btn_Eventos.FlatStyle = FlatStyle.Flat;
            btn_Eventos.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btn_Eventos.ForeColor = Color.White;
            btn_Eventos.Location = new Point(100, 170);
            btn_Eventos.Name = "btn_Eventos";
            btn_Eventos.Size = new Size(300, 70);
            btn_Eventos.TabIndex = 2;
            btn_Eventos.Text = "📅 Gestión de Eventos";
            btn_Eventos.UseVisualStyleBackColor = false;
            btn_Eventos.Click += btnEventos_Click;
            btn_Eventos.MouseEnter += btn_MouseEnter;
            btn_Eventos.MouseLeave += btn_MouseLeave;
            // 
            // btn_Reservas
            // 
            btn_Reservas.BackColor = Color.FromArgb(230, 126, 34);
            btn_Reservas.Cursor = Cursors.Hand;
            btn_Reservas.FlatAppearance.BorderSize = 0;
            btn_Reservas.FlatStyle = FlatStyle.Flat;
            btn_Reservas.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btn_Reservas.ForeColor = Color.White;
            btn_Reservas.Location = new Point(500, 170);
            btn_Reservas.Name = "btn_Reservas";
            btn_Reservas.Size = new Size(300, 70);
            btn_Reservas.TabIndex = 3;
            btn_Reservas.Text = "📋 Gestión de Reservas";
            btn_Reservas.UseVisualStyleBackColor = false;
            btn_Reservas.Click += btnReservas_Click;
            btn_Reservas.MouseEnter += btn_MouseEnter;
            btn_Reservas.MouseLeave += btn_MouseLeave;
            // 
            // panel_Footer
            // 
            panel_Footer.BackColor = Color.FromArgb(44, 62, 80);
            panel_Footer.Controls.Add(lbl_Footer);
            panel_Footer.Dock = DockStyle.Bottom;
            panel_Footer.Location = new Point(0, 510);
            panel_Footer.Name = "panel_Footer";
            panel_Footer.Size = new Size(900, 40);
            panel_Footer.TabIndex = 2;
            // 
            // lbl_Footer
            // 
            lbl_Footer.Dock = DockStyle.Fill;
            lbl_Footer.Font = new Font("Segoe UI", 9F);
            lbl_Footer.ForeColor = Color.White;
            lbl_Footer.Location = new Point(0, 0);
            lbl_Footer.Name = "lbl_Footer";
            lbl_Footer.Size = new Size(900, 40);
            lbl_Footer.TabIndex = 0;
            lbl_Footer.Text = "© 2025 Sistema de Control de Stock - Versión 1.0";
            lbl_Footer.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 550);
            Controls.Add(panel_Footer);
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
            panel_Footer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel_Header;
        private Label lbl_Bienvenida;
        private Label lbl_Subtitulo;
        private Panel panel_Menu;
        private Button btn_Productos;
        private Button btn_Categorias;
        private Button btn_Eventos;
        private Button btn_Reservas;
        private Panel panel_Footer;
        private Label lbl_Footer;
    }
}