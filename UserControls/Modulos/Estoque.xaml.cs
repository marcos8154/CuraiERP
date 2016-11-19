using EM3.Interfaces;
using EM3.UserControls.Estoque.FornecedoresModulo;
using EM3.UserControls.Estoque.UnidadesModulo;
using EM3.Util;
using EM3.Windows;
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

namespace EM3.UserControls.Modulos
{
    /// <summary>
    /// Interação lógica para Estoque.xam
    /// </summary>
    public partial class Estoque : UserControl, IModulo
    {
        private Principal principal { get; set; }

        public UserControl GetContentControl()
        {
            return this;
        }

        public Estoque()
        {
            InitializeComponent();
        }

        public void Inject(Principal principal)
        {
            principal.GridModulos.Children.Add(this);
            this.Width = principal.GridModulos.Width;
            this.Height = principal.GridModulos.Height;
            this.principal = principal;
        }

        private void btUnidades_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UnidadesContainer uc = new UnidadesContainer();
            Navigation.AddTabItem(principal.tabControl, uc, "Unidades");
        }

        private void btFornecedores_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FornecedoresContainer fc = new FornecedoresContainer();
            Navigation.AddTabItem(principal.tabControl, fc, "Fornecedores");
        }
    }
}
