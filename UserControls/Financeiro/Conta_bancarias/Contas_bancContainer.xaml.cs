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

namespace EM3.UserControls.Financeiro.Conta_bancarias
{
    /// <summary>
    /// Interação lógica para Contas_bancContainer.xam
    /// </summary>
    public partial class Contas_bancContainer : UserControl
    {
        public string Tela_id = "24";

        public Contas_bancContainer()
        {
            InitializeComponent();
            GridContainer.Children.Add(new VContas_banc(this));
        }
    }
}
