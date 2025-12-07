namespace Escritorio
{
    partial class Form_Clientes
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panel_Superior = new Panel();
            lbl_Titulo = new Label();
            txtBuscar = new TextBox();
            btnRefrescar = new Button();
            dgvClientes = new DataGridView();
            panel_Inferior = new Panel();
            lbl_TotalClientes = new Label();
            panel_Formulario = new Panel();
            lbl_FormularioTitulo = new Label();
            txtId = new TextBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblApellido = new Label();
            txtApellido = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblTelefono = new Label();
            txtTelefono = new TextBox();
            lblDireccion = new Label();
            txtDireccion = new TextBox();
            panel_Botones = new Panel();
            btnLimpiar = new Button();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            panel_Superior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            panel_Inferior.SuspendLayout();
            panel_Formulario.SuspendLayout();
            panel_Botones.SuspendLayout();
            SuspendLayout();
            // 
            // panel_Superior
            // 
            panel_Superior.Controls.Add(lbl_Titulo);
            panel_Superior.Controls.Add(txtBuscar);
            panel_Superior.Controls.Add(btnRefrescar);
            panel_Superior.Dock = DockStyle.Top;
            panel_Superior.Location = new Point(0, 0);
            panel_Superior.Name = "panel_Superior";
            panel_Superior.Padding = new Padding(10);
            panel_Superior.Size = new Size(1064, 70);
            panel_Superior.TabIndex = 0;
            panel_Superior.Paint += panel_Superior_Paint;
            // 
            // lbl_Titulo
            // 
            lbl_Titulo.AutoSize = true;
            lbl_Titulo.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lbl_Titulo.Location = new Point(13, 13);
            lbl_Titulo.Name = "lbl_Titulo";
            lbl_Titulo.Size = new Size(151, 21);
            lbl_Titulo.TabIndex = 0;
            lbl_Titulo.Text = "Gestión de Clientes";
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBuscar.Location = new Point(704, 20);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar cliente...";
            txtBuscar.Size = new Size(250, 23);
            txtBuscar.TabIndex = 1;
            txtBuscar.Enter += btnBuscar_Click;
            // 
            // btnRefrescar
            // 
            btnRefrescar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefrescar.Location = new Point(964, 17);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(80, 30);
            btnRefrescar.TabIndex = 2;
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.UseVisualStyleBackColor = true;
            btnRefrescar.Click += btnLimpiar_Click;
            // 
            // dgvClientes
            // 
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Location = new Point(0, 70);
            dgvClientes.MultiSelect = false;
            dgvClientes.Name = "dgvClientes";
            dgvClientes.ReadOnly = true;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.Size = new Size(553, 562);
            dgvClientes.TabIndex = 3;
            dgvClientes.CellContentClick += dgvClientes_CellContentClick;
            dgvClientes.CellDoubleClick += dgvClientes_CellDoubleClick;
            // 
            // panel_Inferior
            // 
            panel_Inferior.Controls.Add(lbl_TotalClientes);
            panel_Inferior.Dock = DockStyle.Bottom;
            panel_Inferior.Location = new Point(0, 632);
            panel_Inferior.Name = "panel_Inferior";
            panel_Inferior.Padding = new Padding(10);
            panel_Inferior.Size = new Size(562, 39);
            panel_Inferior.TabIndex = 4;
            // 
            // lbl_TotalClientes
            // 
            lbl_TotalClientes.AutoSize = true;
            lbl_TotalClientes.Location = new Point(13, 12);
            lbl_TotalClientes.Name = "lbl_TotalClientes";
            lbl_TotalClientes.Size = new Size(95, 15);
            lbl_TotalClientes.TabIndex = 0;
            lbl_TotalClientes.Text = "Total: 0 cliente(s)";
            // 
            // panel_Formulario
            // 
            panel_Formulario.BorderStyle = BorderStyle.FixedSingle;
            panel_Formulario.Controls.Add(lbl_FormularioTitulo);
            panel_Formulario.Controls.Add(txtId);
            panel_Formulario.Controls.Add(lblNombre);
            panel_Formulario.Controls.Add(txtNombre);
            panel_Formulario.Controls.Add(lblApellido);
            panel_Formulario.Controls.Add(txtApellido);
            panel_Formulario.Controls.Add(lblEmail);
            panel_Formulario.Controls.Add(txtEmail);
            panel_Formulario.Controls.Add(lblTelefono);
            panel_Formulario.Controls.Add(txtTelefono);
            panel_Formulario.Controls.Add(lblDireccion);
            panel_Formulario.Controls.Add(txtDireccion);
            panel_Formulario.Controls.Add(panel_Botones);
            panel_Formulario.Dock = DockStyle.Right;
            panel_Formulario.Location = new Point(562, 70);
            panel_Formulario.Name = "panel_Formulario";
            panel_Formulario.Padding = new Padding(15);
            panel_Formulario.Size = new Size(502, 601);
            panel_Formulario.TabIndex = 5;
            // 
            // lbl_FormularioTitulo
            // 
            lbl_FormularioTitulo.AutoSize = true;
            lbl_FormularioTitulo.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lbl_FormularioTitulo.Location = new Point(18, 15);
            lbl_FormularioTitulo.Name = "lbl_FormularioTitulo";
            lbl_FormularioTitulo.Size = new Size(116, 19);
            lbl_FormularioTitulo.TabIndex = 0;
            lbl_FormularioTitulo.Text = "Datos del Cliente";
            // 
            // txtId
            // 
            txtId.Location = new Point(18, 45);
            txtId.Name = "txtId";
            txtId.PlaceholderText = "ID (auto)";
            txtId.ReadOnly = true;
            txtId.Size = new Size(100, 23);
            txtId.TabIndex = 1;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(18, 75);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(54, 15);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(18, 95);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(450, 23);
            txtNombre.TabIndex = 3;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(18, 125);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(54, 15);
            lblApellido.TabIndex = 4;
            lblApellido.Text = "Apellido:";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(18, 145);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(450, 23);
            txtApellido.TabIndex = 5;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(18, 175);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(39, 15);
            lblEmail.TabIndex = 6;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(18, 195);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(450, 23);
            txtEmail.TabIndex = 7;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(18, 225);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(55, 15);
            lblTelefono.TabIndex = 8;
            lblTelefono.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(18, 245);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(450, 23);
            txtTelefono.TabIndex = 9;
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblDireccion.Location = new Point(18, 275);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(61, 15);
            lblDireccion.TabIndex = 10;
            lblDireccion.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(18, 295);
            txtDireccion.Multiline = true;
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(450, 23);
            txtDireccion.TabIndex = 11;
            // 
            // panel_Botones
            // 
            panel_Botones.Controls.Add(btnLimpiar);
            panel_Botones.Controls.Add(btnAgregar);
            panel_Botones.Controls.Add(btnModificar);
            panel_Botones.Controls.Add(btnEliminar);
            panel_Botones.Dock = DockStyle.Bottom;
            panel_Botones.Location = new Point(15, 421);
            panel_Botones.Name = "panel_Botones";
            panel_Botones.Size = new Size(470, 163);
            panel_Botones.TabIndex = 15;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Dock = DockStyle.Top;
            btnLimpiar.Location = new Point(0, 120);
            btnLimpiar.Margin = new Padding(0, 0, 0, 5);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(470, 40);
            btnLimpiar.TabIndex = 3;
            btnLimpiar.Text = "Limpiar Campos";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.Dock = DockStyle.Top;
            btnAgregar.Location = new Point(0, 80);
            btnAgregar.Margin = new Padding(0, 5, 0, 5);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(470, 40);
            btnAgregar.TabIndex = 0;
            btnAgregar.Text = "Crear Cliente";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Dock = DockStyle.Top;
            btnModificar.Location = new Point(0, 40);
            btnModificar.Margin = new Padding(0, 5, 0, 5);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(470, 40);
            btnModificar.TabIndex = 1;
            btnModificar.Text = "Editar Cliente";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Dock = DockStyle.Top;
            btnEliminar.Location = new Point(0, 0);
            btnEliminar.Margin = new Padding(0, 5, 0, 0);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(470, 40);
            btnEliminar.TabIndex = 2;
            btnEliminar.Text = "Eliminar Cliente";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // Form_Clientes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1064, 671);
            Controls.Add(panel_Inferior);
            Controls.Add(panel_Formulario);
            Controls.Add(panel_Superior);
            Controls.Add(dgvClientes);
            MinimumSize = new Size(1080, 710);
            Name = "Form_Clientes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Control de Stock - Clientes";
            Load += Form_Clientes_Load;
            panel_Superior.ResumeLayout(false);
            panel_Superior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            panel_Inferior.ResumeLayout(false);
            panel_Inferior.PerformLayout();
            panel_Formulario.ResumeLayout(false);
            panel_Formulario.PerformLayout();
            panel_Botones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel_Superior;
        private System.Windows.Forms.Label lbl_Titulo;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.Panel panel_Inferior;
        private System.Windows.Forms.Label lbl_TotalClientes;
        private System.Windows.Forms.Panel panel_Formulario;
        private System.Windows.Forms.Label lbl_FormularioTitulo;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Panel panel_Botones;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
    }
}