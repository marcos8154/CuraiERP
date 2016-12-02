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

namespace EM3.UserControls.Estoque.GruposProdutos
{
    /// <summary>
    /// Interação lógica para Grupos_produtoContainer.xam
    /// </summary>
    public partial class Grupos_produtoContainer : UserControl
    {
        public string Tela_id = "5";

        public Grupos_produtoContainer()
        {
            InitializeComponent();

            GridContainer.Children.Add(new VGrupos_produtos(this));
        }
    }
}
