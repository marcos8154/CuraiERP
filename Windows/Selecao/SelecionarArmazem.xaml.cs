using EM3.Controller;
using EM3.Extensions;
using EM3.UserControls.Estoque.Armazem;
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
    /// Lógica interna para SelecionarArmazem.xaml
    /// </summary>
    public partial class SelecionarArmazem : Window
    {
        CArmazens cadastro = new CArmazens();
        public Armazens Selecionado = new Armazens();

        public SelecionarArmazem()
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
            List<Armazens> list = ArmazensController.Search(UsuariosController.Empresa, txPesquisa.Text);
            dataGrid.ItemsSource = list;
        }

        private void btNovo_OnClick()
        {
            if (!UsuariosController.ValidaPermissao("3", Enums.TipoPermissao.INSERIR))
                return;

            GridContainer.Children.Add(cadastro);
            GridContainer.Children.Remove(GridListagem);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void Cadastro_OnComplete()
        {
            GridContainer.Children.Remove(cadastro);
            GridContainer.Children.Add(GridListagem);
            Pesquisar();
            cadastro = new CArmazens();
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
            Armazens armz = (Armazens)dataGrid.SelectedItem;

            if (armz == null)
                Selecionado = new Armazens();
            if (armz.Id == 0)
                Selecionado = new Armazens();

            Selecionado = armz;
            Close();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Selecionar();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Pesquisar();
        }
    }
}
