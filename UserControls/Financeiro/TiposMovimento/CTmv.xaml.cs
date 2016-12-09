using EM3.Controller;
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
    /// Interação lógica para CTmv.xam
    /// </summary>
    public partial class CTmv : UserControl
    {
        public delegate void Complete();
        public event Complete OnComplete;

        Tipos_movimento Tipo_movimento = new Tipos_movimento();

        public CTmv()
        {
            InitializeComponent();
        }

        private void btCancelar_OnClick()
        {
            Close();
        }

        private void btSalvarEContinuar_OnClick()
        {
            Salvar(false);
        }

        private void btSalvar_OnClick()
        {
            Salvar(true);
        }

        private void Close()
        {
            if (OnComplete != null) OnComplete();
        }

        private void LimparCampos()
        {
            txCod.Text = "0";
            txDescricao.Text = string.Empty;
        }

        private void Salvar(bool close)
        {
            if (Tipo_movimento == null) Tipo_movimento = new Tipos_movimento();
            
            Tipo_movimento.Id = int.Parse(txCod.Text.ToString());
            Tipo_movimento.Descricao = txDescricao.Text;
            Tipo_movimento.Movimentacao_estoque = cbMov_estoque.SelectedIndex;
            Tipo_movimento.Movimentacao_financeiro = cbMov_financeiro.SelectedIndex;
            Tipo_movimento.Gera_nfe = (cbGeraNfe.SelectedIndex == 0 ? false : true);
            Tipo_movimento.Gera_nfce = (cbGeraNFCe.SelectedIndex == 0 ? false : true);
            Tipo_movimento.Gera_comissao = (cbGeraComissao.SelectedIndex == 0 ? false : true);
            Tipo_movimento.Inativo = (cbInativo.SelectedIndex == 0 ? false : true);

            if (Tipos_movimentoController.Save(Tipo_movimento))
            {
                if (close)
                    Close();
                else
                    LimparCampos();
            }
        }

        internal void Load(int id)
        {
            Tipo_movimento = Tipos_movimentoController.Find(id);

            txCod.Text = Tipo_movimento.Id.ToString();
            txDescricao.Text = Tipo_movimento.Descricao;
            cbGeraNFCe.SelectedIndex = (Tipo_movimento.Gera_nfce ? 1 : 0);
            cbGeraNfe.SelectedIndex = (Tipo_movimento.Gera_nfe ? 1 : 0);
            cbInativo.SelectedIndex = (Tipo_movimento.Inativo ? 1 : 0);
            cbGeraComissao.SelectedIndex = (Tipo_movimento.Gera_comissao ? 1 : 0);
            cbMov_estoque.SelectedIndex = Tipo_movimento.Movimentacao_estoque;
            cbMov_financeiro.SelectedIndex = Tipo_movimento.Movimentacao_financeiro;

            cabecalho.Title = "Alterar tipo de movimento (" + Tipo_movimento.Descricao + ")";
        }
    }
}
