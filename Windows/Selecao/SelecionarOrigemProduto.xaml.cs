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
using System.Windows.Shapes;

namespace EM3.Windows.Selecao
{
    /// <summary>
    /// Lógica interna para SelecionarOrigemProduto.xaml
    /// </summary>
    public partial class SelecionarOrigemProduto : Window
    {
        public Origem Selecionado = new Origem();

        public SelecionarOrigemProduto()
        {
            InitializeComponent();
            dataGrid.ItemsSource = GetList();
            dataGrid.AplicarPadroes();
        }

        private List<Origem> GetList()
        {
            List<Origem> result = new List<Origem>();

            result.Add(new Origem(0, "Nacional"));
            result.Add(new Origem(1, "Estr. (Importação Direta)"));
            result.Add(new Origem(2, "Estr. (Adquirida Merc. Interno"));
            result.Add(new Origem(3, "Nacional-Merc/bem com Cont de Import superior a 40%"));
            result.Add(new Origem(4, "Nacional, prod em conf com os proc produtivos básicos"));
            result.Add(new Origem(5, "Nacional-Merc/bem com Cont de Import inf ou igual a 40%"));
            result.Add(new Origem(6, "Estr-Import dir, sem similar nac, consta na lista CAMEX"));
            result.Add(new Origem(7, "Estr-Adq intern, sem similar nac, consta na lista CAMEX"));

            return result;
        }

        public static string GetDescricao(int cod_origem)
        {
            switch(cod_origem)
            {
                case 0: return "Nacional";
                case 1: return "Estr. (Importação Direta)";
                case 2: return "Estr. (Adquirida Merc. Interno";
                case 3: return "Nacional-Merc/bem com Cont de Import superior a 40%";
                case 4: return "Nacional, prod em conf com os proc produtivos básicos";
                case 5: return "Nacional-Merc/bem com Cont de Import inf ou igual a 40%";
                case 6: return "Estr-Import dir, sem similar nac, consta na lista CAMEX";
                case 7: return "Estr-Adq intern, sem similar nac, consta na lista CAMEX";
            }

            return "Nacional";
        }

        private void btSelecionar_OnClick()
        {
            Selecionar();
        }

        private void Selecionar()
        {
            Origem o = (Origem)dataGrid.SelectedItem;
            if (o == null)
                return;

            Selecionado = o;
            Close();
        }

        private void btCancelar_OnClick()
        {
            Selecionado = new Origem(0, "Nacional");
            Close();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Selecionar();
        }
    }

    public class Origem
    {
        public Origem()
        {

        }

        public Origem(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
    }
}
