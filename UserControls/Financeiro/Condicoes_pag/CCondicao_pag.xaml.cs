using EM3.Controller;
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
    /// Interação lógica para CCondicao_pag.xam
    /// </summary>
    public partial class CCondicao_pag : UserControl
    {
        public delegate void Complete();
        public event Complete OnComplete;

        Formas_pagamento forma_pg;
        private bool isEditMode = false;

        public CCondicao_pag()
        {
            InitializeComponent();
            ChangeStatus();
        }

        public void Load(int id)
        {
            isEditMode = true;
            forma_pg = Formas_pagamentoController.Find(id);

            txCod.Text = forma_pg.Id.ToString();
            cbCond_pag.SelectedIndex = forma_pg.Tipo_pagamento;
            txDescricao.Text = forma_pg.Descricao;
            cbPermite_entrada.SelectedIndex = (forma_pg.Permite_entrada ? 0 : 1);
            cbInativo.SelectedIndex = (forma_pg.Tipo_pagamento);


            if (forma_pg.Tipo_intervalo.Equals("I"))
            {
                cbTipo_intervalo.SelectedIndex = 0;
                txIntervalo.Text = forma_pg.Intervalo.ToString();
            }
            else
            {
                txIntervalo.Text = forma_pg.Dia_base.ToString();
                cbTipo_intervalo.SelectedIndex = 1;
            }
            if(forma_pg.Operadoras_cartao.Id != 0)
            {
                txCod_operadora.Text = forma_pg.Operadoras_cartao.Id.ToString();
                txDesc_operadora.Text = forma_pg.Operadoras_cartao.Nome;
            }

            if(forma_pg.Contas_bancarias.Id != 0)
            {
                txCod_conta.Text = forma_pg.Contas_bancarias.Id.ToString();
                txDesc_conta.Text = forma_pg.Contas_bancarias.Nome;
            }


            txTolerancia.Text = forma_pg.Tolerancia_dias.ToString();
            txJuros_atraso.Text = forma_pg.Juros_atraso.ToString();
            txParcelas.Text = forma_pg.Parcelas.ToString();

            cabecalho.Title = $"Alterar condição de pagamento ({forma_pg.Descricao})";
        }

        private void btCancelar_OnClick()
        {
            Fechar();
        }

        private void btSalvar_OnClick()
        {
            Salvar(true);
        }

        private void btSalvarEContinuar_OnClick()
        {
            Salvar(false);
        }

        private void Salvar(bool close)
        {
            if (forma_pg == null) forma_pg = new Formas_pagamento();

            forma_pg.Id = int.Parse(txCod.Text);
            forma_pg.Descricao = txDescricao.Text;
            forma_pg.Tipo_pagamento = cbCond_pag.SelectedIndex;
            forma_pg.Conta_bancaria_id = txCod_conta.GetInt;
            forma_pg.Operadora_cartao_id = txCod_operadora.GetInt;
            forma_pg.Permite_entrada = (cbPermite_entrada.SelectedIndex == 0);
            forma_pg.Tipo_intervalo = (cbTipo_intervalo.SelectedIndex == 0 ? "I" : "D");
            
            if(forma_pg.Tipo_intervalo.Equals("I"))
            {
                forma_pg.Dia_base = 0;
                forma_pg.Intervalo = txIntervalo.GetInt;
            }
            else
            {
                forma_pg.Dia_base = txIntervalo.GetInt;
                forma_pg.Intervalo = 0;
            }

            forma_pg.Tolerancia_dias = txTolerancia.GetInt;
            forma_pg.Juros_atraso = txJuros_atraso.GetDecimal;
            forma_pg.Parcelas = txParcelas.GetInt;
            forma_pg.Inativo = (cbInativo.SelectedIndex == 1);

            if (Formas_pagamentoController.Save(forma_pg))
            {
                if (close)
                    Fechar();
                else
                    LimparCampos();
            }
        }

        private void LimparCampos()
        {
            txCod.Text = "0";
            txDescricao.Text = string.Empty;
            txIntervalo.Text = "0";
            txCod_operadora.Text = "0";
            txDesc_operadora.Text = string.Empty;
            txCod_conta.Text = "0";
            txDesc_conta.Text = string.Empty;
            txTolerancia.Text = "0";
            txJuros_atraso.Text = "0";
            txParcelas.Text = "0";

            forma_pg = new Formas_pagamento();
            txDescricao.SetFocused();
        }

        private void Fechar()
        {
            if (OnComplete != null) OnComplete();
        }

        private void cbCond_pag_OnChange(object sender, SelectionChangedEventArgs e)
        {
            ChangeStatus();
        }

        private void ChangeStatus()
        {
            try
            {
                switch (cbCond_pag.SelectedIndex)
                {
                    case (int)Formas_pagamento.TIPO_PAGAMENTO.DINHEIRO:

                        cbTipo_intervalo.IsEnabled = false;
                        cbPermite_entrada.IsEnabled = false;
                        txCod_conta.Enabled = false;
                        txCod_operadora.Enabled = false;
                        txTolerancia.Enabled = false;
                        txJuros_atraso.Enabled = false;
                        txParcelas.Enabled = false;
                        txIntervalo.Enabled = false;
                        txParcelas.Text = "0";
                        cbPermite_entrada.SelectedIndex = 1;
                        break;

                    case (int)Formas_pagamento.TIPO_PAGAMENTO.CARTAO:

                        cbTipo_intervalo.IsEnabled = false;
                        cbPermite_entrada.IsEnabled = true;
                        txCod_conta.Enabled = false;
                        txCod_operadora.Enabled = true;
                        txTolerancia.Enabled = false;
                        txJuros_atraso.Enabled = false;
                        txParcelas.Text = "1";
                        txParcelas.Enabled = false;
                        txIntervalo.Enabled = false;
                        cbPermite_entrada.SelectedIndex = 0;
                        break;

                    case (int)Formas_pagamento.TIPO_PAGAMENTO.CHEQUE:

                        cbTipo_intervalo.IsEnabled = true;
                        cbPermite_entrada.IsEnabled = true;
                        txCod_conta.Enabled = true;
                        txCod_operadora.Enabled = false;
                        txTolerancia.Enabled = true;
                        txJuros_atraso.Enabled = true;
                        txParcelas.Text = "1";
                        txParcelas.Enabled = true;
                        cbPermite_entrada.SelectedIndex = 0;
                        txIntervalo.Enabled = true;
                        break;

                    case (int)Formas_pagamento.TIPO_PAGAMENTO.BOLETO:

                        cbTipo_intervalo.IsEnabled = true;
                        cbPermite_entrada.IsEnabled = true;
                        txCod_conta.Enabled = true;
                        txCod_operadora.Enabled = false;
                        txTolerancia.Enabled = true;
                        txJuros_atraso.Enabled = true;
                        txParcelas.Text = "1";
                        txParcelas.Enabled = true;
                        txIntervalo.Enabled = true;
                        cbPermite_entrada.SelectedIndex = 0;
                        break;

                    case (int)Formas_pagamento.TIPO_PAGAMENTO.CREDITO_CLIENTE:

                        cbTipo_intervalo.IsEnabled = true;
                        cbPermite_entrada.IsEnabled = true;
                        txCod_conta.Enabled = true;
                        txCod_operadora.Enabled = false;
                        txTolerancia.Enabled = true;
                        txJuros_atraso.Enabled = true;
                        txParcelas.Text = "1";
                        txParcelas.Enabled = true;
                        txIntervalo.Enabled = true;
                        cbPermite_entrada.SelectedIndex = 0;
                        break;
                }
            }
            catch { }
        }

        private void cbTipo_intervalo_OnChange(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbTipo_intervalo.SelectedIndex == 0)
                    txIntervalo.Title = "Intervalo (dias)";
                else
                    txIntervalo.Title = "Data base";
            }
            catch { }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txDescricao.SetFocused();
            if (isEditMode)
                return;

            cbTipo_intervalo.IsEnabled = false;
            cbPermite_entrada.IsEnabled = false;
            txCod_conta.Enabled = false;
            txCod_operadora.Enabled = false;
            txTolerancia.Enabled = false;
            txJuros_atraso.Enabled = false;
            txParcelas.Enabled = false;
            txIntervalo.Enabled = false;
            cbPermite_entrada.SelectedIndex = 1;
            txParcelas.Text = "0";
        }
    }
}
