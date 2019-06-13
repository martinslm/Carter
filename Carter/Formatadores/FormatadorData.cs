using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Carter.Formatadores
{
    /// <summary>
    /// Classe criada para permitir o uso de objetos DateTime diretamente nos Bindings do WPF.
    /// </summary
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class FormatadorData : IValueConverter
    {

            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                string formatoData = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
                if (value is DateTime)
                {
                    return ((DateTime)value).ToString(formatoData);
                }
                else
                {
                    return null;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                DateTime data;

                if (value != null && DateTime.TryParse(value.ToString(), out data))
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }

    }
}
