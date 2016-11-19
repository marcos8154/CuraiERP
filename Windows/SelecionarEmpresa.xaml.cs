using EM3.Controller;
using EM3.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace EM3.Windows
{
    /// <summary>
    /// Lógica interna para SelecionarEmpresa.xaml
    /// </summary>
    public partial class SelecionarEmpresa : Window
    {
        public Empresa Selecionado { get; set; }
        public SelecionarEmpresa()
        {
            InitializeComponent();
            this.Loaded += SelecionarEmpresa_Loaded;
        }

        private void SelecionarEmpresa_Loaded(object sender, RoutedEventArgs e)
        {
            List<Empresa> empresas = EmpresasController.Search("");
            dataGrid.ItemsSource = empresas;
            dataGrid.AplicarPadroes();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Confirmar();
        }

        private void buscar()
        {
            List<Empresa> empresas = EmpresasController.Search(txPesquisa.Text);
            dataGrid.ItemsSource = empresas;
        }

        private void Confirmar()
        {
            try
            {
                Empresa emp = (Empresa)dataGrid.SelectedItem;
                Selecionado = emp;
            }
            catch (Exception ex)
            {
                Selecionado = new Empresa();
            }
            Fechar();
        }

        private void Fechar()
        {
            if (Selecionado == null)
                Selecionado = new Empresa();
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Fechar();
        }
    }
}
