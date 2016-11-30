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

namespace EM3.UserControls.Estoque.Armazem
{
    /// <summary>
    /// Interação lógica para VArmazens.xam
    /// </summary>
    public partial class VArmazens : UserControl
    {
        private ArmazemContainer Container;
        private CArmazens cadastro = new CArmazens();

        public VArmazens(ArmazemContainer ac)
        {
            InitializeComponent();
            this.Container = ac;
            dataGrid.AplicarPadroes();
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void btNovo_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.INSERIR))
                return;

            cadastro = new CArmazens();
            Container.GridContainer.Children.Remove(this);
            Container.GridContainer.Children.Add(cadastro);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void Cadastro_OnComplete()
        {
            Container.GridContainer.Children.Remove(cadastro);
            Container.GridContainer.Children.Add(this);
            Pesquisar();
        }

        private void Pesquisar()
        {
            List<Armazens> list = ArmazensController.Search(txPesquisa.Text);
            dataGrid.ItemsSource = list;
        }

        private void btExcluir_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.EXCLUIR))
                return;

            Armazens armz = (Armazens)dataGrid.SelectedItem;
            if (armz == null) return;
            if (armz.Id == 0) return;

            if (!(new MsgSimNao("Confirma exclusão do armazém '" + armz.Nome + "'? Esta ação não poderá ser desfeita.").Result))
                return;

            if (ArmazensController.Delete(armz.Id))
                Pesquisar();
        }

        private void btAlterar_OnClick()
        {
            Alterar();
        }

        private void Alterar()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.ATUALIZAR))
                return;

            Armazens armz = (Armazens)dataGrid.SelectedItem;

            if (armz == null) return;
            if (armz.Id == 0) return;

            cadastro = new CArmazens();
            cadastro.Load(armz.Id);
            Container.GridContainer.Children.Remove(this);
            Container.GridContainer.Children.Add(cadastro);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Pesquisar();
        }
    }
}
