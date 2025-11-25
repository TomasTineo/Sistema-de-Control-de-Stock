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
            panel_Header.Size = new Size(900, 120);
            panel_Header.TabIndex = 0;
            // 
            // lbl_Bienvenida
            // 
            lbl_Bienvenida.AutoSize = true;
            lbl_Bienvenida.BorderStyle = BorderStyle.Fixed3D;
            lbl_Bienvenida.Font = new Font("Times New Roman", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lbl_Bienvenida.Location = new Point(167, 33);
            lbl_Bienvenida.Name = "lbl_Bienvenida";
            lbl_Bienvenida.Size = new Size(580, 56);
            lbl_Bienvenida.TabIndex = 0;
            lbl_Bienvenida.Text = "Sistema de Control de Stock";
            lbl_Bienvenida.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl_Subtitulo
            // 
            lbl_Subtitulo.AutoSize = true;
            lbl_Subtitulo.Font = new Font("Segoe UI", 12F);
            lbl_Subtitulo.Location = new Point(53, 15);
            lbl_Subtitulo.Name = "lbl_Subtitulo";
            lbl_Subtitulo.Size = new Size(340, 21);
            lbl_Subtitulo.TabIndex = 1;
            lbl_Subtitulo.Text = "Seleccione una opción del menú para comenzar";
            // 
            // panel_Menu
            // 
            panel_Menu.Controls.Add(btn_Productos);
            panel_Menu.Controls.Add(lbl_Subtitulo);
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
            btn_Productos.Cursor = Cursors.Hand;
            btn_Productos.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Productos.Location = new Point(100, 84);
            btn_Productos.Name = "btn_Productos";
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
            btn_Categorias.Location = new Point(500, 84);
            btn_Categorias.Name = "btn_Categorias";
            btn_Categorias.Size = new Size(300, 70);
            btn_Categorias.TabIndex = 1;
            btn_Categorias.Text = "Gestión de Categorías";
            btn_Categorias.UseVisualStyleBackColor = true;
            btn_Categorias.Click += btnCategorias_Click;
            // 
            // btn_Eventos
            // 
            btn_Eventos.Cursor = Cursors.Hand;
            btn_Eventos.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btn_Eventos.Location = new Point(100, 240);
            btn_Eventos.Name = "btn_Eventos";
            btn_Eventos.Size = new Size(300, 70);
            btn_Eventos.TabIndex = 2;
            btn_Eventos.Text = "Gestión de Eventos";
            btn_Eventos.UseVisualStyleBackColor = true;
            btn_Eventos.Click += btnEventos_Click;
            // 
            // btn_Reservas
            // 
            btn_Reservas.Cursor = Cursors.Hand;
            btn_Reservas.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btn_Reservas.Location = new Point(500, 240);
            btn_Reservas.Name = "btn_Reservas";
            btn_Reservas.Size = new Size(300, 70);
            btn_Reservas.TabIndex = 3;
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
            panel_Header.PerformLayout();
            panel_Menu.ResumeLayout(false);
            panel_Menu.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel_Header;
        private Label lbl_Subtitulo;
        private Panel panel_Menu;
        private Button btn_Productos;
        private Button btn_Categorias;
        private Button btn_Eventos;
        private Button btn_Reservas;
        private Label lbl_Bienvenida;
    }
}