namespace Escritorio
{
    partial class Form_Reportes
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
            tabControl = new TabControl();
            tabProductosStock = new TabPage();
            tabTopProductos = new TabPage();
            tabControl.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabProductosStock);
            tabControl.Controls.Add(tabTopProductos);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1199, 701);
            tabControl.TabIndex = 0;
            // 
            // tabProductosStock
            // 
            tabProductosStock.Location = new Point(4, 24);
            tabProductosStock.Name = "tabProductosStock";
            tabProductosStock.Size = new Size(1191, 673);
            tabProductosStock.TabIndex = 0;
            tabProductosStock.Text = "Productos con Bajo Stock";
            tabProductosStock.UseVisualStyleBackColor = true;
            // 
            // tabTopProductos
            // 
            tabTopProductos.Location = new Point(4, 24);
            tabTopProductos.Name = "tabTopProductos";
            tabTopProductos.Size = new Size(1191, 673);
            tabTopProductos.TabIndex = 1;
            tabTopProductos.Text = "Top Productos Más Reservados";
            tabTopProductos.UseVisualStyleBackColor = true;
            // 
            // Form_Reportes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new Size(1199, 701);
            Controls.Add(tabControl);
            MinimumSize = new Size(1215, 740);
            Name = "Form_Reportes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reportes y Estadísticas";
            tabControl.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}
