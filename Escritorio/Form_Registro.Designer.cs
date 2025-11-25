

namespace Escritorio
{
    partial class Form_Registro
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
            btn_enviarRegistro = new Button();
            txt_username = new TextBox();
            txt_password = new TextBox();
            txt_name = new TextBox();
            txt_surname = new TextBox();
            txt_email = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            SuspendLayout();
            // 
            // btn_enviarRegistro
            // 
            btn_enviarRegistro.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_enviarRegistro.Location = new Point(318, 252);
            btn_enviarRegistro.Name = "btn_enviarRegistro";
            btn_enviarRegistro.Size = new Size(104, 47);
            btn_enviarRegistro.TabIndex = 5;
            btn_enviarRegistro.Text = "Enviar";
            btn_enviarRegistro.UseVisualStyleBackColor = true;
            btn_enviarRegistro.Click += btn_enviarRegistro_Click;
            // 
            // txt_username
            // 
            txt_username.Anchor = AnchorStyles.None;
            txt_username.Location = new Point(178, 65);
            txt_username.Name = "txt_username";
            txt_username.Size = new Size(119, 23);
            txt_username.TabIndex = 0;
            txt_username.TextChanged += txt_name_TextChanged;
            // 
            // txt_password
            // 
            txt_password.Anchor = AnchorStyles.None;
            txt_password.Location = new Point(178, 94);
            txt_password.Name = "txt_password";
            txt_password.Size = new Size(119, 23);
            txt_password.TabIndex = 1;
            txt_password.UseSystemPasswordChar = true;
            // 
            // txt_name
            // 
            txt_name.Anchor = AnchorStyles.None;
            txt_name.Location = new Point(178, 123);
            txt_name.Name = "txt_name";
            txt_name.Size = new Size(119, 23);
            txt_name.TabIndex = 2;
            // 
            // txt_surname
            // 
            txt_surname.Anchor = AnchorStyles.None;
            txt_surname.Location = new Point(178, 152);
            txt_surname.Name = "txt_surname";
            txt_surname.Size = new Size(119, 23);
            txt_surname.TabIndex = 3;
            // 
            // txt_email
            // 
            txt_email.Anchor = AnchorStyles.None;
            txt_email.Location = new Point(178, 181);
            txt_email.Name = "txt_email";
            txt_email.Size = new Size(119, 23);
            txt_email.TabIndex = 4;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(63, 68);
            label1.Name = "label1";
            label1.Size = new Size(109, 15);
            label1.TabIndex = 2;
            label1.Text = "Nombre de usuario";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Location = new Point(105, 97);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 2;
            label2.Text = "Contraseña";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Location = new Point(121, 126);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 2;
            label3.Text = "Nombre";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Location = new Point(121, 155);
            label4.Name = "label4";
            label4.Size = new Size(51, 15);
            label4.TabIndex = 2;
            label4.Text = "Apellido";
            label4.Click += label3_Click;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Location = new Point(136, 184);
            label5.Name = "label5";
            label5.Size = new Size(36, 15);
            label5.TabIndex = 2;
            label5.Text = "Email";
            label5.Click += label3_Click;
            // 
            // Form_Registro
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(434, 311);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txt_email);
            Controls.Add(txt_surname);
            Controls.Add(txt_name);
            Controls.Add(txt_password);
            Controls.Add(txt_username);
            Controls.Add(btn_enviarRegistro);
            MaximizeBox = false;
            MinimumSize = new Size(450, 350);
            Name = "Form_Registro";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Registro";
            Load += Form_Registro_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_enviarRegistro;
        private TextBox txt_username;
        private TextBox txt_password;
        private TextBox txt_name;
        private TextBox txt_surname;
        private TextBox txt_email;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}