using EM3.Controller;
using EM3.Extensions;
using EM3.Windows;
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

namespace EM3.UserControls.Estoquev.Produto
{
    /// <summary>
    /// Interação lógica para VProdutos.xam
    /// </summary>
    public partial class VProdutos : UserControl
    {
        ProdutosContainer Container;
        CProdutos cadastro;

        public VProdutos(ProdutosContainer container)
        {
            InitializeComponent();
            this.Container = container;
        }

        private void btExcluir_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.EXCLUIR))
                return;

            Produtos produto = (Produtos)dataGrid.SelectedItem;
            if (produto == null)
                return;
            if (produto.Id == 0)
                return;

            if (MsgSimNao.Show($@"Confirmar exclusão do produto {produto.Descricao}?
Esta ação não poderá ser revertida!").Result)
            {
                if (ProdutosController.Remove(produto.Id))
                    Pesquisar();
            }
        }

        private void btAlterar_OnClick()
        {
            Alterar();
        }

        private void btNovo_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.INSERIR))
                return;

            cadastro = new CProdutos();
            Container.GridContainer.Children.Add(cadastro);
            Container.GridContainer.Children.Remove(this);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void Cadastro_OnComplete()
        {
            Container.GridContainer.Children.Add(this);
            Container.GridContainer.Children.Remove(cadastro);
            dataGrid.Items.Refresh();
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void Pesquisar()
        {
            List<Produtos> list = ProdutosController.Search(txPesquisa.Text, 2, paginador.CurrentPage);
            dataGrid.ItemsSource = list;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.AplicarPadroes();
            int count = ProdutosController.Count();
            int maxPages = (count / 300);
            paginador.MaxPages = maxPages;
            paginador.IntervalChangeNumber = 300;
            Pesquisar();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Alterar();
        }

        private void Alterar()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.ATUALIZAR))
                return;

            Produtos produto = (Produtos)dataGrid.SelectedItem;
            if (produto == null)
                return;
            if (produto.Id == 0)
                return;

            cadastro = new CProdutos();
            cadastro.Load(produto.Id);
            Container.GridContainer.Children.Add(cadastro);
            Container.GridContainer.Children.Remove(this);
            cadastro.OnComplete += Cadastro_OnComplete;
        }
    }
}
