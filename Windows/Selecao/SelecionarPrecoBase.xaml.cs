using EM3.Controller;
using EM3.Extensions;
using EM3.UserControls.Financeiro.Tabela_preco;
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
using System.Windows.Shapes;

namespace EM3.Windows.Selecao
{
    /// <summary>
    /// Lógica interna para SelecionarPrecoBase.xaml
    /// </summary>
    public partial class SelecionarPrecoBase : Window
    {
        public ItemTabela Selecionado = new ItemTabela();

        public SelecionarPrecoBase()
        {
            InitializeComponent();
            dataGrid.AplicarPadroes();
           
        }

        public SelecionarPrecoBase(int produto_id, int tabela_id)
        {
            InitializeComponent();
            dataGrid.AplicarPadroes();

            ListarByProdutoTabela(produto_id, tabela_id);
        }

        public void ListarByProduto(int produto_id, int tabela_ignorar)
        {
            List<Produtos_precos> list = Produtos_precosController.ListByProduto(produto_id, tabela_ignorar);
            List<ItemTabela> itens = new List<ItemTabela>();
            list.ForEach(e => itens.Add(new ItemTabela(e)));
            dataGrid.ItemsSource = itens;
        }

        private void ListarByProdutoTabela(int produto_id, int tabela_id)
        {
            List<Produtos_precos> list = Produtos_precosController.ListByProdutoTabela(produto_id, tabela_id);
            List<ItemTabela> itens = new List<ItemTabela>();
            list.ForEach(e => itens.Add(new ItemTabela(e)));
            dataGrid.ItemsSource = itens;
        }

        private void btSelecionar_OnClick()
        {
            Selecionar();
        }

        private void Selecionar()
        {
            ItemTabela item = (ItemTabela)dataGrid.SelectedItem;
            if (item == null)
                return;
            if (item.Id == 0)
                return;

            Selecionado = item;
            Close();
        }

        private void btCancelar_OnClick()
        {
            Close();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Selecionar();
        }
    }
}
