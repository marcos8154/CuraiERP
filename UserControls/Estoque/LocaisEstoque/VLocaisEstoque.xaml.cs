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

namespace EM3.UserControls.Estoque.LocaisEstoque
{
    /// <summary>
    /// Interação lógica para VLocaisEstoque.xam
    /// </summary>
    public partial class VLocaisEstoque : UserControl
    {
        private LocaisEstoqueContainer Container;
        private CLocaisEstoque cadastro;

        public VLocaisEstoque(LocaisEstoqueContainer container)
        {
            InitializeComponent();

            this.Container = container;
            dataGrid.AplicarPadroes();
        }

        private void btNovo_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.INSERIR))
                return;

            cadastro = new CLocaisEstoque();
            Container.GridContainer.Children.Remove(this);
            Container.GridContainer.Children.Add(cadastro);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void Cadastro_OnComplete()
        {
            try
            {
                Container.GridContainer.Children.Remove(cadastro);
                Container.GridContainer.Children.Add(this);
                dataGrid.Items.Refresh();
            }
            catch { }
        }

        private void btAlterar_OnClick()
        {
            Alterar();
        }

        private void Alterar()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.ATUALIZAR))
                return;

            Locais_estoqueAdapter le_a = (Locais_estoqueAdapter)dataGrid.SelectedItem;

            if (le_a == null) return;
            if (le_a.Id == 0) return;

            cadastro = new CLocaisEstoque();
            cadastro.Load(le_a.Id);
            Container.GridContainer.Children.Remove(this);
            Container.GridContainer.Children.Add(cadastro);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void btExcluir_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.EXCLUIR))
                return;

            Locais_estoqueAdapter le_a = (Locais_estoqueAdapter)dataGrid.SelectedItem;
            if (le_a == null) return;
            if (le_a.Id == 0) return;

            if (!(new MsgSimNao("Confirma a exclusão do local de estoque '" + le_a.Nome + "'? Esta ação não poderá ser revertida!").Result))
                return;

            if (Locais_estoqueController.Delete(le_a.Id))
                Pesquisar();
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void Pesquisar()
        {
            List<Locais_estoque> list = Locais_estoqueController.Search(txPesquisa.Text);
            List<Locais_estoqueAdapter> listAdapters = new List<Locais_estoqueAdapter>();

            list.ForEach(e => listAdapters.Add(
                new Locais_estoqueAdapter()
                {
                    Id = e.Id,
                    Nome = e.Nome,
                    Altura = (e.Altura + " " + e.Unidade_altura),
                    Largura = (e.Largura + " " + e.Unidade_largura),
                    Comprimento = (e.Comprimento + " " + e.Unidade_compr),
                    Armazem = e.Armazens == null ? string.Empty : e.Armazens.Nome
                }
            ));
            dataGrid.ItemsSource = listAdapters;
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

    public class Locais_estoqueAdapter
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Armazem { get; set; }
        public string Largura { get; set; }
        public string Altura { get; set; }
        public string Comprimento { get; set; }
    }
}
