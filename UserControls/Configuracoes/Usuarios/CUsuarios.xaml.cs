using EM3.Controller;
using EM3.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EM3.UserControls.Configuracoes.CadastroUsuarios
{
    /// <summary>
    /// Interação lógica para CUsuarios.xam
    /// </summary>
    public partial class CUsuarios : UserControl
    {
        public delegate void Complete();
        public event Complete OnComplete;

        Usuarios Usuario = new Usuarios();
        bool isUpdateMode = false;

        public CUsuarios()
        {
            InitializeComponent();
        }

        private void btSalvar_OnClick()
        {
            Salvar(true);
        }

        private void Salvar(bool close)
        {
            if (Usuario == null) Usuario = new Usuarios();
            Usuario.Id = int.Parse(txCodigo.Text);
            Usuario.Nome = txNome.Text;
            Usuario.Senha = txSenha.Password;
            Usuario.Admin = (cbAdmin.SelectedIndex == 0);
            Usuario.Ativo = (cbAtivo.SelectedIndex == 0);
            Usuario.Grupo_usuarios_id = txCod_grupo.Value;

            Usuarios result = UsuariosController.Save(Usuario);

            if (result != null)
            {
                if (close)
                    Close();
                else
                    LimparCampos();

                if (isUpdateMode)
                    LicenceController.UpdateUser(result);
                else
                    LicenceController.RegisterUser(result);
                isUpdateMode = false;
            }
        }

        private void LimparCampos()
        {
            txCodigo.Text = "0";
            txNome.Text = string.Empty;
            txSenha.Password = string.Empty;
        }

        private void Close()
        {
            if (OnComplete != null) OnComplete();
        }

        private void btSalvarEContinuar_OnClick()
        {
            Salvar(false);
        }

        private void btCancelar_OnClick()
        {
            Close();
        }

        private void txCod_grupo_CallSearch()
        {
            SelecionarGrupoUsuarios sgu = new SelecionarGrupoUsuarios();
            sgu.ShowDialog();

            txCod_grupo.Value = sgu.Selecionado.Id;
        }

        public void Load(int id)
        {
            Usuario = UsuariosController.Find(id);

            txCodigo.Text = Usuario.Id.ToString();
            txNome.Text = Usuario.Nome;
            txSenha.Password = Usuario.Senha;
            cbAdmin.SelectedIndex = (Usuario.Admin ? 0 : 1);
            cbAtivo.SelectedIndex = (Usuario.Ativo ? 0 : 1);
            txCod_grupo.Value = Usuario.Grupo_usuarios_id;

            isUpdateMode = true;

        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
  
        }
    }
}
