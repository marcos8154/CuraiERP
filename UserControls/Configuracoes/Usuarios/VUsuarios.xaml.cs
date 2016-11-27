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

namespace EM3.UserControls.Configuracoes.CadastroUsuarios
{
    /// <summary>
    /// Interação lógica para VUsuarios.xam
    /// </summary>
    public partial class VUsuarios : UserControl
    {
        UsuariosContainer Container;
        CUsuarios Cadastro;

        public VUsuarios(UsuariosContainer container)
        {
            InitializeComponent();

            this.Container = container;
            dataGrid.AplicarPadroes();
        }
        
        private void Pesquisar()
        {
            List<Usuarios> usuarios = UsuariosController.Search(txPesquisa.Text);
            dataGrid.ItemsSource = usuarios;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Cadastro = new CUsuarios();
            Pesquisar();
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }

        private void btNovo_OnClick()
        {
            Cadastro = new CUsuarios();

            Container.GridContainer.Children.Remove(this);
            Container.GridContainer.Children.Add(Cadastro);
            Cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void Cadastro_OnComplete()
        {
            Container.GridContainer.Children.Remove(Cadastro);
            Container.GridContainer.Children.Add(this);
        }
    }
}
