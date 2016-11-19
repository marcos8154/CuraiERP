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
using System.Windows.Shapes;

namespace EM3.Windows
{
    /// <summary>
    /// Lógica interna para MsgSimNao.xaml
    /// </summary>
    public partial class MsgSimNao : Window
    {
        public bool Result { get; set; }

        public MsgSimNao(string msg)
        {
            InitializeComponent();

            txMsg.Text = msg;
            ShowDialog();
        }

        private void btSIM_OnClick()
        {
            Result = true;
            Close();
        }
        
        private void btNAO_OnClick()
        {
            Result = false;
            Close();
        }
    }
}
