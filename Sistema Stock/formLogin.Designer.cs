namespace Sistema_Stock
{
    partial class formLogin
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            txt_username = new System.Windows.Forms.TextBox();
            txt_password = new System.Windows.Forms.TextBox();
            btnIngresar = new System.Windows.Forms.Button();
            lnkOlvidaPass = new System.Windows.Forms.LinkLabel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(118, 93);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(229, 30);
            label1.TabIndex = 0;
            label1.Text = "¡Bienvenido al Sistema!\r\nPor favor digite su información de Ingreso\r\n";
            label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(112, 152);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(113, 15);
            label2.TabIndex = 1;
            label2.Text = "Nombre de Usuario:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(155, 197);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(70, 15);
            label3.TabIndex = 2;
            label3.Text = "Contraseña:";
            label3.Click += label3_Click;
            // 
            // txt_username
            // 
            txt_username.Location = new System.Drawing.Point(237, 149);
            txt_username.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txt_username.Name = "txt_username";
            txt_username.Size = new System.Drawing.Size(116, 23);
            txt_username.TabIndex = 3;
            // 
            // txt_password
            // 
            txt_password.Location = new System.Drawing.Point(237, 194);
            txt_password.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txt_password.Name = "txt_password";
            txt_password.Size = new System.Drawing.Size(116, 23);
            txt_password.TabIndex = 4;
            txt_password.UseSystemPasswordChar = true;
            // 
            // btnIngresar
            // 
            btnIngresar.Location = new System.Drawing.Point(266, 240);
            btnIngresar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new System.Drawing.Size(88, 27);
            btnIngresar.TabIndex = 5;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // lnkOlvidaPass
            // 
            lnkOlvidaPass.AutoSize = true;
            lnkOlvidaPass.Location = new System.Drawing.Point(135, 246);
            lnkOlvidaPass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lnkOlvidaPass.Name = "lnkOlvidaPass";
            lnkOlvidaPass.Size = new System.Drawing.Size(119, 15);
            lnkOlvidaPass.TabIndex = 6;
            lnkOlvidaPass.TabStop = true;
            lnkOlvidaPass.Text = "Olvidé mi contraseña";
            lnkOlvidaPass.LinkClicked += lnkOlvidaPass_LinkClicked;
            // 
            // formLogin
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackgroundImage = Sistema_Stock.Properties.Resources.background;
            ClientSize = new System.Drawing.Size(812, 490);
            Controls.Add(lnkOlvidaPass);
            Controls.Add(btnIngresar);
            Controls.Add(txt_password);
            Controls.Add(txt_username);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "formLogin";
            Text = "Ìngreso";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.LinkLabel lnkOlvidaPass;
    }
}

