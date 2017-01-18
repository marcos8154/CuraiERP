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
    /// Lógica interna para SelecionarLocalEstoque.xaml
    /// </summary>
    public partial class SelecionarLocalEstoque : Window
    {
        public Locais_estoque Selecionado = new Locais_estoque();

        public SelecionarLocalEstoque()
        {
            InitializeComponent();

            dataGrid.AplicarPadroes();
            Pesquisar();
        }

        private void Pesquisar()
        {
            List<Locais_estoque> list = Locais_estoqueController.Search(txPesquisa.Text);
            dataGrid.ItemsSource = list;
        }

        private void btSelecionar_OnClick()
        {
            Selecionar();
        }

        private void Selecionar()
        {
            Locais_estoque local = (Locais_estoque)dataGrid.SelectedItem;
            if (local == null)
                return;
            if (local.Id == 0)
                return;

            Selecionado = local;
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

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }
    }
}
