using EM3.Controller;
using EM3.Interfaces;
using EM3.UserControls.Configuracoes;
using EM3.UserControls.Configuracoes.CadastroUsuarios;
using EM3.UserControls.Configuracoes.GruposUsrXPermissoes;
using EM3.UserControls.Configuracoes.GruposUsuarios;
using EM3.UserControls.Estoque.Armazem;
using EM3.UserControls.Estoque.Caracteristica;
using EM3.UserControls.Estoque.UnidadesModulo;
using EM3.Util;
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
            progresso.Maximum = 10;
        }

        private void btn_OnClick()
        {
            progresso.Incresses(1);
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

        private void btGrupos_usuairos_Click(object sender, RoutedEventArgs e)
        {
            Grupos_usuariosContainer gc = new Grupos_usuariosContainer();
            if (UsuariosController.ValidaPermissao(gc.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, gc, "Grupos de usuários");
        }

        private void btGruposXPermissoes_Click(object sender, RoutedEventArgs e)
        {
            GruposUsuariosXPermissoes guxp = new GruposUsuariosXPermissoes();
            if (UsuariosController.ValidaPermissao(guxp.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, guxp, "Grupos de usuários X Permissões");
        }

        private void btEmpresa_Click(object sender, RoutedEventArgs e)
        {
            EmpresasContainer ec = new EmpresasContainer();
            if (UsuariosController.ValidaPermissao(ec.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, ec, "Empresas");
        }

        private void btUnidades_Click(object sender, RoutedEventArgs e)
        {
            UnidadesContainer uc = new UnidadesContainer();
            if (UsuariosController.ValidaPermissao(uc.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, uc, "Unidades");
        }

        private void btCaracteristicas_Click(object sender, RoutedEventArgs e)
        {
            CaracteristicasContainer cc = new CaracteristicasContainer();
            if (UsuariosController.ValidaPermissao(cc.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, cc, "Características");
        }

        private void btArmazens_Click(object sender, RoutedEventArgs e)
        {
            ArmazemContainer ac = new ArmazemContainer();
            if (UsuariosController.ValidaPermissao(ac.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, ac, "Gestão de armazéns");
        }
    }
}
