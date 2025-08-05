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
            SuspendLayout();
            // 
            // btn_login
            // 
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
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Tahoma", 24F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.ImageAlign = ContentAlignment.TopLeft;
            label1.Location = new Point(12, 19);
            label1.Name = "label1";
            label1.Size = new Size(605, 39);
            label1.TabIndex = 1;
            label1.Text = "Sistema Integral de Control de Stock";
            // 
            // Form_Acceso
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.background;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(721, 454);
            Controls.Add(label1);
            Controls.Add(btn_register);
            Controls.Add(btn_login);
            MaximizeBox = false;
            Name = "Form_Acceso";
            Text = "Acceso";
            Load += Form_Acceso_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_login;
        private Button btn_register;
        private Label label1;
    }
}