using EM3.Extensions;
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

namespace EM3.UserControls.Financeiro.TiposMovimento
{
    /// <summary>
    /// Interação lógica para VTmv.xam
    /// </summary>
    public partial class VTmv : UserControl
    {
        TmvContainer container;
        public VTmv(TmvContainer container)
        {
            InitializeComponent();

            this.container = container;
            dataGrid.AplicarPadroes();
        }

        private void txPesquisa_CallSearch()
        {

        }

        private void btNovo_OnClick()
        {

        }

        private void btExcluir_OnClick()
        {

        }

        private void btAlterar_OnClick()
        {

        }
    }
}
