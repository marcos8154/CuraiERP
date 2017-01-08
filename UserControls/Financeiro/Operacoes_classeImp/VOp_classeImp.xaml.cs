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

namespace EM3.UserControls.Financeiro.Operacoes_classeImp
{
    /// <summary>
    /// Interação lógica para VOp_classeImp.xam
    /// </summary>
    public partial class VOp_classeImp : UserControl
    {
        public delegate void Back();
        public event Back OnBack;

        private int classe_imp_id;
        Op_classeImpContainer container;
        COp_classeImp cadastro;

        public VOp_classeImp(int classe_imposto_id, Op_classeImpContainer container)
        {
            InitializeComponent();

            dataGrid.AplicarPadroes();
            Classes_imposto classe = Classes_impostoController.Find(classe_imposto_id);
            cabecalho.Title += $"({classe.Nome})";
            this.classe_imp_id = classe_imposto_id;
            this.container = container;
            ListAll();
        }

        private void ListAll()
        {
            List<Operacoes_classe_imposto> list = Operacoes_classeImpostoController.ListAll(classe_imp_id);
            dataGrid.ItemsSource = list;
        }

        private void btNovo_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(container.Tela_id, Enums.TipoPermissao.INSERIR))
                return;

            cadastro = new COp_classeImp(classe_imp_id);
            container.GridContainer.Children.Add(cadastro);
            container.GridContainer.Children.Remove(this);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void Cadastro_OnComplete()
        {
            container.GridContainer.Children.Add(this);
            container.GridContainer.Children.Remove(cadastro);
            ListAll();
        }

        private void btAlterar_OnClick()
        {
            Alterar();
        }

        private void btExcluir_OnClick()
        {
            Excluir();
        }

        private void Excluir()
        {
            if (!UsuariosController.ValidaPermissao(container.Tela_id, Enums.TipoPermissao.EXCLUIR))
                return;

            Operacoes_classe_imposto operacao = (Operacoes_classe_imposto)dataGrid.SelectedItem;

            if (operacao == null)
                return;
            if (operacao.Id == 0)
                return;

            if (new MsgSimNao("Confirma exclusão da operação? Esta ação não poderá ser revertida").Result)
            {
                if (Operacoes_classeImpostoController.Remove(operacao.Id))
                    ListAll();
            }
        }

        private void btVoltar_OnClick()
        {
            if (OnBack != null) OnBack();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Alterar();
        }

        private void Alterar()
        {
            if (!UsuariosController.ValidaPermissao(container.Tela_id, Enums.TipoPermissao.ATUALIZAR))
                return;

            Operacoes_classe_imposto operacao = (Operacoes_classe_imposto)dataGrid.SelectedItem;

            if (operacao == null)
                return;
            if (operacao.Id == 0)
                return;

            cadastro = new COp_classeImp(classe_imp_id);
            cadastro.Load(operacao.Id);
            container.GridContainer.Children.Add(cadastro);
            container.GridContainer.Children.Remove(this);
            cadastro.OnComplete += Cadastro_OnComplete;
        }
    }
}
