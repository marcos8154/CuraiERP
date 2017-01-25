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

namespace EM3.UserControls.Financeiro.Tabela_preco
{
    /// <summary>
    /// Interação lógica para Tabela_precoContainer.xam
    /// </summary>
    public partial class Tabela_precoContainer : UserControl
    {
        public string Tela_id = "27";
        public Tabela_precoContainer()
        {
            InitializeComponent();
            GridContainer.Children.Add(new VTabelas_precos(this));
        }
    }
}
