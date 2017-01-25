using EM3.Controller;
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

        public int Index
        {
            get
            {
                return txDate.TabIndex;
            }
            set
            {
                txDate.TabIndex = value;
            }
        }

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

        public DatePicker()
        {
            InitializeComponent();

            this.MinHeight = 56;
            if (currentDate)
                Value = Commons.ServerDate;
        }

        private bool currentDate = false;
        public bool CurrentDate
        {
            get
            {
                return currentDate;
            }
            set
            {
                currentDate = value;
            }
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
