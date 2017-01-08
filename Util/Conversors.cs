using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace EM3.Util
{
    public class Tipo_rebebimento_operadora_c : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return value;

            switch(int.Parse(value.ToString()))
            {
                case 0: return "Dias";
                case 1: return "Horas";
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Tipo_operadora_cartao : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return value;

            switch(int.Parse(value.ToString()))
            {
                case 0: return "Crédito";
                case 1: return "Débito";
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Tipo_intervaloConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return value;
            switch(int.Parse(value.ToString()))
            {
                case (int)Formas_pagamento.TIPO_INTERVALO.DIA: return "Dia";
                case (int)Formas_pagamento.TIPO_INTERVALO.INTERVALO: return "Intervalo";
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Tipo_pagamentoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return value;
            switch (int.Parse(value.ToString()))
            {
                case (int)Formas_pagamento.TIPO_PAGAMENTO.DINHEIRO: return "Dinheiro";
                case (int)Formas_pagamento.TIPO_PAGAMENTO.BOLETO: return "Boleto";
                case (int)Formas_pagamento.TIPO_PAGAMENTO.CARTAO: return "Cartão";
                case (int)Formas_pagamento.TIPO_PAGAMENTO.CHEQUE: return "Cheque";
                case (int)Formas_pagamento.TIPO_PAGAMENTO.CREDITO_CLIENTE: return "Crédito cliente";
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Conversors : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return value;
            if (value.ToString().Equals("True"))
                return "SIM";
            else
                return "NÃO";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
