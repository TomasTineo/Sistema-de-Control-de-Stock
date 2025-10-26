using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Escritorio.Helpers
{
    /// <summary>
    /// Clase auxiliar para validación de formularios Windows Forms
    /// </summary>
    public static class FormValidator
    {
        /// <summary>
        /// Valida que un campo de texto no esté vacío
        /// </summary>
        public static bool ValidarCampoRequerido(TextBox textBox, ErrorProvider errorProvider, string mensaje = "Este campo es requerido")
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                errorProvider.SetError(textBox, mensaje);
                return false;
            }
            errorProvider.SetError(textBox, string.Empty);
            return true;
        }

        /// <summary>
        /// Valida que un campo de texto tenga una longitud mínima
        /// </summary>
        public static bool ValidarLongitudMinima(TextBox textBox, ErrorProvider errorProvider, int longitudMinima, string? mensaje = null)
        {
            mensaje ??= $"Debe tener al menos {longitudMinima} caracteres";
            
            if (textBox.Text.Length < longitudMinima)
            {
                errorProvider.SetError(textBox, mensaje);
                return false;
            }
            errorProvider.SetError(textBox, string.Empty);
            return true;
        }

        /// <summary>
        /// Valida que un campo de texto tenga una longitud máxima
        /// </summary>
        public static bool ValidarLongitudMaxima(TextBox textBox, ErrorProvider errorProvider, int longitudMaxima, string? mensaje = null)
        {
            mensaje ??= $"No debe exceder {longitudMaxima} caracteres";
            
            if (textBox.Text.Length > longitudMaxima)
            {
                errorProvider.SetError(textBox, mensaje);
                return false;
            }
            errorProvider.SetError(textBox, string.Empty);
            return true;
        }

        /// <summary>
        /// Valida que un campo de texto contenga un email válido
        /// </summary>
        public static bool ValidarEmail(TextBox textBox, ErrorProvider errorProvider, string mensaje = "Email inválido")
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            
            if (!regex.IsMatch(textBox.Text))
            {
                errorProvider.SetError(textBox, mensaje);
                return false;
            }
            errorProvider.SetError(textBox, string.Empty);
            return true;
        }

        /// <summary>
        /// Valida que un campo de texto contenga un número decimal válido
        /// </summary>
        public static bool ValidarNumericoDecimal(TextBox textBox, ErrorProvider errorProvider, string mensaje = "Debe ser un número válido")
        {
            if (!decimal.TryParse(textBox.Text, out _))
            {
                errorProvider.SetError(textBox, mensaje);
                return false;
            }
            errorProvider.SetError(textBox, string.Empty);
            return true;
        }

        /// <summary>
        /// Valida que un campo de texto contenga un número decimal válido y positivo
        /// </summary>
        public static bool ValidarNumericoPositivo(TextBox textBox, ErrorProvider errorProvider, string mensaje = "Debe ser un número positivo")
        {
            if (!decimal.TryParse(textBox.Text, out decimal valor) || valor <= 0)
            {
                errorProvider.SetError(textBox, mensaje);
                return false;
            }
            errorProvider.SetError(textBox, string.Empty);
            return true;
        }

        /// <summary>
        /// Valida que un campo de texto contenga un número entero válido
        /// </summary>
        public static bool ValidarNumericoEntero(TextBox textBox, ErrorProvider errorProvider, string mensaje = "Debe ser un número entero válido")
        {
            if (!int.TryParse(textBox.Text, out _))
            {
                errorProvider.SetError(textBox, mensaje);
                return false;
            }
            errorProvider.SetError(textBox, string.Empty);
            return true;
        }

        /// <summary>
        /// Valida que un campo de texto contenga un número entero positivo o cero
        /// </summary>
        public static bool ValidarNumericoEnteroPositivoOCero(TextBox textBox, ErrorProvider errorProvider, string mensaje = "Debe ser un número entero positivo o cero")
        {
            if (!int.TryParse(textBox.Text, out int valor) || valor < 0)
            {
                errorProvider.SetError(textBox, mensaje);
                return false;
            }
            errorProvider.SetError(textBox, string.Empty);
            return true;
        }

        /// <summary>
        /// Valida que un campo de texto contenga un teléfono válido
        /// </summary>
        public static bool ValidarTelefono(TextBox textBox, ErrorProvider errorProvider, string mensaje = "Teléfono inválido")
        {
            // Permite formatos como: 1234567890, (123) 456-7890, 123-456-7890, etc.
            var regex = new Regex(@"^[\d\s\-\(\)]+$");
            
            if (textBox.Text.Length < 7 || !regex.IsMatch(textBox.Text))
            {
                errorProvider.SetError(textBox, mensaje);
                return false;
            }
            errorProvider.SetError(textBox, string.Empty);
            return true;
        }

        /// <summary>
        /// Valida que un ComboBox tenga un valor seleccionado
        /// </summary>
        public static bool ValidarComboBoxSeleccionado(ComboBox comboBox, ErrorProvider errorProvider, string mensaje = "Debe seleccionar una opción")
        {
            if (comboBox.SelectedValue == null || comboBox.SelectedIndex < 0)
            {
                errorProvider.SetError(comboBox, mensaje);
                return false;
            }
            errorProvider.SetError(comboBox, string.Empty);
            return true;
        }

        /// <summary>
        /// Valida que un DateTimePicker tenga una fecha válida dentro de un rango
        /// </summary>
        public static bool ValidarFechaRango(DateTimePicker dateTimePicker, ErrorProvider errorProvider, DateTime? fechaMinima = null, DateTime? fechaMaxima = null, string? mensaje = null)
        {
            fechaMinima ??= DateTime.MinValue;
            fechaMaxima ??= DateTime.MaxValue;

            if (dateTimePicker.Value < fechaMinima || dateTimePicker.Value > fechaMaxima)
            {
                mensaje ??= $"La fecha debe estar entre {fechaMinima:dd/MM/yyyy} y {fechaMaxima:dd/MM/yyyy}";
                errorProvider.SetError(dateTimePicker, mensaje);
                return false;
            }
            errorProvider.SetError(dateTimePicker, string.Empty);
            return true;
        }

        /// <summary>
        /// Valida que una fecha sea mayor a la fecha actual
        /// </summary>
        public static bool ValidarFechaFutura(DateTimePicker dateTimePicker, ErrorProvider errorProvider, string mensaje = "La fecha debe ser posterior a hoy")
        {
            if (dateTimePicker.Value.Date <= DateTime.Now.Date)
            {
                errorProvider.SetError(dateTimePicker, mensaje);
                return false;
            }
            errorProvider.SetError(dateTimePicker, string.Empty);
            return true;
        }

        /// <summary>
        /// Limpia todos los errores de un ErrorProvider
        /// </summary>
        public static void LimpiarErrores(ErrorProvider errorProvider)
        {
            errorProvider.Clear();
        }

        /// <summary>
        /// Valida múltiples campos y retorna true solo si todos son válidos
        /// </summary>
        public static bool ValidarMultiples(params Func<bool>[] validaciones)
        {
            bool resultado = true;
            foreach (var validacion in validaciones)
            {
                resultado &= validacion();
            }
            return resultado;
        }
    }
}
