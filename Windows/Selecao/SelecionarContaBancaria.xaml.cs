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
    /// Lógica interna para SelecionarContaBancaria.xaml
    /// </summary>
    public partial class SelecionarContaBancaria : Window
    {
        public Contas_bancarias Selecionado  = new Contas_bancarias(); 

        public SelecionarContaBancaria()
        {
            InitializeComponent();
        }

        private void btSelecionar_OnClick()
        {
            Selecionar();
        }

        private void Selecionar()
        {
            Contas_bancarias conta = (Contas_bancarias)dataGrid.SelectedItem;
            if (conta == null)
                return;
            if (conta.Id == 0)
                return;

            Selecionado = conta;
            Close();
        }

        private void btCancelar_OnClick()
        {
            Close();
        }

        private void Pesquisar()
        {
            List<Contas_bancarias> contas = Contas_bancariasController.Search(txPesquisa.Text, 1);
            dataGrid.ItemsSource = contas;
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Selecionar();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txPesquisa.SetFocused();
            Pesquisar();
            dataGrid.AplicarPadroes();
        }
    }
}
