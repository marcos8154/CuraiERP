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

namespace EM3.UserControls.Estoquev.UnidadesModulo
{
    /// <summary>
    /// Interação lógica para UnidadesContainer.xam
    /// </summary>
    public partial class UnidadesContainer : UserControl
    {
        public string Tela_id = "1";

        public UnidadesContainer()
        {
            InitializeComponent();
            GridContainer.Children.Add(new VUnidades(this));
        }
    }
}
