using EM3.Controller;
using EM3.Interfaces;
using EM3.UserControls.Configuracoes;
using EM3.UserControls.Configuracoes.Usuarios;
using EM3.Windows.Selecao;
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

namespace EM3.Windows
{
    /// <summary>
    /// Lógica interna para Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        public Principal()
        {
            InitializeComponent();

            IModulo estoque = new UserControls.Modulos.Estoque();
            estoque.Inject(this);
        }

        private void btn_OnClick()
        {
            EmpresasContainer emps = new EmpresasContainer();

            TabItem item = new TabItem();
            item.Header = "Empresas";
            item.Content = emps;

            emps.Height = item.Height;
            emps.Width = item.Width;
            tabControl.Items.Add(item);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TabItem item = (TabItem)tabControl.SelectedItem;
            if (item.Header.Equals("Início"))
                return;

            tabControl.Items.Remove(tabControl.SelectedItem);
        }

        private void NormalButton_OnClick()
        {
            new SelecionarUnidade().ShowDialog();
        }

        private void btUsuarios_Click(object sender, RoutedEventArgs e)
        {
            UsuariosContainer uc = new UsuariosContainer();
            if (UsuariosController.ValidaPermissao(uc.Tela_id, Enums.TipoPermissao.ACESSO))
                Util.Navigation.AddTabItem(tabControl, uc, "Usuários");
        }
    }
}
