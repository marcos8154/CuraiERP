﻿using System;
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

namespace EM3.UserControls.Estoquev.Marca
{
    /// <summary>
    /// Interação lógica para MarcasContainer.xam
    /// </summary>
    public partial class MarcasContainer : UserControl
    {
        public string Tela_id = "45";
        public MarcasContainer()
        {
            InitializeComponent();
            GridContainer.Children.Add(new VMarcas(this));
        }
    }
}
