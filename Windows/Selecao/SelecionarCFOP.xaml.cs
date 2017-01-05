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
    /// Lógica interna para SelecionarCFOP.xaml
    /// </summary>
    public partial class SelecionarCFOP : Window
    {
        public Cfop Selecionado = new Cfop();
        public SelecionarCFOP()
        {
            InitializeComponent();
            dataGrid.AplicarPadroes();
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void btCancelar_OnClick()
        {
            if (Selecionado == null) Selecionado = new Cfop();
            Close();
        }

        private void btSelecionar_OnClick()
        {
            Selecionar();
        }

        private void Selecionar()
        {
            Cfop cfop = (Cfop)dataGrid.SelectedItem;

            if (cfop == null) return;
            if (string.IsNullOrEmpty(cfop.Id)) return;

            Selecionado = cfop;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Pesquisar();
        }

        private void Pesquisar()
        {
            List<Cfop> list = CfopController.Search(txPesquisa.Text);
            dataGrid.ItemsSource = list;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Selecionado == null) Selecionado = new Cfop();
            if (Selecionado.Id == null) Selecionado.Id = "0";
            if (Selecionado.Descricao == null) Selecionado.Descricao = "Não selecionado";
            if (Selecionado.Aplicacao == null) Selecionado.Aplicacao = string.Empty;
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Selecionar();
        }
    }
}
