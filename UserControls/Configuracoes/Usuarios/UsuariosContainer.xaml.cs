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

namespace EM3.UserControls.Configuracoes.Usuarios
{
    /// <summary>
    /// Interação lógica para UsuariosContainer.xam
    /// </summary>
    public partial class UsuariosContainer : UserControl
    {
        public string Tela_id = "42";
        VUsuarios visualizacao;

        public UsuariosContainer()
        {
            InitializeComponent();

            visualizacao = new VUsuarios(this);
            GridContainer.Children.Add(visualizacao);
        }

    }
}
