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

namespace EM3.UserControls.Financeiro.Operadora_cartao
{
    /// <summary>
    /// Interação lógica para Operadora_cartaoContainer.xam
    /// </summary>
    public partial class Operadora_cartaoContainer : UserControl
    {
        public string Tela_id = "22";
        public Operadora_cartaoContainer()
        {
            InitializeComponent();
            GridContainer.Children.Add(new VOperadoras_c(this));
        }
    }
}
