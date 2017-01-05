using EM3.Controller;
using EM3.Extensions;
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
        }

        private void btNovo_OnClick()
        {
            if (!UsuariosController.ValidaPermissao(container.Tela_id, Enums.TipoPermissao.INSERIR))
                return;

            cadastro = new COp_classeImp();
            container.GridContainer.Children.Add(cadastro);
            container.GridContainer.Children.Remove(this);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void Cadastro_OnComplete()
        {
            container.GridContainer.Children.Add(this);
            container.GridContainer.Children.Remove(cadastro);
        }

        private void btAlterar_OnClick()
        {

        }

        private void btExcluir_OnClick()
        {

        }

        private void btVoltar_OnClick()
        {
            if (OnBack != null) OnBack();
        }
    }
}
