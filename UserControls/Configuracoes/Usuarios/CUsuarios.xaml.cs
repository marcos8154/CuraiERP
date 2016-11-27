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

            if (close)
                Close();
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
    }
}
