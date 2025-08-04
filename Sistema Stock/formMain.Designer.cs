namespace Sistema_Stock
{
    partial class formMain
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
            btn_login = new System.Windows.Forms.Button();
            Titulo = new System.Windows.Forms.Label();
            btn_register = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // btn_login
            // 
            btn_login.BackColor = System.Drawing.Color.GhostWhite;
            btn_login.FlatAppearance.BorderSize = 0;
            btn_login.ForeColor = System.Drawing.SystemColors.ControlText;
            btn_login.Location = new System.Drawing.Point(678, 451);
            btn_login.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btn_login.Name = "btn_login";
            btn_login.Size = new System.Drawing.Size(141, 51);
            btn_login.TabIndex = 0;
            btn_login.Text = "Login";
            btn_login.UseVisualStyleBackColor = false;
            btn_login.Click += btn_login_Click;
            // 
            // Titulo
            // 
            Titulo.AutoSize = true;
            Titulo.BackColor = System.Drawing.Color.Transparent;
            Titulo.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Titulo.ForeColor = System.Drawing.SystemColors.HighlightText;
            Titulo.Location = new System.Drawing.Point(14, 10);
            Titulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Titulo.Name = "Titulo";
            Titulo.Size = new System.Drawing.Size(249, 37);
            Titulo.TabIndex = 2;
            Titulo.Text = "Stock Manager";
            Titulo.Click += label1_Click;
            // 
            // btn_register
            // 
            btn_register.BackColor = System.Drawing.Color.GhostWhite;
            btn_register.FlatAppearance.BorderSize = 0;
            btn_register.ForeColor = System.Drawing.SystemColors.ControlText;
            btn_register.Location = new System.Drawing.Point(519, 451);
            btn_register.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btn_register.Name = "btn_register";
            btn_register.Size = new System.Drawing.Size(141, 51);
            btn_register.TabIndex = 0;
            btn_register.Text = "Register";
            btn_register.UseVisualStyleBackColor = false;
            btn_register.Click += btn_register_Click;
            // 
            // formMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackgroundImage = SistemaStock.Properties.Resources.background;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            ClientSize = new System.Drawing.Size(833, 516);
            Controls.Add(Titulo);
            Controls.Add(btn_register);
            Controls.Add(btn_login);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "formMain";
            Text = "Form1";
            Load += formMain_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Label Titulo;
        private System.Windows.Forms.Button btn_register;
    }
}