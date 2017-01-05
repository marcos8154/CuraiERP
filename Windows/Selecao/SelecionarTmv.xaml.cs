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
    /// Lógica interna para SelecionarTmv.xaml
    /// </summary>
    public partial class SelecionarTmv : Window
    {
        public Tipos_movimento Selecionado = new Tipos_movimento();

        public SelecionarTmv()
        {
            InitializeComponent();
            dataGrid.AplicarPadroes();
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void Pesquisar()
        {
            List<Tipos_movimento> list = Tipos_movimentoController.Search(txPesquisa.Text, 1);
            dataGrid.ItemsSource = list;
        }

        private void btSelecionar_OnClick()
        {
            Selecionar();
        }

        private void Selecionar()
        {
            Tipos_movimento tmv = (Tipos_movimento)dataGrid.SelectedItem;
            if (tmv == null) return;
            if (tmv.Id == 0) return;
            Selecionado = tmv;
            Close();
        }

        private void btCancelar_OnClick()
        {
            if (Selecionado == null) Selecionado = new Tipos_movimento();
            if (Selecionado.Descricao == null) Selecionado.Descricao = "Não selecionado";
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Selecionado == null) Selecionado = new Tipos_movimento();
            if (Selecionado.Descricao == null) Selecionado.Descricao = "Não selecionado";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Pesquisar();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Selecionar();   
        }
    }
}
