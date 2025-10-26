namespace Escritorio
{
    partial class Form_Eventos

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
            eventosInMemoryBindingSource = new BindingSource(components);
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            btn_Borrar = new Button();
            txt_Name = new TextBox();
            GrdVw_Evento = new DataGridView();
            btn_agregar = new Button();
            btn_Editar = new Button();
            txt_ID = new TextBox();

            Evento_ID = new DataGridViewTextBoxColumn();
            Evento_Name = new DataGridViewTextBoxColumn();
            Evento_Fecha = new DataGridViewTextBoxColumn();


            ((System.ComponentModel.ISupportInitialize)eventosInMemoryBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GrdVw_Evento).BeginInit();
            SuspendLayout();
            // 
            // eventosInMemoryBindingSource
            // 
            eventosInMemoryBindingSource.CurrentChanged += eventosInMemoryBindingSource_CurrentChanged;
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
            // GrdVw_Evento
            // 
            GrdVw_Evento.AllowUserToOrderColumns = true;
            GrdVw_Evento.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GrdVw_Evento.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GrdVw_Evento.Columns.AddRange(new DataGridViewColumn[] { Evento_ID, Evento_Name, Evento_Fecha });
            GrdVw_Evento.Location = new Point(0, 0);
            GrdVw_Evento.Name = "GrdVw_Evento";
            GrdVw_Evento.Size = new Size(922, 241);
            GrdVw_Evento.TabIndex = 0;
            GrdVw_Evento.CellContentClick += dataGridView1_CellContentClick;
            GrdVw_Evento.CellDoubleClick += GrdVw_Evento_CellDoubleClick;
            // 
            // Evento_ID
            // 
            Evento_ID.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = "0";
            Evento_ID.DefaultCellStyle = dataGridViewCellStyle1;
            Evento_ID.Frozen = true;
            Evento_ID.HeaderText = "ID";
            Evento_ID.Name = "Evento_ID";
            Evento_ID.Resizable = DataGridViewTriState.True;
            // 
            // Evento_Name
            // 
            Evento_Name.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Evento_Name.FillWeight = 61.4562263F;
            Evento_Name.Frozen = true;
            Evento_Name.HeaderText = "Nombre";
            Evento_Name.Name = "Evento_Name";
            Evento_Name.Width = 779;

            // 
            // Evento_Fecha
            // 
            Evento_Fecha.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Evento_Fecha.FillWeight = 61.4562263F;
            Evento_Fecha.Frozen = true;
            Evento_Fecha.HeaderText = "Fecha";
            Evento_Fecha.Name = "Evento_Fecha";
            Evento_Fecha.Width = 779;


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
            // Form_Eventos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(922, 486);
            Controls.Add(btn_Editar);
            Controls.Add(btn_agregar);
            Controls.Add(btn_Borrar);
            Controls.Add(txt_ID);
            Controls.Add(txt_Name);
           
            Controls.Add(GrdVw_Evento);
            Name = "Form_Eventos";
            Text = "Eventos";
            Load += FormEventos_load;
            ((System.ComponentModel.ISupportInitialize)eventosInMemoryBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)GrdVw_Evento).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private BindingSource eventosInMemoryBindingSource;
        private Button btn_Borrar;
        private TextBox txt_Name;

        private DataGridView GrdVw_Evento;
     
        private Button btn_agregar;
        private Button btn_Editar;
        private TextBox txt_ID;
      

        private DataGridViewTextBoxColumn Evento_ID;
        private DataGridViewTextBoxColumn Evento_Name;
        private DataGridViewTextBoxColumn Evento_Fecha;
    }
}