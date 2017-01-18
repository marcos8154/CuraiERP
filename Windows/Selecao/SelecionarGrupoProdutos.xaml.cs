using EM3.Controller;
using EM3.Extensions;
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
using System.Windows.Shapes;

namespace EM3.Windows.Selecao
{
    /// <summary>
    /// Lógica interna para SelecionarGrupoProdutos.xaml
    /// </summary>
    public partial class SelecionarGrupoProdutos : Window
    {
        public Grupos_produtos Selecionado = new Grupos_produtos();

        public SelecionarGrupoProdutos()
        {
            InitializeComponent();

            dataGrid.AplicarPadroes();
            Pesquisar();
        }

        private void Pesquisar()
        {
            List<Grupos_produtos> grupos = Grupos_produtosController.Search(txPesquisa.Text, 1);
            dataGrid.ItemsSource = grupos;
        }

        private void btSelecionar_OnClick()
        {
            Selecionar();
        }

        private void Selecionar()
        {
            Grupos_produtos grupo = (Grupos_produtos)dataGrid.SelectedItem;
            if (grupo == null)
                return;
            if (grupo.Id == 0)
                return;

            Selecionado = grupo;
            Close();
        }

        private void btCancelar_OnClick()
        {
            Close();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Selecionar();
        }
    }
}
