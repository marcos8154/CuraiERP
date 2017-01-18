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
    /// Lógica interna para SelecionarClasseImposto.xaml
    /// </summary>
    public partial class SelecionarClasseImposto : Window
    {
        public Classes_imposto Selecionado = new Classes_imposto();

        public SelecionarClasseImposto()
        {
            InitializeComponent();
            dataGrid.AplicarPadroes();
            Pesquisar();
        }

        private void Pesquisar()
        {
            List<Classes_imposto> list = Classes_impostoController.Search(txPesquisa.Text);
            dataGrid.ItemsSource = list;
        }

        private void btCancelar_OnClick()
        {
            Close();
        }

        private void btSelecionar_OnClick()
        {
            Selecionar();
        }

        private void Selecionar()
        {
            Classes_imposto classe = (Classes_imposto)dataGrid.SelectedItem;
            if (classe == null)
                return;
            if (classe.Id == 0)
                return;

            Selecionado = classe;
            Close();
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
