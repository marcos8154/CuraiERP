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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EM3.UserControls.Estoque.FornecedoresModulo
{
    /// <summary>
    /// Interação lógica para VFornecedores.xam
    /// </summary>
    public partial class VFornecedores : UserControl
    {
        public FornecedoresContainer Container { get; private set; }
        CFornecedores cadastro;

        public VFornecedores(FornecedoresContainer fc)
        {
            InitializeComponent();

            Container = fc;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Pesquisar();
            dataGrid.AplicarPadroes();
        }

        private void Pesquisar()
        {
            List<Fornecedores> fornecedores = FornecedoresController.Search(txPesquisa.Text);
            dataGrid.ItemsSource = fornecedores;
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void btNovo_OnClick()
        {
            cadastro = new CFornecedores();

            Container.GridContainer.Children.Remove(this);
            Container.GridContainer.Children.Add(cadastro);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void Cadastro_OnComplete()
        {
            Container.GridContainer.Children.Remove(cadastro);
            Container.GridContainer.Children.Add(this);
            dataGrid.Items.Refresh();
        }
    }
}
