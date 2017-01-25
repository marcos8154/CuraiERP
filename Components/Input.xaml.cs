using EM3.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace EM3.Components
{
    /// <summary>
    /// Interação lógica para Input.xam
    /// </summary>
    public partial class Input : UserControl
    {
        public int Index
        {
            get
            {
                return txInput.TabIndex;
            }
            set
            {
                this.TabIndex = 9999;
                txInput.TabIndex = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return this.IsEnabled;
            }
            set
            {
                if (value)
                    txInput.Background = Brushes.White;
                else
                    txInput.Background = Brushes.WhiteSmoke;

                this.IsEnabled = value;
            }
        }

        public CharacterCasing CharacterCasing
        {
            get
            {
                return txInput.CharacterCasing;
            }
            set
            {
                txInput.CharacterCasing = value;
            }
        }

        public int MaxLength
        {
            get
            {
                return txInput.MaxLength;
            }
            set
            {
                txInput.MaxLength = value;
            }
        }

        public Visibility Visible
        {
            get
            {
                return txInput.Visibility;
            }
            set
            {
                txInput.Visibility = value;
            }
        }

        public string Text
        {
            get
            {
                if (IsNumeric || isMoney)
                    if (string.IsNullOrEmpty(txInput.Text)) return "0";

                return txInput.Text;
            }
            set
            {
                txInput.Text = value;
                if (isMoney)
                    txInput.Text = decimal.Parse(txInput.Text).ToString("N2");
            }
        }

        public string Title
        {
            get
            {
                return lbTitle.Content.ToString();
            }
            set
            {
                lbTitle.Content = value;

                if (Required)
                    lbTitle.Content += " * ";
            }
        }

        private bool required = false;

        public bool Required
        {
            set
            {
                required = value;
                if (value)
                    lbTitle.Content += " * ";
            }
            get
            {
                return this.required;
            }
        }

        private bool isNumeric = false;
        private bool isMoney = false;

        public bool IsNumeric
        {
            set
            {
                isNumeric = value;
                txInput.Text = "0";
            }
            get
            {
                return isNumeric;
            }
        }

        public bool IsMoney
        {
            set
            {
                if (value)
                    txInput.HorizontalContentAlignment = HorizontalAlignment.Right;
                isMoney = value;
                txInput.Text = "0,00";
            }
            get
            {
                return isMoney;
            }
        }

        public string HelpName { get; set; }

        private decimal value;

        public decimal GetDecimal
        {
            get
            {
                if (string.IsNullOrEmpty(txInput.Text))
                    return 0;
                try
                {
                    string value = string.Format("{0:0,0.00}", txInput.Text);

                    string[] t = value.Split('.');
                    if (t.Length <= 2 && !txInput.Text.Contains(","))
                        value = value.Replace(".", ",");

                    decimal d = decimal.Parse(value);
                    return d;
                }
                catch (Exception ex)
                {
                    new MsgAlerta("Ocorreu um problema durante a conversão numérica em um dos campos. Verifique os valores numéricos e tente novamente.");
                }
                return 0;
            }
        }

        public int GetInt
        {
            get
            {
                if (string.IsNullOrEmpty(txInput.Text))
                    return 0;

                if (!IsNumeric)
                    return 0;

                return int.Parse(txInput.Text);
            }
        }

        public double GetDouble
        {
            get
            {
                if (string.IsNullOrEmpty(txInput.Text))
                    return 0;

                try
                {
                    string value = string.Format("{0:0,0.00}", txInput.Text);

                    string[] t = value.Split('.');
                    if (t.Length <= 2 && !txInput.Text.Contains(","))
                        value = value.Replace(".", ",");

                    double d = double.Parse(value);
                    return d;
                }
                catch (Exception ex)
                {
                    new MsgAlerta("Ocorreu um problema durante a conversão numérica em um dos campos. Verifique os valores numéricos e tente novamente.");
                }
                return 0;
            }
        }

        public delegate void KeyEvent(object sender, KeyEventArgs e);
        public delegate void OnGainFocusEvent(object sender, RoutedEventArgs e);
        public delegate void OnLostFocusEvent(object sender, RoutedEventArgs e);

        public event KeyEvent InputKeyDown;
        public event KeyEvent InputKeyUp;
        public event OnLostFocusEvent InputLostFocus;
        public event OnGainFocusEvent InputGainFocus;

        public decimal MaxValue { get; set; }

        /// <summary>
        /// *** Constructor ***
        /// </summary>
        public Input()
        {
            InitializeComponent();

            this.MinHeight = 56;
            txInput.KeyDown += TxInput_KeyDown;
            txInput.KeyUp += TxInput_KeyUp;
            txInput.LostFocus += TxInput_LostFocus;
            txInput.GotFocus += TxInput_GotFocus;
            Enabled = true;
            CharacterCasing = CharacterCasing.Upper;
        }

        private void TxInput_GotFocus(object sender, RoutedEventArgs e)
        {
            border.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FF4DA1F5");
            if (InputGainFocus != null) InputGainFocus(sender, e);
            txInput.SelectAll();
        }

        private void TxInput_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                border.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FFACA6A6");
                if (InputLostFocus != null) InputLostFocus(sender, e);

                if (isMoney)
                {
                    string text = txInput.Text;
                    if (!string.IsNullOrEmpty(text))
                        value = decimal.Parse(text);
                    else
                    {
                        value = 0;
                        txInput.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                new MsgAlerta("Ocorreu um problema durante a conversão numérica em um dos campos. Verifique os valores numéricos e tente novamente.");
            }
        }

        public void SetFocused()
        {
            this.Focus();
            txInput.Focus();
        }

        private void TxInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (InputKeyUp != null) InputKeyUp(sender, e);
        }

        private void TxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
                new Help(HelpName);

            if (InputKeyDown != null) InputKeyDown(sender, e);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (isNumeric)
                {
                    Regex rgxNumbers = new Regex("[^0-9]+");
                    e.Handled = (rgxNumbers.IsMatch(e.Text));
                }

                if (isMoney)
                {
                    Regex rgxNumbers = new Regex("[^0-9]+");
                    e.Handled = (rgxNumbers.IsMatch(e.Text) || (e.Text.Equals(",")));
                    if (e.Text.Equals(",") || e.Text.Equals("."))
                    {
                        if (e.Text.Equals(",") && txInput.Text.Contains(","))
                            return;

                        if (e.Text.Equals(".") && txInput.Text.Contains(","))
                            return;

                        /*   if (e.Text.Equals(".") && txInput.Text.Contains("."))
                               return; */

                        if (e.Text.Equals(",") && txInput.Text.Contains(","))
                            return;


                        if (e.Text.Equals(".") && (txInput.Text.Last().Equals('.') || txInput.Text.Last().Equals(',')))
                            return;

                        if (e.Text.Equals(",") && (txInput.Text.Last().Equals('.') || txInput.Text.Last().Equals(',')))
                            return;

                        txInput.Text += e.Text;
                        txInput.SelectionStart = txInput.Text.Length; // add some logic if length is 0
                        txInput.SelectionLength = 0;

                    }
                    if(e.Text.Equals("-"))
                    {
                        if (txInput.Text.Contains("-"))
                            return;

                        txInput.Text = "-";
                        txInput.SelectionStart = txInput.Text.Length; // add some logic if length is 0
                        txInput.SelectionLength = 0;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TrataMaxValue();
            }
            catch (Exception ex)
            {
                txInput.Text = "0";
            }
        }

        private void TrataMaxValue()
        {
            if (!isMoney && !isNumeric)
                return;

            if (MaxValue > 0)
            {
                decimal result = 0;
                if (decimal.TryParse(txInput.Text, out result))
                {
                    if (result > MaxValue)
                    {
                        while (result > MaxValue)
                            result -= MaxValue;
                        txInput.Text = (result).ToString();
                        txInput.SelectionLength = txInput.Text.Length;
                    }
                }
            }
        }
    }
}