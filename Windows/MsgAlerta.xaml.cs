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
    /// Lógica interna para MsgAlerta.xaml
    /// </summary>
    public partial class MsgAlerta : Window
    {
        public MsgAlerta(string message)
        {
            InitializeComponent();

            this.txMsg.Text = message;
            this.Topmost = true;
            this.ShowDialog();
        }
        
        private void btOK_OnClick()
        {
            Close();
        }
    }
}
