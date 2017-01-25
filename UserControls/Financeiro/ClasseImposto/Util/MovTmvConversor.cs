using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace EM3.Util
{
    public class MovTmvConversor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value == null)
                return value;

            int tipo = int.Parse(value.ToString());
            
            switch(tipo)
            {
                case 0: return "Entrada";
                case 1: return "Saída";
                case 2: return "Nenhum";
            }

            return "INVALIDO";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
