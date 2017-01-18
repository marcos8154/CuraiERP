using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interação lógica para Button.xam
    /// </summary>
    /// 
    public partial class NormalButton : UserControl
    {
        private bool isFontBold = false;
        public bool Enabled
        {
            get
            {
                return this.IsEnabled;
            }
            set
            {
                this.IsEnabled = value;
            }
        }

        public bool IsFontBold
        {
            set
            {
                isFontBold = value;
            }
            get
            {
                return isFontBold;
            }
        }

        public Visibility Visible
        {
            get
            {
                return this.Visibility;
            }
            set
            {
                this.Visibility = value;
            }
        }


        public delegate void Clicked();
        public event Clicked OnClick;

        public double SizeFont
        {
            get
            {
                return lbTextNormal.FontSize;
            }
            set
            {
                lbTextNormal.FontSize = value;
                lbTextPressed.FontSize = value;
            }
        }

        public string Text
        {
            get
            {
                return lbTextNormal.Content.ToString();
            }
            set
            {
                lbTextNormal.Content = value;
                lbTextPressed.Content = value;
            }
        }

        public NormalButton()
        {
            InitializeComponent();
            pressed.Visibility = Visibility.Visible;
            normal.Visibility = Visibility.Hidden;

            this.Loaded += NormalButton_Loaded;
        }

        private void NormalButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (isFontBold)
            {
                lbTextNormal.FontWeight = FontWeights.Bold;
                lbTextPressed.FontWeight = FontWeights.Bold;
            }
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            pressed.Visibility = Visibility.Hidden;
            normal.Visibility = Visibility.Visible;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            pressed.Visibility = Visibility.Visible;
            normal.Visibility = Visibility.Hidden;
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            pressed.Visibility = Visibility.Visible;
            normal.Visibility = Visibility.Hidden;
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            pressed.Visibility = Visibility.Hidden;
            normal.Visibility = Visibility.Visible;

            if (OnClick != null) OnClick();
        }
    }
}
