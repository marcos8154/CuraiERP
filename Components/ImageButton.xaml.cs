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
    public partial class ImageButton : UserControl
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

        private string imageSource = string.Empty;
        public string ImageSource
        {
            get
            {
                return this.imageSource;
            }
            set
            {
                try
                {
                    imageSource = "pack://application:,,,/Images/" + value;
                    imageNormal.Source = new BitmapImage(new Uri(imageSource));
                    imagePressed.Source = new BitmapImage(new Uri(imageSource));
                }
                catch(Exception ex) { }
            }
        }

        public ImageButton()
        {
            InitializeComponent();
            pressed.Visibility = Visibility.Visible;
            normal.Visibility = Visibility.Hidden;
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
