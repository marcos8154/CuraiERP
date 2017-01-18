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

namespace EM3.UserControls.Estoque.Produto
{
    /// <summary>
    /// Interação lógica para ProdutosContainer.xam
    /// </summary>
    public partial class ProdutosContainer : UserControl
    {
        public string Tela_id = "6";
        public ProdutosContainer()
        {
            InitializeComponent();
            GridContainer.Children.Add(new VProdutos(this));
        }
    }
}
