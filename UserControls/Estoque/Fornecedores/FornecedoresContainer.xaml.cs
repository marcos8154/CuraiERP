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

namespace EM3.UserControls.Estoquev.FornecedoresModulo
{
    /// <summary>
    /// Interação lógica para FornecedoresContainer.xam
    /// </summary>
    public partial class FornecedoresContainer : UserControl
    {
        public FornecedoresContainer()
        {
            InitializeComponent();
            GridContainer.Children.Add(new VFornecedores(this));
        }
    }
}
