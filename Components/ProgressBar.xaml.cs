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
    /// Interação lógica para ProgressBar.xam
    /// </summary>
    public partial class ProgressBar : UserControl
    {
        private double max = 0;
        public double Maximum
        {
            get
            {
                return max;
            }
            set
            {
                max = this.Width / value;
            }
        }

        public ProgressBar()
        {
            InitializeComponent();
            gridProgress.Width = 0;
        }

        public void Incresses(double value)
        {
            gridProgress.Width += (max / value);
        }
    }
}
