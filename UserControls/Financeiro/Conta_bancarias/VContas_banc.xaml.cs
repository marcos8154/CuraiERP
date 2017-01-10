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

namespace EM3.UserControls.Financeiro.Conta_bancarias
{
    /// <summary>
    /// Interação lógica para VContas_banc.xam
    /// </summary>
    public partial class VContas_banc : UserControl
    {
        Contas_bancContainer Container;
        CContas_bancarias cadastro;
        public VContas_banc(Contas_bancContainer container)
        {
            InitializeComponent();
            Container = container;
        }

        private void btNovo_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.INSERIR))
                return;

            cadastro = new CContas_bancarias();
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

            Contas_bancarias conta = (Contas_bancarias)dataGrid.SelectedItem;
            if (conta == null)
                return;
            if (conta.Id == 0)
                return;

            cadastro = new CContas_bancarias();
            cadastro.Load(conta.Id);
            Container.GridContainer.Children.Add(cadastro);
            Container.GridContainer.Children.Remove(this);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void btExcluir_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.ATUALIZAR))
                return;

            Contas_bancarias conta = (Contas_bancarias)dataGrid.SelectedItem;
            if (conta == null)
                return;
            if (conta.Id == 0)
                return;

            if (!(new MsgSimNao($"Confirmar exclusão da conta {conta.Nome}? Esta ação não poderá ser revertida!").Result))
                return;

            if (Contas_bancariasController.Remove(conta.Id))
                Pesquisar();
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void Pesquisar()
        {
            int tipo = 2;
            if (cbExibir.SelectedIndex == 0)
                tipo = 2; // todods
            if (cbExibir.SelectedIndex == 1)
                tipo = 1; //apenas ativos
            if (cbExibir.SelectedIndex == 2)
                tipo = 0; //apenas inativos

            List<Contas_bancarias> result = Contas_bancariasController.Search(txPesquisa.Text, tipo);
            dataGrid.ItemsSource = result;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Pesquisar();
            dataGrid.AplicarPadroes();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Alterar();
        }
    }
}
