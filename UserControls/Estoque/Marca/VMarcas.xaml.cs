using EM3.Controller;
using EM3.Extensions;
using EM3.Model;
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

namespace EM3.UserControls.Estoque.Marca
{
    /// <summary>
    /// Interação lógica para VMarcas.xam
    /// </summary>
    public partial class VMarcas : UserControl
    {
        MarcasContainer Container;
        CMarcas cadastro;
        public VMarcas(MarcasContainer container)
        {
            InitializeComponent();

            Container = container;
            dataGrid.AplicarPadroes();
        }

        private void btNovo_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.INSERIR))
                return;

            cadastro = new CMarcas();
            Container.GridContainer.Children.Add(cadastro);
            Container.GridContainer.Children.Remove(this);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void Cadastro_OnComplete()
        {
            Container.GridContainer.Children.Add(this);
            Container.GridContainer.Children.Remove(cadastro);
            Pesquisar();
        }

        private void btAlterar_OnClick()
        {
            Alterar();
        }

        private void btExcluir_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.EXCLUIR))
                return;

            Marcas marca = (Marcas)dataGrid.SelectedItem;
            if (marca == null)
                return;
            if (marca.Id == 0)
                return;

            if (new MsgSimNao($"Confirmar exclusão da marca {marca.Nome}?").Result)
            {
                if (MarcasController.Remove(marca.Id))
                {
                    FotoController.Remove(marca.Foto_id);
                    Pesquisar();
                }
            }
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        public void Pesquisar()
        {
            List<Marcas> list = MarcasController.Search(txPesquisa.Text);
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

        private void Alterar()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.ATUALIZAR))
                return;

            Marcas marca = (Marcas)dataGrid.SelectedItem;
            if (marca == null)
                return;
            if (marca.Id == 0)
                return;

            cadastro = new CMarcas();
            cadastro.Load(marca.Id);
            Container.GridContainer.Children.Add(cadastro);
            Container.GridContainer.Children.Remove(this);
            cadastro.OnComplete += Cadastro_OnComplete;
        }
    }
}
