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

namespace EM3.UserControls.Estoque.Armazem
{
    /// <summary>
    /// Interação lógica para ArmazemContainer.xam
    /// </summary>
    public partial class ArmazemContainer : UserControl
    {
        public string Tela_id = "3";
        public ArmazemContainer()
        {
            InitializeComponent();
            GridContainer.Children.Add(new VArmazens(this));
        }
    }
}
