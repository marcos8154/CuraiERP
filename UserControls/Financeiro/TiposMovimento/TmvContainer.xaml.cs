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

namespace EM3.UserControls.Financeiro.TiposMovimento
{
    /// <summary>
    /// Interação lógica para TmvContainer.xam
    /// </summary>
    public partial class TmvContainer : UserControl
    {
        public string Tela_id = "19";

        public TmvContainer()
        {
            InitializeComponent();
            GridContainer.Children.Add(new VTmv(this));
        }
    }
}
