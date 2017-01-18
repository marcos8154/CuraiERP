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
    /// Lógica interna para SelecionarNcm.xaml
    /// </summary>
    public partial class SelecionarNcm : Window
    {
        public Ncm Selecionado = new Ncm();

        public SelecionarNcm()
        {
            InitializeComponent();
            dataGrid.AplicarPadroes();
            Pesquisar();
            paginator.IntervalChangeNumber = 300;
            paginator.MaxPages = (NcmController.Count / 300);
        }

        private void btSelecionar_OnClick()
        {
            Selecionar();
        }

        private void Pesquisar()
        {
            List<Ncm> list = NcmController.Search(txPesquisa.Text, paginator.CurrentPage);
            dataGrid.ItemsSource = list;
        }

        private void Selecionar()
        {
            Ncm ncm = (Ncm)dataGrid.SelectedItem;
            if (ncm == null)
                return;
            if (ncm.Id == 0)
                return;

            Selecionado = ncm;
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

        private void txPesquisa_CallSearch()
        {
            paginator.SetPageNumber(0);
            Pesquisar();
        }

        private void paginator_OnPageChange(int page)
        {
            Pesquisar();
        }
    }
}
