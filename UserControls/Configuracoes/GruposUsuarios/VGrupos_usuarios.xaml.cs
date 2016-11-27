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

namespace EM3.UserControls.Configuracoes.GruposUsuarios
{
    /// <summary>
    /// Interação lógica para VGrupos_usuarios.xam
    /// </summary>
    public partial class VGrupos_usuarios : UserControl
    {
        Grupos_usuariosContainer Container;
        CGrupos_usuarios cadastro;

        public VGrupos_usuarios(Grupos_usuariosContainer container)
        {
            InitializeComponent();
            this.Container = container;
            dataGrid.AplicarPadroes();
        }

        private void btNovo_OnClick()
        {
            cadastro = new CGrupos_usuarios();
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
            List<Grupos_usuarios> lista = Grupos_usuariosController.Search(txPesquisa.Text);
            dataGrid.ItemsSource = lista;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Pesquisar();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Alterar();
        }

        private void Alterar()
        {
            Grupos_usuarios grupo = (Grupos_usuarios)dataGrid.SelectedItem;
            if (grupo == null) return;
            if (grupo.Id == 0) return;

            cadastro = new CGrupos_usuarios();
            cadastro.Load(grupo.Id);

            Container.GridContainer.Children.Remove(this);
            Container.GridContainer.Children.Add(cadastro);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void btExcluir_OnClick()
        {
            Grupos_usuarios grupo = (Grupos_usuarios)dataGrid.SelectedItem;

            if (!(new MsgSimNao("Deseja excluir o grupo de usuários '" + grupo.Nome + "'? Esta ação não poderá ser revertida").Result))
                return;

            if (grupo == null)
                return;
            if (grupo.Id == 0)
                return;

            if (Grupos_usuariosController.Remove(grupo.Id))
                Pesquisar();
        }

        private void btAlterar_OnClick()
        {
            Alterar();
        }
    }
}
