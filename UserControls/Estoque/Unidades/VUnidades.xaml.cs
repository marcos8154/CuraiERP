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

namespace EM3.UserControls.Estoque.UnidadesModulo
{
    /// <summary>
    /// Interação lógica para VUnidades.xam
    /// </summary>
    public partial class VUnidades : UserControl
    {
        private CUnidades Cadastro { get; set; }
        private UnidadesContainer Container { get; set; }
        public VUnidades(UnidadesContainer container)
        {
            InitializeComponent();

            Container = container;
            Cadastro = new CUnidades();
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void Pesquisar()
        {
            List<Unidades> unidades = UnidadesController.Search(txPesquisa.Text);
            dataGrid.ItemsSource = unidades;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.AplicarPadroes();
            Pesquisar();
        }

        private void btNovo_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.INSERIR))
                return;

            Cadastro = new CUnidades();
            Container.GridContainer.Children.Remove(this);
            Container.GridContainer.Children.Add(Cadastro);
            Cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void Cadastro_OnComplete()
        {
            Container.GridContainer.Children.Remove(Cadastro);
            Container.GridContainer.Children.Add(this);
            Pesquisar();
        }

        private void Alterar()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.ATUALIZAR))
                return;

            Unidades unidade = (Unidades)dataGrid.SelectedItem;

            if (unidade == null)
                return;
            if (unidade.Id == 0)
                return;

            Cadastro = new CUnidades();
            Cadastro.Load(unidade.Id);
            Container.GridContainer.Children.Remove(this);
            Container.GridContainer.Children.Add(Cadastro);
            Cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void btAlterar_OnClick()
        {
            Alterar();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Alterar();
        }

        private void btExcluir_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.EXCLUIR))
                return;

            Unidades unidade = (Unidades)dataGrid.SelectedItem;

            if (unidade == null)
                return;
            if (unidade.Id == 0)
                return;

            string msg = "Confirmar exclusão da unidade '" + unidade.Descricao + "'? Esta ação não poderá ser revertida!";
            if (new MsgSimNao(msg).Result)
                if (UnidadesController.Delete(unidade.Id))
                    Pesquisar();
        }
    }
}
