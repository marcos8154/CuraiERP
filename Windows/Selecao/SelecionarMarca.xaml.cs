using EM3.Controller;
using EM3.Extensions;
using EM3.Model;
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
    /// Lógica interna para SelecionarMarca.xaml
    /// </summary>
    public partial class SelecionarMarca : Window
    {
        public Marcas Selecionado = new Marcas();

        public SelecionarMarca()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.AplicarPadroes();
            Pesquisar();
        }

        private void Pesquisar()
        {
            List<Marcas> list = MarcasController.Search(txPesquisa.Text);
            dataGrid.ItemsSource = list;
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Selecionar();
        }

        private void Selecionar()
        {
            Marcas marca = (Marcas)dataGrid.SelectedItem;
            if (marca == null)
                return;
            if (marca.Id == 0)
                return;

            Selecionado = marca;
            Close();
        }

        private void btSelecionar_OnClick()
        {
            Selecionar();
        }

        private void btCancelar_OnClick()
        {
            Close();
        }
    }
}
