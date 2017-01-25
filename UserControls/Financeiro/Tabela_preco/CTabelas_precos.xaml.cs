using EM3.Controller;
using EM3.Extensions;
using EM3.Windows;
using EM3.Windows.Adicao_item;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace EM3.UserControls.Financeiro.Tabela_preco
{
    /// <summary>
    /// Interação lógica para CTabelas_precos.xam
    /// </summary>
    public partial class CTabelas_precos : UserControl
    {
        public delegate void Complete();
        public event Complete OnComplete;

        private List<ItemTabela> ItensTabela = new List<ItemTabela>();
        AddProdutoTabPreco addProdutoTab = new AddProdutoTabPreco();

        public CTabelas_precos()
        {
            InitializeComponent();
            dataGrid.AplicarPadroes();
            dataGrid.ItemsSource = ItensTabela;
        }

        public void Load(int id)
        {
            Tabelas_precos tabela = Tabelas_precoController.Find(id);
            txCod.Text = tabela.Id.ToString();
            txNome.Text = tabela.Nome;
            txData_inicio.Value = (string.IsNullOrEmpty(tabela.Data_inicio)
                ? Commons.ServerDate
                : Convert.ToDateTime(tabela.Data_inicio));
            txData_inat.Value = (string.IsNullOrEmpty(tabela.Data_inativacao)
                ? new DateTime()
                : Convert.ToDateTime(tabela.Data_inativacao));
            cbInativo.SelectedIndex = (tabela.Inativo ? 1 : 0);
            txForma_pag.Text = tabela.Forma_pagamento_id.ToString();

            List<Produtos_precos> list = Produtos_precosController.ListByTabela(tabela.Id);
            list.ForEach(e => ItensTabela.Add(new ItemTabela(e)));
            dataGrid.ItemsSource = ItensTabela;

            cabecalho.Title = $"Alterar tabela de preço ({tabela.Nome})";
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
            Tabelas_precos tabela = new Tabelas_precos();
            tabela.Id = txCod.GetInt;
            tabela.Nome = txNome.Text;
            tabela.Forma_pagamento_id = txForma_pag.GetInt;
            tabela.Data_inicio = txData_inicio.Value.ToString("dd-MM-yyyy");
            tabela.Data_inativacao = txData_inat.Value.ToString("dd-MM-yyyy");

            int id = Tabelas_precoController.Save(tabela);
            if (id > 0)
            {
                foreach (ItemTabela item in ItensTabela)
                {
                    Produtos_precos pp = new Produtos_precos();
                    pp.Id = item.Id;
                    pp.Tabela_id = id;
                    pp.Produto_id = item.Produto_id;
                    pp.Preco_base = item.Cod_Preco_base;
                    pp.Margem = item.Margem;
                    pp.Valor = item.Valor;
                    pp.Faixa = item.Faixa;
                    pp.Uf = item.Uf;
                    pp.Preco_padrao = item.Preco_padrao;

                    Produtos_precosController.Save(pp);
                }

                if (close)
                    Fechar();
                else
                    LimparCampos();
            }
        }

        private void LimparCampos()
        {

        }

        private void Fechar()
        {
            if (OnComplete != null)
                OnComplete();
        }

        private void btAdicionaItem_OnClick()
        {
            addProdutoTab = new AddProdutoTabPreco(txCod.GetInt);
            addProdutoTab.ShowDialog();

            AtualizaLista();
        }

        private void AtualizaLista()
        {
            foreach (ItemTabela item in addProdutoTab.Itens)
            {
                ItemTabela existingItem = ItensTabela.FirstOrDefault(
                    e => e.Id == item.Id &&
                    e.Produto_id == item.Produto_id &&
                    e.Tabela_id == item.Tabela_id &&
                    e.Uf.Equals(item.Uf) &&
                    e.Valor == item.Valor &&
                    e.Cod_Preco_base == item.Cod_Preco_base &&
                    e.Margem == item.Margem);

                if (item == existingItem)
                {
                    existingItem = item;
                    dataGrid.Items.Refresh();
                    continue;
                }

                if (existingItem != null)
                {
                    existingItem.Id = item.Id;
                    existingItem.Produto_id = item.Produto_id;
                    existingItem.Produto = item.Produto;
                    existingItem.Cod_Preco_base = item.Cod_Preco_base;
                    existingItem.Preco_base = item.Preco_base;
                    existingItem.Margem = item.Margem;
                    existingItem.Valor = item.Valor;
                    existingItem.Faixa = item.Faixa;
                    existingItem.Uf = item.Uf;
                    existingItem.Preco_padrao = item.Preco_padrao;
                    continue;
                }

                ItensTabela.Add(item);
                dataGrid.Items.Refresh();
            }
        }

        private void btAlteraItem_OnClick()
        {
            AlterarItem();
        }

        private void AlterarItem()
        {
            ItemTabela item = (ItemTabela)dataGrid.SelectedItem;
            if (item == null)
                return;

            addProdutoTab = new AddProdutoTabPreco(txCod.GetInt);
            addProdutoTab.Load(item);

            addProdutoTab.ShowDialog();
            AtualizaLista();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AlterarItem();
        }

        private void btRemoveItem_OnClick()
        {
            ItemTabela item = (ItemTabela)dataGrid.SelectedItem;
            if (item == null)
                return;
            if (item.Id > 0)
                if (!Produtos_precosController.Remove(item.Id))
                    return;

            ItensTabela.Remove(item);
            dataGrid.Items.Refresh();
        }
    }

    public class ItemTabela
    {
        public ItemTabela()
        {

        }

        public ItemTabela(Produtos_precos pp)
        {
            Id = pp.Id;
            Produto = ProdutosController.Find(pp.Produto_id).Descricao;
            Tabela = Tabelas_precoController.Find(pp.Tabela_id).Nome;
            Produto_id = pp.Produto_id;
            Margem = pp.Margem;
            Valor = pp.Valor;
            Faixa = pp.Faixa;
            Uf = pp.Uf;
            Preco_padrao = pp.Preco_padrao;
            Cod_Preco_base = pp.Preco_base;
            if (Cod_Preco_base > 0)
                Preco_base = Produtos_precosController.Find(Cod_Preco_base).Valor;
        }

        public int Id { get; set; }
        public string Produto { get; set; }
        public int Produto_id { get; set; }
        public int Cod_Preco_base { get; set; }
        public decimal Preco_base { get; set; }
        public string Tabela { get; set; }
        public int Tabela_id { get; set; }
        public decimal Margem { get; set; }
        public decimal Valor { get; set; }
        public decimal Faixa { get; set; }
        public string Uf { get; set; }
        public bool Preco_padrao { get; set; }
    }
}
