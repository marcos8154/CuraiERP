using EM3.Controller;
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

namespace EM3.UserControls.Configuracoes.GruposUsuarios
{
    /// <summary>
    /// Interação lógica para CGrupos_usuarios.xam
    /// </summary>
    public partial class CGrupos_usuarios : UserControl
    {
        public delegate void Complete();
        public event Complete OnComplete;

        private Grupos_usuarios Grupo = new Grupos_usuarios();

        public CGrupos_usuarios()
        {
            InitializeComponent();
        }

        public void Load(int id)
        {
            Grupo = Grupos_usuariosController.Find(id);

            txCodigo.Text = Grupo.Id.ToString();
            txNome.Text = Grupo.Nome;

            cabecalho.Title = "Alterar grupo de usuários (" + Grupo.Nome + ")";
        }

        private void btSalvar_OnClick()
        {
            Salvar(true);
        }

        private void Salvar(bool close)
        {
            Grupo.Id = int.Parse(txCodigo.Text);
            Grupo.Nome = txNome.Text;

            if (Grupos_usuariosController.Save(Grupo))
            {
                if (close)
                    Close();
            }
            LimparCampos();
        }

        private void Close()
        {
            if (OnComplete != null) OnComplete();
        }

        private void btSalvarEContinuar_OnClick()
        {
            Salvar(false);
        }

        private void LimparCampos()
        {
            txCodigo.Text = "0";
            txNome.Text = string.Empty;
        }

        private void btCancelar_OnClick()
        {
            Close();
        }
    }
}
