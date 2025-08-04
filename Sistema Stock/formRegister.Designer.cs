namespace Sistema_Stock
{
    partial class formRegister
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
            label1 = new System.Windows.Forms.Label();
            txt_username = new System.Windows.Forms.TextBox();
            txt_password = new System.Windows.Forms.TextBox();
            txt_name = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            txt_surname = new System.Windows.Forms.TextBox();
            label6 = new System.Windows.Forms.Label();
            txt_email = new System.Windows.Forms.TextBox();
            btn_registerSend = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(14, 10);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(49, 15);
            label1.TabIndex = 0;
            label1.Text = "Register";
            label1.Click += label1_Click;
            // 
            // txt_username
            // 
            txt_username.Location = new System.Drawing.Point(103, 141);
            txt_username.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txt_username.Name = "txt_username";
            txt_username.Size = new System.Drawing.Size(116, 23);
            txt_username.TabIndex = 1;
            // 
            // txt_password
            // 
            txt_password.Location = new System.Drawing.Point(103, 171);
            txt_password.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txt_password.Name = "txt_password";
            txt_password.Size = new System.Drawing.Size(116, 23);
            txt_password.TabIndex = 1;
            txt_password.UseSystemPasswordChar = true;
            // 
            // txt_name
            // 
            txt_name.Location = new System.Drawing.Point(103, 50);
            txt_name.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txt_name.Name = "txt_name";
            txt_name.Size = new System.Drawing.Size(116, 23);
            txt_name.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(31, 141);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(60, 15);
            label2.TabIndex = 0;
            label2.Text = "Username";
            label2.Click += label1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(35, 174);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(57, 15);
            label3.TabIndex = 0;
            label3.Text = "Password";
            label3.Click += label1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(55, 53);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(39, 15);
            label4.TabIndex = 0;
            label4.Text = "Name";
            label4.Click += label1_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(30, 83);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(54, 15);
            label5.TabIndex = 0;
            label5.Text = "Surname";
            label5.Click += label1_Click;
            // 
            // txt_surname
            // 
            txt_surname.Location = new System.Drawing.Point(103, 80);
            txt_surname.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txt_surname.Name = "txt_surname";
            txt_surname.Size = new System.Drawing.Size(116, 23);
            txt_surname.TabIndex = 1;
            txt_surname.TextChanged += txt_surname_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(58, 113);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(36, 15);
            label6.TabIndex = 0;
            label6.Text = "Email";
            label6.Click += label1_Click;
            // 
            // txt_email
            // 
            txt_email.Location = new System.Drawing.Point(103, 110);
            txt_email.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txt_email.Name = "txt_email";
            txt_email.Size = new System.Drawing.Size(116, 23);
            txt_email.TabIndex = 1;
            // 
            // btn_registerSend
            // 
            btn_registerSend.Location = new System.Drawing.Point(624, 407);
            btn_registerSend.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btn_registerSend.Name = "btn_registerSend";
            btn_registerSend.Size = new System.Drawing.Size(88, 27);
            btn_registerSend.TabIndex = 2;
            btn_registerSend.Text = "Register";
            btn_registerSend.UseVisualStyleBackColor = true;
            btn_registerSend.Click += btn_registersend_click;
            // 
            // formRegister
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackgroundImage = Sistema_Stock.Properties.Resources.background;
            ClientSize = new System.Drawing.Size(726, 448);
            Controls.Add(btn_registerSend);
            Controls.Add(txt_email);
            Controls.Add(txt_surname);
            Controls.Add(txt_name);
            Controls.Add(label6);
            Controls.Add(txt_password);
            Controls.Add(label5);
            Controls.Add(txt_username);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "formRegister";
            Text = "Register";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_surname;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.Button btn_registerSend;
    }
}