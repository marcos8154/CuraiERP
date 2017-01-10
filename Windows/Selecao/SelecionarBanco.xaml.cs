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
    /// Lógica interna para SelecionarBanco.xaml
    /// </summary>
    public partial class SelecionarBanco : Window
    {
        public  Bancos Selecionado = new Bancos();

        public SelecionarBanco()
        {
            InitializeComponent();
        }

        private void btSelecionar_OnClick()
        {
            Selecionar();
        }

        private void Selecionar()
        {
            Bancos banco = (Bancos)dataGrid.SelectedItem;
            if (banco == null)
                return;
            if (banco.Id == 0)
                return;

            Selecionado = banco;
            Close();
        }

        private void Pesquisar()
        {
            List<Bancos> bancos = BancosController.Search(txPesquisa.Text);
            dataGrid.ItemsSource = bancos;
        }

        private void btCancelar_OnClick()
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Pesquisar();
            dataGrid.AplicarPadroes();
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Selecionar();
        }
    }
}
