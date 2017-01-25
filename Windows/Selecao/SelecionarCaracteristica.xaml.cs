using EM3.Controller;
using EM3.Extensions;
using EM3.UserControls.Estoquev.Caracteristica;
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
    /// Lógica interna para SelecionarCaracteristica.xaml
    /// </summary>
    public partial class SelecionarCaracteristica : Window
    {
        public Caracteristicas Selecionado = new Caracteristicas();
        CCaracteristicas cadastro;
        public SelecionarCaracteristica()
        {
            InitializeComponent();
            dataGrid.AplicarPadroes();
        }

        public void Pesquisar()
        {
            List<Caracteristicas> list = CaracteristicasController.Search(txPesquisa.Text);
            dataGrid.ItemsSource = list;
        }

        private void btSelecionar_OnClick()
        {
            Selecionar();
        }

        private void Selecionar()
        {
            Caracteristicas c = (Caracteristicas)dataGrid.SelectedItem;
            if (c == null)
                return;
            if (c.Id == 0)
                return;

            Selecionado = c;
            Close();
        }

        private void btCancelar_OnClick()
        {
            Close();
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Pesquisar();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Selecionar();
        }

        private void btNovo_OnClick()
        {
            cadastro = new CCaracteristicas();
            GridContainer.Children.Remove(GridPesquisa);
            GridContainer.Children.Add(cadastro);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void Cadastro_OnComplete()
        {
            GridContainer.Children.Remove(cadastro);
            GridContainer.Children.Add(GridPesquisa);
            Pesquisar();
        }
    }
}
