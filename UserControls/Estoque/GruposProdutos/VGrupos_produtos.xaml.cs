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

namespace EM3.UserControls.Estoque.GruposProdutos
{
    /// <summary>
    /// Interação lógica para VGrupos_produtos.xam
    /// </summary>
    public partial class VGrupos_produtos : UserControl
    {
        private Grupos_produtoContainer Container;
        private CGrupos_produtos cadastro = new CGrupos_produtos();

        public VGrupos_produtos(Grupos_produtoContainer c)
        {
            InitializeComponent();

            this.Container = c;
            dataGrid.AplicarPadroes();
            Pesquisar();
        }

        private void Pesquisar()
        {
            List<Grupos_produtos> list = Grupos_produtosController.Search(txPesquisa.Text);
            dataGrid.ItemsSource = list;
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void btNovo_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.INSERIR))
                return;

            cadastro = new CGrupos_produtos();
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

        private void btAlterar_OnClick()
        {
            Alterar();
        }

        private void btExcluir_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id,
                Enums.TipoPermissao.EXCLUIR))
                return;

            Grupos_produtos gprods = (Grupos_produtos)dataGrid.SelectedItem;

            if (gprods == null) return;
            if (gprods.Id == 0) return;

            if (!(new MsgSimNao("Confirmar exclusão do grupo de produtos '" + gprods.Descricao + "'? Esta opção não pode ser revertida!").Result)) return;
            if (Grupos_produtosController.Delete(gprods.Id))
                Pesquisar();
        }

        private void Alterar()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.ATUALIZAR))
                return;

            Grupos_produtos gp = (Grupos_produtos)dataGrid.SelectedItem;

            if (gp == null) return;
            if (gp.Id == 0) return;

            cadastro = new CGrupos_produtos();
            cadastro.Load(gp.Id);
            Container.GridContainer.Children.Remove(this);
            Container.GridContainer.Children.Add(cadastro);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Alterar();
        }
    }
}
