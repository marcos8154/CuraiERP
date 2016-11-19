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

namespace EM3.UserControls.Configuracoes.Empresas
{
    /// <summary>
    /// Interação lógica para Visualizacao.xam
    /// </summary>
    public partial class VEmpresas : UserControl
    {
        EmpresasContainer Container { get; set; }
        CEmpresas cadastro;
        public VEmpresas(EmpresasContainer parent)
        {
            InitializeComponent();

            this.Container = parent;
            dataGrid.AplicarPadroes();
        }

        private void btNovo_OnClick()
        {
            cadastro = new CEmpresas();
            
            Container.GridContainer.Children.Remove(this);
            Container.GridContainer.Children.Add(cadastro);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void Cadastro_OnComplete()
        {
            Container.GridContainer.Children.Remove(cadastro);
            Container.GridContainer.Children.Add(this);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Pesquisar("");
        }

        private void Pesquisar(string termo)
        {
            List<Empresa> empresas = EmpresasController.Search(termo);
            dataGrid.ItemsSource = empresas;
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Alterar();
        }

        private void Alterar()
        {
            Empresa emp = (Empresa)dataGrid.SelectedItem;
            if (emp == null)
                return;
            if (emp.Id == 0)
                return;

            cadastro = new CEmpresas();
            cadastro.Load(emp.Id);

            Container.GridContainer.Children.Remove(this);
            Container.GridContainer.Children.Add(cadastro);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar(txPesquisa.Text);
        }

        private void btAlterar_OnClick()
        {
            Alterar();
        }
    }
}
