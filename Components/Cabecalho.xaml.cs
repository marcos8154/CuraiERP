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

namespace EM3.Components
{
    /// <summary>
    /// Interação lógica para Cabecalho.xam
    /// </summary>
    public partial class Cabecalho : UserControl
    {
        public string Title
        {
            get
            {
                return lbTitulo.Content.ToString();
            }
            set
            {
                lbTitulo.Content = value;
            }
        }

        public Cabecalho()
        {
            InitializeComponent();
        }
    }
}
