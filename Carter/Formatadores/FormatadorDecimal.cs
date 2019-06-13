using System;
using System.Windows.Data;

namespace Carter.Formatadores
{
    /// <summary>
    /// Classe criada para permitir o uso de objetos decimal diretamente nos Bindings do WPF.
    /// </summary>
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class FormatadorDecimal : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return ((decimal)value).ToString("N2");
            }
            else
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal valor;
            if (decimal.TryParse(value.ToString(), out valor))
            {
                return valor;
            }
            else
            {
                return null;
            }
        }

    }

}
