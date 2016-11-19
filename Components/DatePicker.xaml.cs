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
    /// Interação lógica para DatePicker.xam
    /// </summary>
    public partial class DatePicker : UserControl
    {

        public bool Enabled
        {
            get
            {
                return txDate.IsEnabled;
            }
            set
            {
                txDate.IsEnabled = value;
            }
        }

        public Visibility Visible
        {
            get
            {
                return txDate.Visibility;
            }
            set
            {
                txDate.Visibility = value;
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

        public DatePicker()
        {
            InitializeComponent();

            this.MinHeight = 56;
        }

        public DateTime Value
        {
            get
            {
                if (!txDate.SelectedDate.HasValue) return new DateTime();
                return (DateTime)txDate.SelectedDate;
            }
            set
            {
                txDate.SelectedDate = (DateTime)value;
            }
        }
    }
}
