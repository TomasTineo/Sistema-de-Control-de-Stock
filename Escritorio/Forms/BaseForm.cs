using System;
using System.Drawing;
using System.Windows.Forms;

namespace Escritorio.Forms
{
    /// <summary>
    /// Clase base para todos los formularios de la aplicación
    /// Proporciona funcionalidad común y estilos consistentes
    /// </summary>
    public abstract class BaseForm : Form
    {
        #region Colores del Sistema
        
        protected static readonly Color PrimaryColor = Color.FromArgb(41, 128, 185);
        protected static readonly Color SecondaryColor = Color.FromArgb(52, 152, 219);
        protected static readonly Color SuccessColor = Color.FromArgb(39, 174, 96);
        protected static readonly Color WarningColor = Color.FromArgb(241, 196, 15);
        protected static readonly Color DangerColor = Color.FromArgb(231, 76, 60);
        protected static readonly Color InfoColor = Color.FromArgb(52, 152, 219);
        protected static readonly Color LightGray = Color.FromArgb(236, 240, 241);
        protected static readonly Color DarkGray = Color.FromArgb(149, 165, 166);
        
        #endregion

        #region Fuentes del Sistema
        
        protected static readonly Font HeaderFont = new Font("Segoe UI", 16F, FontStyle.Bold);
        protected static readonly Font SubHeaderFont = new Font("Segoe UI", 12F, FontStyle.Bold);
        protected static readonly Font BodyFont = new Font("Segoe UI", 10F);
        protected static readonly Font SmallFont = new Font("Segoe UI", 9F);
        
        #endregion

        #region Constructor
        
        protected BaseForm()
        {
            InitializeBaseForm();
        }

        private void InitializeBaseForm()
        {
            this.BackColor = Color.White;
            this.Font = BodyFont;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.AutoScaleMode = AutoScaleMode.Font;
        }
        
        #endregion

        #region Métodos de Mensajes

        /// <summary>
        /// Muestra un mensaje de éxito al usuario
        /// </summary>
        protected DialogResult MostrarExito(string mensaje, string titulo = "Éxito")
        {
            return MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// Muestra un mensaje de advertencia al usuario
        /// </summary>
        protected DialogResult MostrarAdvertencia(string mensaje, string titulo = "Advertencia")
        {
            return MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Muestra un mensaje de error al usuario
        /// </summary>
        protected DialogResult MostrarError(string mensaje, string titulo = "Error")
        {
            return MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        /// <summary>
        /// Muestra un mensaje informativo al usuario
        /// </summary>
        protected DialogResult MostrarInfo(string mensaje, string titulo = "Información")
        {
            return MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// Muestra un diálogo de confirmación Sí/No
        /// </summary>
        protected bool Confirmar(string mensaje, string titulo = "Confirmar")
        {
            var resultado = MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            return resultado == DialogResult.Yes;
        }

        /// <summary>
        /// Muestra un diálogo de confirmación con opciones Sí/No/Cancelar
        /// </summary>
        protected DialogResult ConfirmarConCancelar(string mensaje, string titulo = "Confirmar")
        {
            return MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);
        }

        #endregion

        #region Métodos de Estilo

        /// <summary>
        /// Aplica estilo de botón primario (azul)
        /// </summary>
        protected void EstilizarBotonPrimario(Button boton)
        {
            boton.BackColor = PrimaryColor;
            boton.ForeColor = Color.White;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            boton.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Aplica estilo de botón de éxito (verde)
        /// </summary>
        protected void EstilizarBotonExito(Button boton)
        {
            boton.BackColor = SuccessColor;
            boton.ForeColor = Color.White;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            boton.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Aplica estilo de botón de advertencia (amarillo)
        /// </summary>
        protected void EstilizarBotonAdvertencia(Button boton)
        {
            boton.BackColor = WarningColor;
            boton.ForeColor = Color.White;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            boton.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Aplica estilo de botón de peligro (rojo)
        /// </summary>
        protected void EstilizarBotonPeligro(Button boton)
        {
            boton.BackColor = DangerColor;
            boton.ForeColor = Color.White;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            boton.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Aplica estilo de botón secundario (gris)
        /// </summary>
        protected void EstilizarBotonSecundario(Button boton)
        {
            boton.BackColor = DarkGray;
            boton.ForeColor = Color.White;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            boton.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Crea un panel de encabezado estilizado
        /// </summary>
        protected Panel CrearPanelEncabezado(string titulo, Color? colorFondo = null)
        {
            var panel = new Panel
            {
                BackColor = colorFondo ?? PrimaryColor,
                Dock = DockStyle.Top,
                Height = 70,
                Padding = new Padding(15, 10, 15, 10)
            };

            var label = new Label
            {
                Text = titulo,
                Font = HeaderFont,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(15, 20)
            };

            panel.Controls.Add(label);
            return panel;
        }

        #endregion

        #region Métodos de Control de Estado

        /// <summary>
        /// Deshabilita todos los botones del formulario
        /// </summary>
        protected void DeshabilitarBotones()
        {
            foreach (Control control in this.Controls)
            {
                DeshabilitarBotonesRecursivo(control);
            }
        }

        /// <summary>
        /// Habilita todos los botones del formulario
        /// </summary>
        protected void HabilitarBotones()
        {
            foreach (Control control in this.Controls)
            {
                HabilitarBotonesRecursivo(control);
            }
        }

        private void DeshabilitarBotonesRecursivo(Control control)
        {
            if (control is Button button)
            {
                button.Enabled = false;
            }

            foreach (Control child in control.Controls)
            {
                DeshabilitarBotonesRecursivo(child);
            }
        }

        private void HabilitarBotonesRecursivo(Control control)
        {
            if (control is Button button)
            {
                button.Enabled = true;
            }

            foreach (Control child in control.Controls)
            {
                HabilitarBotonesRecursivo(child);
            }
        }

        /// <summary>
        /// Muestra un indicador de carga (cursor de espera)
        /// </summary>
        protected void MostrarCargando()
        {
            this.Cursor = Cursors.WaitCursor;
            this.Enabled = false;
        }

        /// <summary>
        /// Oculta el indicador de carga
        /// </summary>
        protected void OcultarCargando()
        {
            this.Cursor = Cursors.Default;
            this.Enabled = true;
        }

        #endregion

        #region Métodos de Navegación

        /// <summary>
        /// Cierra el formulario actual y abre uno nuevo
        /// </summary>
        protected void NavegarA<T>() where T : Form, new()
        {
            var nuevoForm = new T();
            nuevoForm.Show();
            this.Close();
        }

        /// <summary>
        /// Abre un formulario como diálogo modal
        /// </summary>
        protected DialogResult AbrirDialogo<T>() where T : Form, new()
        {
            using var dialogo = new T();
            return dialogo.ShowDialog(this);
        }

        #endregion

        #region Manejo de Excepciones

        /// <summary>
        /// Maneja excepciones comunes de API
        /// </summary>
        protected void ManejarExcepcionApi(Exception ex)
        {
            if (ex is UnauthorizedAccessException)
            {
                MostrarAdvertencia("Su sesión ha expirado. Por favor, inicie sesión nuevamente.", "Sesión Expirada");
                // Aquí podrías redirigir al login
                this.Close();
            }
            else if (ex is HttpRequestException)
            {
                MostrarError($"Error de conexión con el servidor: {ex.Message}", "Error de Conexión");
            }
            else
            {
                MostrarError($"Ha ocurrido un error inesperado: {ex.Message}", "Error");
            }
        }

        #endregion
    }
}
