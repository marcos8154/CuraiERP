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
    /// Lógica interna para SelecionarProduto.xaml
    /// </summary>
    public partial class SelecionarProduto : Window
    {
        public Produtos Selecionado = new Produtos();
        public SelecionarProduto()
        {
            InitializeComponent();

            int countProduto = ProdutosController.Count();
            int pages = (countProduto / 300);
            paginator.MaxPages = pages;
            paginator.IntervalChangeNumber = 300;
        }

        private void paginator_OnPageChange(int page)
        {
            Pesquisar();
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void Pesquisar()
        {
            List<Produtos> list = ProdutosController.Search(txPesquisa.Text, 1, paginator.CurrentPage);
            dataGrid.ItemsSource = list;
        }

        private void btSelecionar_OnClick()
        {
            Selecionar();
        }

        private void Selecionar()
        {
            Produtos produto = (Produtos)dataGrid.SelectedItem;
            if (produto == null)
                return;
            if (produto.Id == 0)
                return;

            Selecionado = produto;
            Close();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Selecionar();
        }

        private void btCancelar_OnClick()
        {
            Close();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Pesquisar();
            dataGrid.AplicarPadroes();
        }
    }
}
