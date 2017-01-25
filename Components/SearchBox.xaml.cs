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

namespace EM3.Components
{
    /// <summary>
    /// Interação lógica para Input.xam
    /// </summary>
    public partial class SearchBox : UserControl
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
                {
                    txInput.Background = Brushes.White;
                    border.Background = Brushes.White;
                }
                else
                {
                    txInput.Background = Brushes.WhiteSmoke;
                    border.Background = Brushes.WhiteSmoke;
                }
                this.IsEnabled = value;
            }
        }

        public string HelpName { get; set; }

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
                if (IsNumeric)
                    this.value = int.Parse(value);
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

        public bool IsNumeric
        {
            set
            {
                if (!value)
                    txInput.Text = string.Empty;
                isNumeric = value;
            }
            get
            {
                return isNumeric;
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

        private int value;

        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                txInput.Text = value.ToString();
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

        public delegate void KeyUpEvt(object sender, KeyEventArgs e);
        public delegate void KeyDownEvt(object sender, KeyEventArgs e);
        public delegate void SearchOpenWindow();

        public event KeyDownEvt InputKeyDown;
        public event KeyUpEvt InputKeyUp;
        public event SearchOpenWindow CallSearch;

        public SearchBox()
        {
            InitializeComponent();

            this.MinHeight = 56;
            txInput.KeyDown += TxInput_KeyDown;
            txInput.KeyUp += TxInput_KeyUp;
            this.IsNumeric = true;
            CharacterCasing = CharacterCasing.Upper;
            this.GotFocus += SearchBox_GotFocus;
            this.LostFocus += SearchBox_LostFocus;
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            border.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FFACA6A6");
        }
        
        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            border.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FF4DA1F5");
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
            if (e.Key == Key.F1)
                new Help(HelpName);
            if (InputKeyDown != null) InputKeyDown(sender, e);

            if (IsNumeric)
            {
                if (e.Key == Key.F3)
                    if (CallSearch != null) CallSearch();
            }
            else
            {
                if (e.Key == Key.Enter)
                    if (CallSearch != null) CallSearch();
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            if (isNumeric)
            {
                Regex rgxNumbers = new Regex("[^0-9]+");
                e.Handled = (rgxNumbers.IsMatch(e.Text));
            }
        }

        private void txInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsNumeric)
            {
                string text = txInput.Text;
                if (!string.IsNullOrEmpty(text))
                    value = int.Parse(text);
                else
                    value = 0;
            }
        }

        private void img_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (CallSearch != null) CallSearch();
        }
    }
}
