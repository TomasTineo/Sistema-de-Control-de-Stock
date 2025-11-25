namespace Escritorio
{
    partial class Form_Login
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
            btn_enviarLogin = new Button();
            txt_username = new TextBox();
            txt_password = new TextBox();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // btn_enviarLogin
            // 
            btn_enviarLogin.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_enviarLogin.Location = new Point(320, 187);
            btn_enviarLogin.Name = "btn_enviarLogin";
            btn_enviarLogin.Size = new Size(102, 47);
            btn_enviarLogin.TabIndex = 2;
            btn_enviarLogin.Text = "Enviar";
            btn_enviarLogin.UseVisualStyleBackColor = true;
            btn_enviarLogin.Click += btn_enviarLogin_Click;
            // 
            // txt_username
            // 
            txt_username.Anchor = AnchorStyles.None;
            txt_username.Location = new Point(213, 83);
            txt_username.Name = "txt_username";
            txt_username.Size = new Size(100, 23);
            txt_username.TabIndex = 0;
            // 
            // txt_password
            // 
            txt_password.Anchor = AnchorStyles.None;
            txt_password.Location = new Point(213, 112);
            txt_password.Name = "txt_password";
            txt_password.Size = new Size(100, 23);
            txt_password.TabIndex = 1;
            txt_password.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(98, 86);
            label1.Name = "label1";
            label1.Size = new Size(109, 15);
            label1.TabIndex = 2;
            label1.Text = "Nombre de usuario";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Location = new Point(140, 115);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 2;
            label2.Text = "Contraseña";
            // 
            // Form_Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 246);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txt_password);
            Controls.Add(txt_username);
            Controls.Add(btn_enviarLogin);
            MaximizeBox = false;
            MinimumSize = new Size(450, 285);
            Name = "Form_Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_enviarLogin;
        private TextBox txt_username;
        private TextBox txt_password;
        private Label label1;
        private Label label2;
    }
}