namespace Escritorio
{
    partial class Form_Reserva
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
            panel_Superior = new Panel();
            lbl_Titulo = new Label();
            txt_Buscar = new TextBox();
            btn_Refrescar = new Button();
            GrdVw_Reservas = new DataGridView();
            panel_Inferior = new Panel();
            lbl_TotalReservas = new Label();
            panel_Formulario = new Panel();
            lbl_FormularioTitulo = new Label();
            txt_ID = new TextBox();
            lbl_Cliente = new Label();
            cmb_Cliente = new ComboBox();
            lbl_Evento = new Label();
            cmb_Evento = new ComboBox();
            lbl_FechaReserva = new Label();
            dtp_FechaReserva = new DateTimePicker();
            lbl_Estado = new Label();
            cmb_Estado = new ComboBox();
            lbl_Productos = new Label();
            GrdVw_Productos = new DataGridView();
            panel_AgregarProducto = new Panel();
            cmb_Producto = new ComboBox();
            nud_Cantidad = new NumericUpDown();
            btn_AgregarProducto = new Button();
            btn_QuitarProducto = new Button();
            lbl_TotalTexto = new Label();
            lbl_TotalMonto = new Label();
            panel_Botones = new Panel();
            btn_Limpiar = new Button();
            btn_Crear = new Button();
            btn_Editar = new Button();
            btn_Eliminar = new Button();
            panel_Superior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GrdVw_Reservas).BeginInit();
            panel_Inferior.SuspendLayout();
            panel_Formulario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GrdVw_Productos).BeginInit();
            panel_AgregarProducto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nud_Cantidad).BeginInit();
            panel_Botones.SuspendLayout();
            SuspendLayout();
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
            panel_Superior.Size = new Size(1384, 70);
            panel_Superior.TabIndex = 0;
            // 
            // lbl_Titulo
            // 
            lbl_Titulo.AutoSize = true;
            lbl_Titulo.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lbl_Titulo.Location = new Point(13, 13);
            lbl_Titulo.Name = "lbl_Titulo";
            lbl_Titulo.Size = new Size(158, 21);
            lbl_Titulo.TabIndex = 0;
            lbl_Titulo.Text = "Gestión de Reservas";
            // 
            // txt_Buscar
            // 
            txt_Buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_Buscar.Location = new Point(1034, 20);
            txt_Buscar.Name = "txt_Buscar";
            txt_Buscar.PlaceholderText = "Buscar reserva...";
            txt_Buscar.Size = new Size(250, 23);
            txt_Buscar.TabIndex = 1;
            // 
            // btn_Refrescar
            // 
            btn_Refrescar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_Refrescar.Location = new Point(1294, 17);
            btn_Refrescar.Name = "btn_Refrescar";
            btn_Refrescar.Size = new Size(80, 30);
            btn_Refrescar.TabIndex = 2;
            btn_Refrescar.Text = "Refrescar";
            btn_Refrescar.UseVisualStyleBackColor = true;
            btn_Refrescar.Click += btnRefrescar_Click;
            // 
            // GrdVw_Reservas
            // 
            GrdVw_Reservas.AllowUserToAddRows = false;
            GrdVw_Reservas.AllowUserToDeleteRows = false;
            GrdVw_Reservas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GrdVw_Reservas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GrdVw_Reservas.Dock = DockStyle.Fill;
            GrdVw_Reservas.Location = new Point(0, 70);
            GrdVw_Reservas.MultiSelect = false;
            GrdVw_Reservas.Name = "GrdVw_Reservas";
            GrdVw_Reservas.ReadOnly = true;
            GrdVw_Reservas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GrdVw_Reservas.Size = new Size(834, 611);
            GrdVw_Reservas.TabIndex = 3;
            GrdVw_Reservas.CellDoubleClick += GrdVw_Reservas_CellDoubleClick;
            // 
            // panel_Inferior
            // 
            panel_Inferior.Controls.Add(lbl_TotalReservas);
            panel_Inferior.Dock = DockStyle.Bottom;
            panel_Inferior.Location = new Point(0, 681);
            panel_Inferior.Name = "panel_Inferior";
            panel_Inferior.Padding = new Padding(10);
            panel_Inferior.Size = new Size(834, 40);
            panel_Inferior.TabIndex = 4;
            // 
            // lbl_TotalReservas
            // 
            lbl_TotalReservas.AutoSize = true;
            lbl_TotalReservas.Location = new Point(13, 12);
            lbl_TotalReservas.Name = "lbl_TotalReservas";
            lbl_TotalReservas.Size = new Size(97, 15);
            lbl_TotalReservas.TabIndex = 0;
            lbl_TotalReservas.Text = "Total: 0 reserva(s)";
            // 
            // panel_Formulario
            // 
            panel_Formulario.BorderStyle = BorderStyle.FixedSingle;
            panel_Formulario.Controls.Add(lbl_FormularioTitulo);
            panel_Formulario.Controls.Add(txt_ID);
            panel_Formulario.Controls.Add(lbl_Cliente);
            panel_Formulario.Controls.Add(cmb_Cliente);
            panel_Formulario.Controls.Add(lbl_Evento);
            panel_Formulario.Controls.Add(cmb_Evento);
            panel_Formulario.Controls.Add(lbl_FechaReserva);
            panel_Formulario.Controls.Add(dtp_FechaReserva);
            panel_Formulario.Controls.Add(lbl_Estado);
            panel_Formulario.Controls.Add(cmb_Estado);
            panel_Formulario.Controls.Add(lbl_Productos);
            panel_Formulario.Controls.Add(GrdVw_Productos);
            panel_Formulario.Controls.Add(panel_AgregarProducto);
            panel_Formulario.Controls.Add(lbl_TotalTexto);
            panel_Formulario.Controls.Add(lbl_TotalMonto);
            panel_Formulario.Controls.Add(panel_Botones);
            panel_Formulario.Dock = DockStyle.Right;
            panel_Formulario.Location = new Point(834, 70);
            panel_Formulario.Name = "panel_Formulario";
            panel_Formulario.Padding = new Padding(15);
            panel_Formulario.Size = new Size(550, 651);
            panel_Formulario.TabIndex = 5;
            // 
            // lbl_FormularioTitulo
            // 
            lbl_FormularioTitulo.AutoSize = true;
            lbl_FormularioTitulo.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lbl_FormularioTitulo.Location = new Point(18, 15);
            lbl_FormularioTitulo.Name = "lbl_FormularioTitulo";
            lbl_FormularioTitulo.Size = new Size(132, 19);
            lbl_FormularioTitulo.TabIndex = 0;
            lbl_FormularioTitulo.Text = "Datos de la Reserva";
            // 
            // txt_ID
            // 
            txt_ID.Location = new Point(18, 45);
            txt_ID.Name = "txt_ID";
            txt_ID.PlaceholderText = "ID (auto)";
            txt_ID.ReadOnly = true;
            txt_ID.Size = new Size(100, 23);
            txt_ID.TabIndex = 1;
            // 
            // lbl_Cliente
            // 
            lbl_Cliente.AutoSize = true;
            lbl_Cliente.Location = new Point(18, 75);
            lbl_Cliente.Name = "lbl_Cliente";
            lbl_Cliente.Size = new Size(47, 15);
            lbl_Cliente.TabIndex = 2;
            lbl_Cliente.Text = "Cliente:";
            // 
            // cmb_Cliente
            // 
            cmb_Cliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_Cliente.FormattingEnabled = true;
            cmb_Cliente.Location = new Point(18, 95);
            cmb_Cliente.Name = "cmb_Cliente";
            cmb_Cliente.Size = new Size(510, 23);
            cmb_Cliente.TabIndex = 3;
            // 
            // lbl_Evento
            // 
            lbl_Evento.AutoSize = true;
            lbl_Evento.Location = new Point(18, 125);
            lbl_Evento.Name = "lbl_Evento";
            lbl_Evento.Size = new Size(46, 15);
            lbl_Evento.TabIndex = 4;
            lbl_Evento.Text = "Evento:";
            // 
            // cmb_Evento
            // 
            cmb_Evento.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_Evento.FormattingEnabled = true;
            cmb_Evento.Location = new Point(18, 145);
            cmb_Evento.Name = "cmb_Evento";
            cmb_Evento.Size = new Size(510, 23);
            cmb_Evento.TabIndex = 5;
            // 
            // lbl_FechaReserva
            // 
            lbl_FechaReserva.AutoSize = true;
            lbl_FechaReserva.Location = new Point(18, 175);
            lbl_FechaReserva.Name = "lbl_FechaReserva";
            lbl_FechaReserva.Size = new Size(110, 15);
            lbl_FechaReserva.TabIndex = 6;
            lbl_FechaReserva.Text = "Fecha Finalización:";
            // 
            // dtp_FechaReserva
            // 
            dtp_FechaReserva.Format = DateTimePickerFormat.Short;
            dtp_FechaReserva.Location = new Point(18, 195);
            dtp_FechaReserva.Name = "dtp_FechaReserva";
            dtp_FechaReserva.Size = new Size(250, 23);
            dtp_FechaReserva.TabIndex = 7;
            // 
            // lbl_Estado
            // 
            lbl_Estado.AutoSize = true;
            lbl_Estado.Location = new Point(278, 175);
            lbl_Estado.Name = "lbl_Estado";
            lbl_Estado.Size = new Size(45, 15);
            lbl_Estado.TabIndex = 8;
            lbl_Estado.Text = "Estado:";
            // 
            // cmb_Estado
            // 
            cmb_Estado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_Estado.FormattingEnabled = true;
            cmb_Estado.Location = new Point(278, 195);
            cmb_Estado.Name = "cmb_Estado";
            cmb_Estado.Size = new Size(250, 23);
            cmb_Estado.TabIndex = 9;
            // 
            // lbl_Productos
            // 
            lbl_Productos.AutoSize = true;
            lbl_Productos.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lbl_Productos.Location = new Point(18, 225);
            lbl_Productos.Name = "lbl_Productos";
            lbl_Productos.Size = new Size(64, 15);
            lbl_Productos.TabIndex = 10;
            lbl_Productos.Text = "Productos:";
            // 
            // GrdVw_Productos
            // 
            GrdVw_Productos.AllowUserToAddRows = false;
            GrdVw_Productos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GrdVw_Productos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GrdVw_Productos.Location = new Point(18, 245);
            GrdVw_Productos.MultiSelect = false;
            GrdVw_Productos.Name = "GrdVw_Productos";
            GrdVw_Productos.ReadOnly = true;
            GrdVw_Productos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GrdVw_Productos.Size = new Size(510, 150);
            GrdVw_Productos.TabIndex = 11;
            // 
            // panel_AgregarProducto
            // 
            panel_AgregarProducto.Controls.Add(cmb_Producto);
            panel_AgregarProducto.Controls.Add(nud_Cantidad);
            panel_AgregarProducto.Controls.Add(btn_AgregarProducto);
            panel_AgregarProducto.Controls.Add(btn_QuitarProducto);
            panel_AgregarProducto.Location = new Point(18, 400);
            panel_AgregarProducto.Name = "panel_AgregarProducto";
            panel_AgregarProducto.Size = new Size(510, 35);
            panel_AgregarProducto.TabIndex = 12;
            // 
            // cmb_Producto
            // 
            cmb_Producto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_Producto.FormattingEnabled = true;
            cmb_Producto.Location = new Point(0, 5);
            cmb_Producto.Name = "cmb_Producto";
            cmb_Producto.Size = new Size(250, 23);
            cmb_Producto.TabIndex = 0;
            // 
            // nud_Cantidad
            // 
            nud_Cantidad.Location = new Point(260, 5);
            nud_Cantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nud_Cantidad.Name = "nud_Cantidad";
            nud_Cantidad.Size = new Size(80, 23);
            nud_Cantidad.TabIndex = 1;
            nud_Cantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btn_AgregarProducto
            // 
            btn_AgregarProducto.Location = new Point(350, 3);
            btn_AgregarProducto.Name = "btn_AgregarProducto";
            btn_AgregarProducto.Size = new Size(75, 27);
            btn_AgregarProducto.TabIndex = 2;
            btn_AgregarProducto.Text = "Agregar";
            btn_AgregarProducto.UseVisualStyleBackColor = true;
            btn_AgregarProducto.Click += btnAgregarProducto_Click;
            // 
            // btn_QuitarProducto
            // 
            btn_QuitarProducto.Location = new Point(435, 3);
            btn_QuitarProducto.Name = "btn_QuitarProducto";
            btn_QuitarProducto.Size = new Size(75, 27);
            btn_QuitarProducto.TabIndex = 3;
            btn_QuitarProducto.Text = "Quitar";
            btn_QuitarProducto.UseVisualStyleBackColor = true;
            btn_QuitarProducto.Click += btnQuitarProducto_Click;
            // 
            // lbl_TotalTexto
            // 
            lbl_TotalTexto.AutoSize = true;
            lbl_TotalTexto.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lbl_TotalTexto.Location = new Point(18, 445);
            lbl_TotalTexto.Name = "lbl_TotalTexto";
            lbl_TotalTexto.Size = new Size(43, 19);
            lbl_TotalTexto.TabIndex = 13;
            lbl_TotalTexto.Text = "Total:";
            // 
            // lbl_TotalMonto
            // 
            lbl_TotalMonto.AutoSize = true;
            lbl_TotalMonto.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lbl_TotalMonto.ForeColor = Color.Green;
            lbl_TotalMonto.Location = new Point(68, 445);
            lbl_TotalMonto.Name = "lbl_TotalMonto";
            lbl_TotalMonto.Size = new Size(45, 19);
            lbl_TotalMonto.TabIndex = 14;
            lbl_TotalMonto.Text = "$0.00";
            // 
            // panel_Botones
            // 
            panel_Botones.Controls.Add(btn_Limpiar);
            panel_Botones.Controls.Add(btn_Crear);
            panel_Botones.Controls.Add(btn_Editar);
            panel_Botones.Controls.Add(btn_Eliminar);
            panel_Botones.Dock = DockStyle.Bottom;
            panel_Botones.Location = new Point(15, 471);
            panel_Botones.Name = "panel_Botones";
            panel_Botones.Size = new Size(518, 163);
            panel_Botones.TabIndex = 15;
            // 
            // btn_Limpiar
            // 
            btn_Limpiar.Dock = DockStyle.Top;
            btn_Limpiar.Location = new Point(0, 120);
            btn_Limpiar.Margin = new Padding(0, 0, 0, 5);
            btn_Limpiar.Name = "btn_Limpiar";
            btn_Limpiar.Size = new Size(518, 40);
            btn_Limpiar.TabIndex = 3;
            btn_Limpiar.Text = "Limpiar Campos";
            btn_Limpiar.UseVisualStyleBackColor = true;
            btn_Limpiar.Click += btnLimpiar_Click;
            // 
            // btn_Crear
            // 
            btn_Crear.Dock = DockStyle.Top;
            btn_Crear.Location = new Point(0, 80);
            btn_Crear.Margin = new Padding(0, 5, 0, 5);
            btn_Crear.Name = "btn_Crear";
            btn_Crear.Size = new Size(518, 40);
            btn_Crear.TabIndex = 0;
            btn_Crear.Text = "Crear Reserva";
            btn_Crear.UseVisualStyleBackColor = true;
            btn_Crear.Click += btnCrear_Click;
            // 
            // btn_Editar
            // 
            btn_Editar.Dock = DockStyle.Top;
            btn_Editar.Location = new Point(0, 40);
            btn_Editar.Margin = new Padding(0, 5, 0, 5);
            btn_Editar.Name = "btn_Editar";
            btn_Editar.Size = new Size(518, 40);
            btn_Editar.TabIndex = 1;
            btn_Editar.Text = "Editar Reserva";
            btn_Editar.UseVisualStyleBackColor = true;
            btn_Editar.Click += btnEditar_Click;
            // 
            // btn_Eliminar
            // 
            btn_Eliminar.Dock = DockStyle.Top;
            btn_Eliminar.Location = new Point(0, 0);
            btn_Eliminar.Margin = new Padding(0, 5, 0, 0);
            btn_Eliminar.Name = "btn_Eliminar";
            btn_Eliminar.Size = new Size(518, 40);
            btn_Eliminar.TabIndex = 2;
            btn_Eliminar.Text = "Eliminar Reserva";
            btn_Eliminar.UseVisualStyleBackColor = true;
            btn_Eliminar.Click += btnEliminar_Click;
            // 
            // Form_Reserva
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1384, 721);
            Controls.Add(GrdVw_Reservas);
            Controls.Add(panel_Inferior);
            Controls.Add(panel_Formulario);
            Controls.Add(panel_Superior);
            MinimumSize = new Size(1400, 760);
            Name = "Form_Reserva";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Control de Stock - Reservas";
            Load += Form_Reserva_Load;
            panel_Superior.ResumeLayout(false);
            panel_Superior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GrdVw_Reservas).EndInit();
            panel_Inferior.ResumeLayout(false);
            panel_Inferior.PerformLayout();
            panel_Formulario.ResumeLayout(false);
            panel_Formulario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GrdVw_Productos).EndInit();
            panel_AgregarProducto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nud_Cantidad).EndInit();
            panel_Botones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        // Panel Superior
        private Panel panel_Superior;
        private Label lbl_Titulo;
        private TextBox txt_Buscar;
        private Button btn_Refrescar;
        
        // DataGridView Reservas
        private DataGridView GrdVw_Reservas;
        
        // Panel Inferior
        private Panel panel_Inferior;
        private Label lbl_TotalReservas;
        
        // Panel Formulario
        private Panel panel_Formulario;
        private Label lbl_FormularioTitulo;
        private TextBox txt_ID;
        private Label lbl_Cliente;
        private ComboBox cmb_Cliente;
        private Label lbl_Evento;
        private ComboBox cmb_Evento;
        private Label lbl_FechaReserva;
        private DateTimePicker dtp_FechaReserva;
        private Label lbl_Estado;
        private ComboBox cmb_Estado;
        private Label lbl_Productos;
        private DataGridView GrdVw_Productos;
        
        // Panel agregar producto
        private Panel panel_AgregarProducto;
        private ComboBox cmb_Producto;
        private NumericUpDown nud_Cantidad;
        private Button btn_AgregarProducto;
        private Button btn_QuitarProducto;
        
        // Total
        private Label lbl_TotalTexto;
        private Label lbl_TotalMonto;
        
        // Panel Botones
        private Panel panel_Botones;
        private Button btn_Limpiar;
        private Button btn_Crear;
        private Button btn_Editar;
        private Button btn_Eliminar;
    }
}