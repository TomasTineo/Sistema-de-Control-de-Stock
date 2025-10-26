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
            Productos_btn = new Button();
            Eventos_btn = new Button();
            Reservas_btn = new Button();
            Categorias_btn = new Button();
            SuspendLayout();
            // 
            // Productos_btn
            // 
            Productos_btn.Location = new Point(295, 61);
            Productos_btn.Name = "Productos_btn";
            Productos_btn.Size = new Size(225, 54);
            Productos_btn.TabIndex = 0;
            Productos_btn.Text = "Productos";
            Productos_btn.UseVisualStyleBackColor = true;
            Productos_btn.Click += button1_Click;
            // 
            // Eventos_btn
            // 
            Eventos_btn.Location = new Point(295, 241);
            Eventos_btn.Name = "Eventos_btn";
            Eventos_btn.Size = new Size(225, 54);
            Eventos_btn.TabIndex = 0;
            Eventos_btn.Text = "Eventos";
            Eventos_btn.UseVisualStyleBackColor = true;
            Eventos_btn.Click += button1_Click;
            // 
            // Reservas_btn
            // 
            Reservas_btn.Location = new Point(295, 181);
            Reservas_btn.Name = "Reservas_btn";
            Reservas_btn.Size = new Size(225, 54);
            Reservas_btn.TabIndex = 0;
            Reservas_btn.Text = "Reservas";
            Reservas_btn.UseVisualStyleBackColor = true;
            Reservas_btn.Click += button1_Click;
            // 
            // Categorias_btn
            // 
            Categorias_btn.Location = new Point(295, 121);
            Categorias_btn.Name = "Categorias_btn";
            Categorias_btn.Size = new Size(225, 54);
            Categorias_btn.TabIndex = 0;
            Categorias_btn.Text = "Categorias";
            Categorias_btn.UseVisualStyleBackColor = true;
            Categorias_btn.Click += button1_Click;
            // 
            // Form_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(800, 450);
            Controls.Add(Categorias_btn);
            Controls.Add(Reservas_btn);
            Controls.Add(Eventos_btn);
            Controls.Add(Productos_btn);
            Name = "Form_Main";
            Text = "Inicio";
            Load += FormMain_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button Productos_btn;
        private Button Eventos_btn;
        private Button Reservas_btn;
        private Button Categorias_btn;
    }
}