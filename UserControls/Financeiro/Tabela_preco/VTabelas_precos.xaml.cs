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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EM3.UserControls.Financeiro.Tabela_preco
{
    /// <summary>
    /// Interação lógica para VTabelas_precos.xam
    /// </summary>
    public partial class VTabelas_precos : UserControl
    {
        Tabela_precoContainer Container;
        CTabelas_precos cadastro;
        public VTabelas_precos(Tabela_precoContainer container)
        {
            InitializeComponent();
            Container = container;
            dataGrid.AplicarPadroes();
        }

        private void btNovo_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.INSERIR))
                return;

            cadastro = new CTabelas_precos();
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

        private void btAlterar_OnClick()
        {
            Alterar();
        }

        private void Alterar()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.ATUALIZAR))
                return;

            Tabelas_precos tabela = (Tabelas_precos)dataGrid.SelectedItem;
            if (tabela == null)
                return;
            if (tabela.Id == 0)
                return;

            cadastro = new CTabelas_precos();
            cadastro.Load(tabela.Id);
            Container.GridContainer.Children.Add(cadastro);
            Container.GridContainer.Children.Remove(this);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void btExcluir_OnClick()
        {

        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void Pesquisar()
        {
            int tipo = 2;
            if (cbExibir.SelectedIndex == 0)
                tipo = 2;
            if (cbExibir.SelectedIndex == 1)
                tipo = 1;
            if (cbExibir.SelectedIndex == 2)
                tipo = 0;

            List<Tabelas_precos> list = Tabelas_precoController.Search(txPesquisa.Text, tipo);
            dataGrid.ItemsSource = list;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Pesquisar();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Alterar();
        }
    }
}
