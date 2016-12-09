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

namespace EM3.UserControls.Financeiro.ClasseImposto
{
    /// <summary>
    /// Interação lógica para CLImpContainer.xam
    /// </summary>
    public partial class CLImpContainer : UserControl
    {
        public string Tela_id = "20";
        public CLImpContainer()
        {
            InitializeComponent();
            GridContainer.Children.Add(new VClasses_imp(this));
        }
    }
}
