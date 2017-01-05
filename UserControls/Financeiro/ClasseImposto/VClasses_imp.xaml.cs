using EM3.Controller;
using EM3.Extensions;
using EM3.UserControls.Financeiro.Operacoes_classeImp;
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

namespace EM3.UserControls.Financeiro.ClasseImposto
{
    /// <summary>
    /// Interação lógica para VClasses_imp.xam
    /// </summary>
    public partial class VClasses_imp : UserControl
    {
        CLImpContainer container;
        CCImp cadastro;
        Op_classeImpContainer vOperacoes_container;

        public VClasses_imp(CLImpContainer c)
        {
            InitializeComponent();

            this.container = c;
            dataGrid.AplicarPadroes();
        }

        private void Pesquisar()
        {
            List<Classes_imposto> list = Classes_impostoController.Search(txPesquisa.Text);
            dataGrid.ItemsSource = list;
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void btNovo_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(container.Tela_id, Enums.TipoPermissao.INSERIR))
                return;

            cadastro = new CCImp();
            container.GridContainer.Children.Add(cadastro);
            container.GridContainer.Children.Remove(this);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void Cadastro_OnComplete()
        {
            try
            {
                container.GridContainer.Children.Add(this);
                container.GridContainer.Children.Remove(cadastro);
                dataGrid.Items.Refresh();
            }
            catch { }
        }

        private void btExcluir_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(container.Tela_id, Enums.TipoPermissao.EXCLUIR))
                return;

            Classes_imposto classe = (Classes_imposto)dataGrid.SelectedItem;
            if (classe == null) return;
            if (classe.Id == 0) return;

            if (!(new MsgSimNao("Confirmar exclusão da classe de imposto '" + classe.Nome + "'? Esta ação não poderá ser revertida!").Result))
                return;

            if (Classes_impostoController.Delete(classe.Id))
                Pesquisar();
        }

        private void btAlterar_OnClick()
        {
            Alterar();
        }

        private void Alterar()
        {
            if (!UsuariosController.ValidaPermissao(container.Tela_id, Enums.TipoPermissao.ATUALIZAR))
                return;

            Classes_imposto classe = (Classes_imposto)dataGrid.SelectedItem;
            if (classe == null) return;
            if (classe.Id == 0) return;

            cadastro = new CCImp();
            cadastro.Load(classe.Id);
            container.GridContainer.Children.Add(cadastro);
            container.GridContainer.Children.Remove(this);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Pesquisar();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Alterar();
        }

        private void btOperacoes_OnClick()
        {
            Classes_imposto cl_imp = (Classes_imposto)dataGrid.SelectedItem;
            if (cl_imp == null) return;
            if (cl_imp.Id == 0) return;

            vOperacoes_container = new Op_classeImpContainer(cl_imp.Id, container.Tela_id);
            container.GridContainer.Children.Add(vOperacoes_container);
            container.GridContainer.Children.Remove(this);
            vOperacoes_container.OnBack += VOperacoes_OnBack;
        }

        private void VOperacoes_OnBack()
        {
            container.GridContainer.Children.Add(this);
            container.GridContainer.Children.Remove(vOperacoes_container);
        }
    }
}
