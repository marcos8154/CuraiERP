using EM3.UserControls.Configuracoes.Empresas;
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

namespace EM3.UserControls.Configuracoes
{
    /// <summary>
    /// Interação lógica para Empresa.xam
    /// </summary>
    public partial class EmpresasContainer : UserControl
    {
        VEmpresas visualizacao;

        public EmpresasContainer()
        {
            InitializeComponent();

            visualizacao = new VEmpresas(this);
            GridContainer.Children.Add(visualizacao);
        }
    }
}
