using EM3.Controller;
using EM3.Extensions;
using EM3.UserControls.Configuracoes.CadastroUsuarios;
using EM3.UserControls.Configuracoes.GruposUsuarios;
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
    /// Lógica interna para SelecionarGrupoUsuarios.xaml
    /// </summary>
    public partial class SelecionarGrupoUsuarios : Window
    {
        public Grupos_usuarios Selecionado { get; set; }
        CGrupos_usuarios cadastro = new CGrupos_usuarios();

        public SelecionarGrupoUsuarios()
        {
            InitializeComponent();
        }

        private void Pesquisar()
        {
            List<Grupos_usuarios> grupos = Grupos_usuariosController.Search(txPesquisa.Text);
            dataGrid.ItemsSource = grupos;
        }

        private void btSelecionar_OnClick()
        {
            Selecionar();
        }

        private void Selecionar()
        {
            Grupos_usuarios grupo = (Grupos_usuarios)dataGrid.SelectedItem;
            if (grupo == null)
                return;
            if (grupo.Id == 0)
                return;

            Selecionado = grupo;
            Close();
        }

        private void btCancelar_OnClick()
        {
            Close();
        }

        private void btNovo_OnClick()
        {
            GridContainer.Children.Remove(GridListagem);
            GridContainer.Children.Add(cadastro);
            cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void Cadastro_OnComplete()
        {
            GridContainer.Children.Remove(cadastro);
            GridContainer.Children.Add(GridListagem);
            Pesquisar();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;
            this.Selecionado = new Grupos_usuarios();
            dataGrid.AplicarPadroes();
            Pesquisar();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Selecionar();
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }
    }
}
