using EM3.Controller;
using EM3.Windows.Selecao;
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

namespace EM3.UserControls.Financeiro.Conta_bancarias
{
    /// <summary>
    /// Interação lógica para CContas_bancarias.xam
    /// </summary>
    public partial class CContas_bancarias : UserControl
    {
        public delegate void Complete();
        public event Complete OnComplete;

        Contas_bancarias Conta;

        public CContas_bancarias()
        {
            InitializeComponent();
        }

        public void Load(int id)
        {
            Conta = Contas_bancariasController.Find(id);
            txCod.Text = Conta.Id.ToString();
            txNome.Text = Conta.Nome;
            txCod_banco.Text = Conta.Bancos.Id.ToString();
            txNome_banco.Text = Conta.Bancos.Nome;
            txConta.Text = Conta.Conta;
            txDv_conta.Text = Conta.Dv_conta;
            txAgencia.Text = Conta.Agencia;
            txDv_agencia.Text = Conta.Dv_agencia;
            txCarteira.Text = Conta.Carteira;
            txCorrentista.Text = Conta.Correntista;
            txSaldo_atual.Text = Conta.Saldo_atual.ToString();
            txLimiteCredito.Text = Conta.Limite_credito.ToString();
            cbInativo.SelectedIndex = (Conta.Inativo ? 1 : 0);

            cabecalho.Title = $"Alterar conta bancária {Conta.Nome}";
        }

        private void btSalvar_OnClick()
        {
            Salvar(true);
        }

        private void btSalvarEContinuar_OnClick()
        {
            Salvar(false);
        }

        private void btCancelar_OnClick()
        {
            Fechar();
        }

        private void Salvar(bool close)
        {
            Conta = new Contas_bancarias();
            Conta.Id = txCod.GetInt;
            Conta.Nome = txNome.Text;
            Conta.Banco_id = txCod_banco.GetInt;
            Conta.Agencia = txAgencia.Text;
            Conta.Dv_agencia = txDv_agencia.Text;
            Conta.Conta = txConta.Text;
            Conta.Dv_conta = txDv_conta.Text;
            Conta.Carteira = txCarteira.Text;
            Conta.Correntista = txCorrentista.Text;
            Conta.Saldo_atual = txSaldo_atual.GetDecimal;
            Conta.Limite_credito = txLimiteCredito.GetDecimal;
            Conta.Inativo = (cbInativo.SelectedIndex == 1);

            if (Contas_bancariasController.Save(Conta))
            {
                if (close)
                    Fechar();
                else
                    LimparCampos();
            }
        }

        private void Fechar()
        {
            if (OnComplete != null) OnComplete();
        }

        private void LimparCampos()
        {
            txCod.Text = "0";
            txNome.Text = string.Empty;
            txCod_banco.Text = "0";
            txNome.Text = string.Empty;
            txAgencia.Text = string.Empty;
            txDv_agencia.Text = string.Empty;
            txConta.Text = string.Empty;
            txDv_conta.Text = string.Empty;
            txCarteira.Text = string.Empty;
            txCorrentista.Text = string.Empty;
            txSaldo_atual.Text = "0";
            txLimiteCredito.Text = "0";

            Conta = new Contas_bancarias();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txNome.SetFocused();
        }

        private void txCod_banco_CallSearch()
        {
            SelecionarBanco sb = new SelecionarBanco();
            sb.ShowDialog();

            txCod_banco.Text = sb.Selecionado.Id.ToString();
            txNome_banco.Text = (sb.Selecionado.Id == 0 ? "Não selecionado" : sb.Selecionado.Nome);
        }
    }
}
