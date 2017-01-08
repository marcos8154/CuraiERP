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

namespace EM3.UserControls.Financeiro.Operadora_cartao
{
    /// <summary>
    /// Interação lógica para COperadora_cartao.xam
    /// </summary>
    public partial class COperadora_cartao : UserControl
    {
        public delegate void Complete();
        public event Complete OnComplete;

        Operadoras_cartao operadora;

        public COperadora_cartao()
        {
            InitializeComponent();
            
        }

        public void Load(int id)
        {
            operadora = Operadoras_cartaoController.Find(id);
            txCod.Text = operadora.Id.ToString();
            txNome.Text = operadora.Nome;
            cbTipo.SelectedIndex = operadora.Tipo;
            txPrazo_receb.Text = operadora.Prazo_recebimento.ToString();
            cbTipo_receb.SelectedIndex = operadora.Tipo_recebimento;
            txTaxa.Text = operadora.Taxa.ToString();
            cbInativo.SelectedIndex = (operadora.Inativo ? 1 : 0);

            cabecalho.Title = $"Alterar operadora de cartão ({operadora.Nome})";
        }

        private void btSalvar_OnClick()
        {
            Save(true);
        }

        private void btSalvarEContinuar_OnClick()
        {
            Save(false);
        }

        private void btCancelar_OnClick()
        {
            Close();
        }

        private void Save(bool close)
        {
            operadora = new Operadoras_cartao();

            operadora.Id = txCod.GetInt;
            operadora.Nome = txNome.Text;
            operadora.Tipo = cbTipo.SelectedIndex;
            operadora.Prazo_recebimento = txPrazo_receb.GetInt;
            operadora.Tipo_recebimento = cbTipo_receb.SelectedIndex;
            operadora.Taxa = txTaxa.GetDecimal;
            operadora.Inativo = (cbInativo.SelectedIndex == 1);

            if (Operadoras_cartaoController.Save(operadora))
            {
                if (close)
                    Close();
                else
                    LimparCampos();
            }
        }

        private void Close()
        {
            if (OnComplete != null) OnComplete();
        }

        private void LimparCampos()
        {
            txCod.Text = "0";
            txNome.Text = string.Empty;
            txPrazo_receb.Text = "0";
            txTaxa.Text = "0";
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txNome.SetFocused();
        }
    }
}
