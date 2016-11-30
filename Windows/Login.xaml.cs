using EM3.Controller;
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
    /// Lógica interna para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        SelecionarEmpresa se = new SelecionarEmpresa();
        public Login()
        {
            InitializeComponent();

            txUsuario.SetFocused();
            txData.Value = DateTime.Now;
            this.Closed += Login_Closed;

            Empresa e = EmpresasController.Find(Configuration.standard_company);
            txCod_empresa.Text = e.Id.ToString();
            txNomeEmpresa.Text = e.Razao_social;

            txUsuario.Text = "Admin";
        }

        private void Login_Closed(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btSair_click()
        {
            System.Environment.Exit(0);
        }

        private void btLogin_click()
        {
            Efetuar();
        }

        private void Efetuar()
        {
            /*
            if(txCod_empresa.Value == 0)
            {
                new MsgAlerta("Informe a empresa");
                return;
            }
            */

            Principal p = new Principal();
            if (UsuariosController.EfetuaLogin(txUsuario.Text, txSenha.Text, txData.Value, txCod_empresa.Value))
            {
                this.Hide();
                p.ShowDialog();
            }
        }

        private void txUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                txSenha.SetFocused();
        }

        private void buscarEmpresa()
        {
            se = new SelecionarEmpresa();
            se.ShowDialog();
            txCod_empresa.Value = se.Selecionado.Id;
            txNomeEmpresa.Text = se.Selecionado.Nome_fantasia;
        }

        private void txSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Efetuar();
        }
    }
}
