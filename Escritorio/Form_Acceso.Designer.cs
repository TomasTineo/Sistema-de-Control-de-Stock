namespace Escritorio
{
    partial class Form_Acceso
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
            btn_login = new Button();
            btn_register = new Button();
            label1 = new Label();
            lbl_Bienvenida = new Label();
            SuspendLayout();
            // 
            // btn_login
            // 
            btn_login.Anchor = AnchorStyles.Bottom;
            btn_login.Location = new Point(572, 402);
            btn_login.Name = "btn_login";
            btn_login.Size = new Size(137, 40);
            btn_login.TabIndex = 0;
            btn_login.Text = "Ingresar";
            btn_login.UseVisualStyleBackColor = true;
            btn_login.Click += btn_login_click;
            // 
            // btn_register
            // 
            btn_register.Anchor = AnchorStyles.Bottom;
            btn_register.Location = new Point(429, 402);
            btn_register.Name = "btn_register";
            btn_register.Size = new Size(137, 40);
            btn_register.TabIndex = 0;
            btn_register.Text = "Registrarse";
            btn_register.UseVisualStyleBackColor = true;
            btn_register.Click += btn_register_click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(140, 150);
            label1.MaximumSize = new Size(500, 0);
            label1.Name = "label1";
            label1.Size = new Size(442, 100);
            label1.TabIndex = 1;
            label1.Text = "¡Bienvenido al sistema! \r\n\r\nPara continuar por favor ingrese sus credenciales o registre unas nuevas como operador.\r\n";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click;
            // 
            // lbl_Bienvenida
            // 
            lbl_Bienvenida.Anchor = AnchorStyles.Top;
            lbl_Bienvenida.AutoSize = true;
            lbl_Bienvenida.BorderStyle = BorderStyle.Fixed3D;
            lbl_Bienvenida.Font = new Font("Times New Roman", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lbl_Bienvenida.Location = new Point(70, 30);
            lbl_Bienvenida.Name = "lbl_Bienvenida";
            lbl_Bienvenida.Size = new Size(580, 56);
            lbl_Bienvenida.TabIndex = 2;
            lbl_Bienvenida.Text = "Sistema de Control de Stock";
            lbl_Bienvenida.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form_Acceso
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(721, 454);
            Controls.Add(lbl_Bienvenida);
            Controls.Add(label1);
            Controls.Add(btn_register);
            Controls.Add(btn_login);
            MaximizeBox = false;
            MinimumSize = new Size(737, 493);
            Name = "Form_Acceso";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Acceso";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_login;
        private Button btn_register;
        private Label label1;
        private Label lbl_Bienvenida;
    }
}