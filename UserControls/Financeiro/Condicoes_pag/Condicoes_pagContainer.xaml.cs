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

namespace EM3.UserControls.Financeiro.Condicoes_pag
{
    /// <summary>
    /// Interação lógica para Condicoes_pagContainer.xam
    /// </summary>
    public partial class Condicoes_pagContainer : UserControl
    {
        public string Tela_id = "21";
        public Condicoes_pagContainer()
        {
            InitializeComponent();
            GridContainer.Children.Add(new VCondicoes_pag(this));
        }
    }
}
