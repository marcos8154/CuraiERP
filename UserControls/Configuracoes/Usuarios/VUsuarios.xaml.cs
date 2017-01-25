using EM3.Controller;
using EM3.Extensions;
using EM3.Windows;
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
            if (Configuration.licence_mode == 0)
            {
                MsgAlerta.Show("Rotina desabilitada se o ambiente de licenças for 0 (homologação)");
                return;
            }

            if (!LicenceController.AuthorizeAdd())
            {
                new MsgAlerta(@"O limite de usuários ativos foi excedido no plano atualmente contratado. 
Para adicionar novos usuários, desative ao menos 1 (um) usuário, ou acione o setor comercial da Doware para 
solicitar uma expansão de limite.");
                return;
            }

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

        private void Alterar()
        {
            Usuarios usuario = (Usuarios)dataGrid.SelectedItem;

            if (usuario == null)
                return;
            if (usuario.Id == 0)
                return;

            Cadastro = new CUsuarios();
            Cadastro.Load(usuario.Id);
            Container.GridContainer.Children.Remove(this);
            Container.GridContainer.Children.Add(Cadastro);
            Cadastro.OnComplete += Cadastro_OnComplete;
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Alterar();
        }

        private void btAlterar_OnClick()
        {
            Alterar();
        }

        private void btExcluir_OnClick()
        {
            Usuarios usuario = (Usuarios)dataGrid.SelectedItem;

            if (usuario == null)
                return;
            if (usuario.Id == 0)
                return;
            
            if (!new MsgSimNao("Confirma exclusão do usuário '" + usuario.Nome + "'? Esta ação não pode ser revertida!").Result)
                return;

            if (UsuariosController.Delete(usuario.Id))
                Pesquisar();
        }
    }
}
