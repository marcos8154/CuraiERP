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

namespace EM3.UserControls.Financeiro.Operadora_cartao
{
    /// <summary>
    /// Interação lógica para VOperadoras_c.xam
    /// </summary>
    public partial class VOperadoras_c : UserControl
    {
        Operadora_cartaoContainer Container;
        COperadora_cartao cadastro;

        public VOperadoras_c(Operadora_cartaoContainer container)
        {
            InitializeComponent();
            Container = container;
        }

        private void btNovo_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.INSERIR))
                return;

            cadastro = new COperadora_cartao();
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

        private void btExcluir_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.EXCLUIR))
                return;

            Operadoras_cartao operadora = (Operadoras_cartao)dataGrid.SelectedItem;
            if (operadora == null)
                return;
            if (operadora.Id == 0)
                return;

            if (!(new MsgSimNao($"Confirmar exclusão da operadora {operadora.Nome}?").Result))
                return;

            if (Operadoras_cartaoController.Remove(operadora.Id))
                Pesquisar();
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void Alterar()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.ATUALIZAR))
                return;

            Operadoras_cartao operadora = (Operadoras_cartao)dataGrid.SelectedItem;
            if (operadora == null)
                return;
            if (operadora.Id == 0)
                return;

            cadastro = new COperadora_cartao();
            cadastro.Load(operadora.Id);
            Container.GridContainer.Children.Remove(this);
            Container.GridContainer.Children.Add(cadastro);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void Pesquisar()
        {
            List<Operadoras_cartao> result = Operadoras_cartaoController.Search(txPesquisa.Text);
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
