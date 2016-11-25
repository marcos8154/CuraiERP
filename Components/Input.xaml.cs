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
                return txInput.Text;
            }
            set
            {
                txInput.Text = value;
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
            }
        }

        public bool Required
        {
            set
            {
                if (value)
                    lbTitle.Content += " * ";
            }
        }

        private bool isNumeric = false;
        private bool isMoney = false;

        public bool IsNumeric
        {
            set
            {
                isNumeric = value;
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
            }
            get
            {
                return isMoney;
            }
        }

        private decimal value;

        public decimal Value
        {
            get
            {
                return value;
            }
        }

        public delegate void KeyEvent(object sender, KeyEventArgs e);
        public delegate void OnGainFocusEvent(object sender, RoutedEventArgs e);
        public delegate void OnLostFocusEvent(object sender, RoutedEventArgs e);

        public event KeyEvent InputKeyDown;
        public event KeyEvent InputKeyUp;
        public event OnLostFocusEvent InputLostFocus;
        public event OnGainFocusEvent InputGainFocus;

        public Input()
        {
            InitializeComponent();

            this.MinHeight = 56;
            txInput.KeyDown += TxInput_KeyDown;
            txInput.KeyUp += TxInput_KeyUp;
            txInput.LostFocus += TxInput_LostFocus;
            txInput.GotFocus += TxInput_GotFocus;
            Enabled = true;
        }

        private void TxInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if (InputGainFocus != null) InputGainFocus(sender, e);
            txInput.SelectAll();
        }

        private void TxInput_LostFocus(object sender, RoutedEventArgs e)
        {

            if (InputLostFocus != null) InputLostFocus(sender, e);

            if (isMoney)
            {
                string text = txInput.Text;
                if (!string.IsNullOrEmpty(text))
                    value = decimal.Parse(text);
            }
        }

        public void SetFocused()
        {
            txInput.Focus();
        }

        private void TxInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (InputKeyUp != null) InputKeyUp(sender, e);
        }

        private void TxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (InputKeyDown != null) InputKeyDown(sender, e);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
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

                    if (e.Text.Equals(".") && (txInput.Text.Last().Equals('.') || txInput.Text.Last().Equals(',')))
                        return;

                    if (e.Text.Equals(",") && (txInput.Text.Last().Equals('.') || txInput.Text.Last().Equals(',')))
                        return;

                    txInput.Text += e.Text;
                    txInput.SelectionStart = txInput.Text.Length; // add some logic if length is 0
                    txInput.SelectionLength = 0;
                }
            }
        }
    }
}
