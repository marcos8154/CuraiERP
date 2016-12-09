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

namespace EM3.UserControls.Estoque.Caracteristica
{
    /// <summary>
    /// Interação lógica para VCaracteristicas.xam
    /// </summary>
    public partial class VCaracteristicas : UserControl
    {
        private CCaracteristicas cadastro = new CCaracteristicas();
        private CaracteristicasContainer Container;

        public VCaracteristicas(CaracteristicasContainer container)
        {
            InitializeComponent();

            this.Container = container;
            dataGrid.AplicarPadroes();
        }

        private void btNovo_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.INSERIR))
                return;

            cadastro = new CCaracteristicas();
            Container.GridContainer.Children.Remove(this);
            Container.GridContainer.Children.Add(cadastro);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void Cadastro_OnComplete()
        {
            Container.GridContainer.Children.Remove(cadastro);
            Container.GridContainer.Children.Add(this);
            dataGrid.Items.Refresh();
        }

        private void Pesquisar()
        {
            List<Caracteristicas> list = CaracteristicasController.Search(txPesquisa.Text);
            dataGrid.ItemsSource = list;
        }

        private void btAlterar_OnClick()
        {
            Alterar();
        }

        private void Alterar()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.ATUALIZAR))
                return;

            Caracteristicas caract = (Caracteristicas)dataGrid.SelectedItem;

            if (caract == null) return;
            if (caract.Id == 0) return;

            cadastro = new CCaracteristicas();
            cadastro.Load(caract.Id);

            Container.GridContainer.Children.Remove(this);
            Container.GridContainer.Children.Add(cadastro);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Alterar();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Pesquisar();
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void btExcluir_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.EXCLUIR)) return;

            Caracteristicas c = (Caracteristicas)dataGrid.SelectedItem;

            if (c == null) return;
            if (c.Id == 0) return;

            if (!(new MsgSimNao("Confirma exclusão da característica '" + c.Atributo + ": " + c.Valor + "'? Esta ação não poderá ser revertida.").Result))
                return;

            if (CaracteristicasController.Delete(c.Id))
                Pesquisar();
        }
    }
}
