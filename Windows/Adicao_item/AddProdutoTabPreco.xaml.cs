using EM3.Controller;
using EM3.UserControls.Financeiro.Tabela_preco;
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

namespace EM3.Windows.Adicao_item
{
    /// <summary>
    /// Interação lógica para AddProdutoTabPreco.xam
    /// </summary>
    public partial class AddProdutoTabPreco : Window
    {
        public List<ItemTabela> Itens = new List<ItemTabela>();
        bool isEditMode = false;

        public AddProdutoTabPreco()
        {
            InitializeComponent();

            cbUF.AddItem("Todos");
            Commons.GetList_Ufs().ForEach(e => cbUF.AddItem(e));
        }

        public AddProdutoTabPreco(int tabela_preco_id = 0)
        {
            InitializeComponent();

            cbUF.AddItem("Todos");
            Commons.GetList_Ufs().ForEach(e => cbUF.AddItem(e));
            txTab_preco.Text = tabela_preco_id.ToString();
            if (tabela_preco_id > 0)
                txTab_preco.Enabled = false;
        }

        public void Load(ItemTabela item)
        {
            txCod.Text = item.Id.ToString();
            txProduto.Text = item.Produto_id.ToString();
            txDesc_prod.Text = item.Produto;
            txPreco_base.Text = item.Cod_Preco_base.ToString();
            if (item.Cod_Preco_base > 0)
                txValor_precoBase.Text = Produtos_precosController.Find(item.Cod_Preco_base).Valor.ToString();
            txValor.Text = item.Valor.ToString();
            txMargem.Text = item.Margem.ToString();
            cbUF.Text = item.Uf;
            txFaixa.Text = item.Faixa.ToString();
            cbPreco_padrao.SelectedIndex = (item.Preco_padrao ? 1 : 0);

            Title = "Alterar item da tabela de preço";
            Itens.Add(item);
            isEditMode = true;
        }

        private void btFechar_OnClick()
        {
            Close();
        }

        private void txProduto_CallSearch()
        {
            SelecionarProduto sp = new SelecionarProduto();
            sp.ShowDialog();

            txProduto.Text = sp.Selecionado.Id.ToString();
            txDesc_prod.Text = (sp.Selecionado.Id == 0
                ? "Não selecionado"
                : sp.Selecionado.Descricao);

            if (sp.Selecionado.Id > 0)
                txPreco_base.SetFocused();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            txProduto.SetFocused();
        }

        private void txPreco_base_CallSearch()
        {
            if (txProduto.GetInt == 0)
            {
                MsgAlerta.Show("Selecione o produto!");
                return;
            }

            SelecionarPrecoBase spb = new SelecionarPrecoBase();
            spb.ListarByProduto(txProduto.GetInt, txTab_preco.GetInt);
            spb.ShowDialog();

            txPreco_base.Text = spb.Selecionado.Id.ToString();
            txValor_precoBase.Text = (spb.Selecionado.Id == 0
                ? "0,00"
                : Produtos_precosController.Find(spb.Selecionado.Id).Valor.ToString());

            if (spb.Selecionado.Id == 0)
            {
                txValor.Text = "0,00";
                txMargem.Text = "0";
                txValor.SetFocused();
            }
            else
                txMargem.SetFocused();
        }

        private void txMargem_InputLostFocus(object sender, RoutedEventArgs e)
        {
            if (txPreco_base.GetInt == 0)
                return;

            decimal precoBase = Produtos_precosController.Find(txPreco_base.GetInt).Valor;
            decimal valorFinal = ((precoBase / 100 * txMargem.GetDecimal) + precoBase);
            txValor.Text = valorFinal.ToString();
        }

        private void btSalvarEContinuar_OnClick()
        {
            Salvar(false);
        }

        private void Salvar(bool close)
        {
            if(txProduto.GetInt == 0)
            {
                MsgAlerta.Show("Informe o produto");
                return;
            }

            if(txFaixa.GetDecimal == 0)
            {
                MsgAlerta.Show("Informe a faixa");
                return;
            }

            if(txValor.GetDecimal == 0)
            {
                MsgAlerta.Show("Informe o valor");
                return;
            }

            ItemTabela item = null;
            if (isEditMode)
                item = Itens.First();
            else
                item = new ItemTabela();

            item.Id = txCod.GetInt;
            item.Tabela_id = txTab_preco.GetInt;
            item.Produto_id = txProduto.GetInt;
            item.Produto = txDesc_prod.Text;
            item.Cod_Preco_base = txPreco_base.GetInt;
            if (txPreco_base.GetInt > 0)
                item.Preco_base = Produtos_precosController.Find(txPreco_base.GetInt).Valor;
            item.Margem = txMargem.GetDecimal;
            item.Valor = txValor.GetDecimal;
            item.Faixa = txFaixa.GetDecimal;
            item.Uf = (cbUF.SelectedIndex == 0 ? string.Empty : cbUF.Text);
            item.Preco_padrao = (cbPreco_padrao.SelectedIndex == 1);
            
            if (!isEditMode)
                Itens.Add(item);
            isEditMode = false;

            if (close)
                Fechar();
            else
                LimparCampos();
        }

        private void LimparCampos()
        {
            txCod.Text = "0";
            txTab_preco.Text = "0";
            txProduto.Text = "0";
            txDesc_prod.Text = string.Empty;
            txTab_preco.Text = "0";
            txPreco_base.Text = "0";
            txValor_precoBase.Text = "0,00";
            txMargem.Text = "0";
            txFaixa.Text = "9999";
            txValor.Text = "0";

            txProduto.SetFocused();
        }

        private void Fechar()
        {
            Close();
        }

        private void btSalvar_OnClick()
        {
            Salvar(true);
        }

        private void btCancelar_OnClick()
        {
            Fechar();
        }
    }
}