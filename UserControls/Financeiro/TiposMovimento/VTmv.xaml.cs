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

namespace EM3.UserControls.Financeiro.TiposMovimento
{
    /// <summary>
    /// Interação lógica para VTmv.xam
    /// </summary>
    public partial class VTmv : UserControl
    {
        TmvContainer container;
        CTmv cadastro;

        public VTmv(TmvContainer container)
        {
            InitializeComponent();

            this.container = container;
            dataGrid.AplicarPadroes();
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void btNovo_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(container.Tela_id, Enums.TipoPermissao.INSERIR))
                return;

            cadastro = new CTmv();
            container.GridContainer.Children.Remove(this);
            container.GridContainer.Children.Add(cadastro);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void Cadastro_OnComplete()
        {
            try
            {
                container.GridContainer.Children.Remove(cadastro);
                container.GridContainer.Children.Add(this);
                dataGrid.Items.Refresh();
            }
            catch { }
        }

        private void btExcluir_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(container.Tela_id, Enums.TipoPermissao.EXCLUIR))
                return;

            Tipos_movimento tmv = (Tipos_movimento)dataGrid.SelectedItem;

            if (tmv == null)
                return;
            if (tmv.Id == 0)
                return;

            if (!(new MsgSimNao("Comfirmar exclusão do tipo de movimento '" + tmv.Descricao + "'? Esta ação não pode ser revertida!").Result))
                return;

            if (Tipos_movimentoController.Delete(tmv.Id))
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

            Tipos_movimento tmv = (Tipos_movimento)dataGrid.SelectedItem;

            if (tmv == null)
                return;
            if (tmv.Id == 0)
                return;

            cadastro = new CTmv();
            cadastro.Load(tmv.Id);
            container.GridContainer.Children.Remove(this);
            container.GridContainer.Children.Add(cadastro);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void Pesquisar()
        {
            int tipo = (int)cbExibir.SelectedValue;
            List<Tipos_movimento> list = Tipos_movimentoController.Search(txPesquisa.Text, tipo);
            dataGrid.ItemsSource = list;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<KeyValuePair<int, string>> itemsExibir = new List<KeyValuePair<int, string>>();

            itemsExibir.Add(new KeyValuePair<int, string>(2, "Todos"));
            itemsExibir.Add(new KeyValuePair<int, string>(1, "Somente ativos"));
            itemsExibir.Add(new KeyValuePair<int, string>(0, "Somente inativos"));

            cbExibir.SetItemsSource(itemsExibir);
            Pesquisar();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Alterar();
        }
    }
}
