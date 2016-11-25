using EM3.Extensions;
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Cadastro = new CUsuarios();
        }
    }
}
