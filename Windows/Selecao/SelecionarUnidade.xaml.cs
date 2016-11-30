using EM3.Controller;
using EM3.Extensions;
using EM3.UserControls.Estoque.UnidadesModulo;
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

namespace EM3.Windows.Selecao
{
    /// <summary>
    /// Interação lógica para SelecionarUnidade.xam
    /// </summary>
    public partial class SelecionarUnidade : Window
    {
        CUnidades Cadastro;
        public Unidades Selecionado { get; set; }
        public SelecionarUnidade()
        {
            InitializeComponent();

            this.Topmost = true;
            Selecionado = new Unidades();
            Cadastro = new CUnidades();
            Pesquisar();
            dataGrid.AplicarPadroes();
        }

        private void Pesquisar()
        {
            List<Unidades> unidades = UnidadesController.Search(txPesquisa.Text);
            dataGrid.ItemsSource = unidades;
        }

        private void btNovo_OnClick()
        {
            if (UsuariosController.ValidaPermissao("1", Enums.TipoPermissao.INSERIR))
                return;

            GridListagem.Visibility = Visibility.Hidden;
            GridContainer.Children.Add(Cadastro);

            Cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void Cadastro_OnComplete()
        {
            GridContainer.Children.Remove(Cadastro);
            GridListagem.Visibility = Visibility.Visible;
            Pesquisar();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Selecionar();
        }

        private void Selecionar()
        {
            Unidades unidade = (Unidades)dataGrid.SelectedItem;

            if (unidade == null)  return;
            if (unidade.Id == 0) return;

            Selecionado = unidade;
            Close();
        }

        private void btSelecionar_OnClick()
        {
            Selecionar();
        }

        private void btCancelar_OnClick()
        {
            Selecionado = new Unidades();
            Close();
        }

        private void txPesquisa_CallSearch()
        {
            Pesquisar();
        }
    }
}
