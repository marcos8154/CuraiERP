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

namespace EM3.UserControls.Financeiro.Condicoes_pag
{
    /// <summary>
    /// Interação lógica para VCondicoes_pag.xam
    /// </summary>
    public partial class VCondicoes_pag : UserControl
    {
        private Condicoes_pagContainer Container;
        private CCondicao_pag cadastro;

        public VCondicoes_pag(Condicoes_pagContainer container)
        {
            InitializeComponent();

            Container = container;
        }

        public void Pesquisar()
        {
            int filtro_inativo = 0;
            if (cbExibir.SelectedIndex == 0)
                filtro_inativo = 2;
            if (cbExibir.SelectedIndex == 1)
                filtro_inativo = 1;
            if (cbExibir.SelectedIndex == 2)
                filtro_inativo = 0;

            List<Formas_pagamento> list = Formas_pagamentoController.Search(txPesquisa.Text, filtro_inativo);
            dataGrid.ItemsSource = list;
        }

        private void btNovo_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.INSERIR))
                return;

            cadastro = new CCondicao_pag();
            Container.GridContainer.Children.Add(cadastro);
            Container.GridContainer.Children.Remove(this);
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

        private void Alterar()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.ATUALIZAR))
                return;

            Formas_pagamento forma = (Formas_pagamento)dataGrid.SelectedItem;
            if (forma == null)
                return;
            if (forma.Id == 0)
                return;

            cadastro = new CCondicao_pag();
            cadastro.Load(forma.Id);
            Container.GridContainer.Children.Remove(this);
            Container.GridContainer.Children.Add(cadastro);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void btExcluir_OnClick()
        {
            Excluir();
        }

        private void Excluir()
        {
            if (!UsuariosController.ValidaPermissao(Container.Tela_id, Enums.TipoPermissao.EXCLUIR))
                return;

            Formas_pagamento forma = (Formas_pagamento)dataGrid.SelectedItem;
            if (forma == null)
                return;
            if (forma.Id == 0)
                return;

            if (!(new MsgSimNao($"Confirmar exclusão da condição de pagamento {forma.Descricao} ? Esta ação não poderá ser revertida").Result))
                return;

            if (Formas_pagamentoController.Remove(forma.Id))
                Pesquisar();
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
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
