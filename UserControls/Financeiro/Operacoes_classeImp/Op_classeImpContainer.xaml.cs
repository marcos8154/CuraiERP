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

namespace EM3.UserControls.Financeiro.Operacoes_classeImp
{
    /// <summary>
    /// Interação lógica para Op_classeImpContainer.xam
    /// </summary>
    public partial class Op_classeImpContainer : UserControl
    {
        public delegate void Back();
        public event Back OnBack;

        public string Tela_id;

        public Op_classeImpContainer(int classe_imposto_id, string tela_id)
        {
            InitializeComponent();

            this.Tela_id = tela_id;
            VOp_classeImp vOp = new VOp_classeImp(classe_imposto_id, this);
            GridContainer.Children.Add(vOp);
            vOp.OnBack += VOp_OnBack;
        }

        private void VOp_OnBack()
        {
            if (OnBack != null) OnBack();
        }
    }
}
