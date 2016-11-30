using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace EM3.Util
{
    public class TipoArmazemConversor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return value;
            if (value.ToString().Equals("0"))
                return "Próprio";
            if(value.ToString().Equals("1"))
                return "Alugado";
            if (value.ToString().Equals("2"))
                return "De terceiros";

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
