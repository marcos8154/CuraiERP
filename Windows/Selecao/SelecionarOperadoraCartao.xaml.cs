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
using System.Windows.Shapes;

namespace EM3.Windows.Selecao
{
    /// <summary>
    /// Lógica interna para SelecionarOperadoraCartao.xaml
    /// </summary>
    public partial class SelecionarOperadoraCartao : Window
    {
        public Operadoras_cartao Selecionado = new Operadoras_cartao();

        public SelecionarOperadoraCartao()
        {
            InitializeComponent();

            dataGrid.AplicarPadroes();
            Pesquisar();
        }

        private void Pesquisar()
        {
            List<Operadoras_cartao> result = Operadoras_cartaoController.Search(txPesquisa.Text);
            dataGrid.ItemsSource = result;
        }

        private void btConfirmar_OnClick()
        {
            Selecionar();
        }

        private void Selecionar()
        {
            Operadoras_cartao operadora = (Operadoras_cartao)dataGrid.SelectedItem;
            if (operadora == null)
                return;
            if (operadora.Id == 0)
                return;

            Selecionado = operadora;
            Close();
        }

        private void btCancelar_OnClick()
        {
            Selecionado = new Operadoras_cartao();
            Close();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Selecionar();    
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }
    }
}
