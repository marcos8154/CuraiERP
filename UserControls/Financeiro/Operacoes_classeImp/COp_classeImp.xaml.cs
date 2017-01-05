using EM3.Controller;
using EM3.Windows.Selecao;
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
    /// Interação lógica para COp_classeImp.xam
    /// </summary>
    public partial class COp_classeImp : UserControl
    {
        Operacoes_classe_imposto Operacao = new Operacoes_classe_imposto();

        public delegate void Complete();
        public event Complete OnComplete;

        public COp_classeImp()
        {
            InitializeComponent();
        }

        private void btCancelar_OnClick()
        {
            Close();
        }

        private void btSalvar_OnClick()
        {
            Salvar(true);
        }

        private void btSalvarEContinuar_OnClick()
        {
            Salvar(false);
        }

        private void Salvar(bool close)
        {
            if (this.Operacao == null) this.Operacao = new Operacoes_classe_imposto();

            if (close)
                Close();
            else
                LimparCampos();
        }

        private void Close()
        {
            if (OnComplete != null) OnComplete();
        }

        private void LimparCampos()
        {

        }

        private void txCod_CFOP_CallSearch()
        {
            SelecionarCFOP sCfop = new SelecionarCFOP();
            sCfop.ShowDialog();
            txCod_CFOP.Text = sCfop.Selecionado.Id;
            txDescricao_cfop.Text = sCfop.Selecionado.Descricao;
        }

        private void txCod_tipo_mov_CallSearch()
        {
            SelecionarTmv sTmv = new SelecionarTmv();
            sTmv.ShowDialog();
            txCod_tipo_mov.Text = sTmv.Selecionado.Id.ToString();
            txDescricao_Tmv.Text = sTmv.Selecionado.Descricao.ToString();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Empresa emp = EmpresasController.Find(UsuariosController.Empresa_atual_id);
            if (emp.Optante_simples)
                LoadSimplesNacional();
            else
                LoadRegimeNormal();

            //tpc == Tipos de Pis e Cofins. Neste dia estava com preguiça de nomear variavel rsrs
            List<KeyValuePair<string, string>> tpc = new List<KeyValuePair<string, string>>();

            tpc.Add(new KeyValuePair<string, string>("01", "01 - Tributável com alíquota básica"));
            tpc.Add(new KeyValuePair<string, string>("02", "02 - Tributável com alíquota diferenciada"));
            tpc.Add(new KeyValuePair<string, string>("04", "04 - Tributável monofásica - Revenda alíquota zero"));
            tpc.Add(new KeyValuePair<string, string>("05", "05 - Tributável por substituição tributária"));
            tpc.Add(new KeyValuePair<string, string>("06", "06 - Tributável a alíquota zero"));
            tpc.Add(new KeyValuePair<string, string>("07", "07 - Isento"));
            tpc.Add(new KeyValuePair<string, string>("08", "08 - Sem incidência de contribuição"));
            tpc.Add(new KeyValuePair<string, string>("09", "09 - Suspensão da contribuição"));
            tpc.Add(new KeyValuePair<string, string>("49", "49 - Outras operações de saídas"));
            tpc.Add(new KeyValuePair<string, string>("99", "99 - Outras"));

            cbTipo_Pis.SetItemsSource(tpc);
        }

        private void LoadSimplesNacional()
        {
            cbCST.Title = "CSOSN";
            List<KeyValuePair<int, string>> cst = new List<KeyValuePair<int, string>>();

            cst.Add(new KeyValuePair<int, string>(101, "101 - Tributado pelo Simples Nacional com permissão de crédito"));
            cst.Add(new KeyValuePair<int, string>(102, "102 - Tributada pelo Simples Nacional sem permissão de crédito"));
            cst.Add(new KeyValuePair<int, string>(103, "103 - Isenção do ICMS no Simples Nacional para faixa de receita bruta"));
            cst.Add(new KeyValuePair<int, string>(201, "201 - Tributada pelo Simples Nacional com permissão de crédito e com cobrança do ICMS por substituição tributária"));
            cst.Add(new KeyValuePair<int, string>(202, "202 - Tributada pelo Simples Nacional sem permissão de crédito e com cobrança do ICMS por substituição tributária"));
            cst.Add(new KeyValuePair<int, string>(203, "203 - Isenção do ICMS no Simples Nacional para faixa de receita bruta e com cobrança de ICMS por substituição tributária"));
            cst.Add(new KeyValuePair<int, string>(300, "300 - Imune"));
            cst.Add(new KeyValuePair<int, string>(400, "400 - Não tributada pelo Simples Nacional"));
            cst.Add(new KeyValuePair<int, string>(500, "500 - ICMS cobrado anteriormente por substituição tributária (substituído) ou por antecipação"));
            cst.Add(new KeyValuePair<int, string>(900, "900 - Outros"));

            cbCST.SetItemsSource(cst);
        }

        private void LoadRegimeNormal()
        {
            cbCST.Title = "CST";
            List<KeyValuePair<int, string>> cst = new List<KeyValuePair<int, string>>();

            cst.Add(new KeyValuePair<int, string>(00, "00 - Tributado Integralmente"));
            cst.Add(new KeyValuePair<int, string>(10, "10 - Tributado com cobrança do ICMS opr Substituição Tributária"));
            cst.Add(new KeyValuePair<int, string>(20, "20 - Com redução de Base de Cálculo"));
            cst.Add(new KeyValuePair<int, string>(30, "30 - Isenta ou não tributada com ICMS por Substituição Tributária"));
            cst.Add(new KeyValuePair<int, string>(40, "40 - Isenta"));
            cst.Add(new KeyValuePair<int, string>(40, "40 - Isenta"));
            cst.Add(new KeyValuePair<int, string>(41, "41 - Não tributada"));
            cst.Add(new KeyValuePair<int, string>(50, "50 - Suspensão"));
            cst.Add(new KeyValuePair<int, string>(51, "51 - Diferimento"));
            cst.Add(new KeyValuePair<int, string>(60, "60 - Cobrado anteriormente por Substituição Tributária"));
            cst.Add(new KeyValuePair<int, string>(70, "70 - Com redução da base de cálculo e ICMS de Substituição Tributária"));
            cst.Add(new KeyValuePair<int, string>(90, "90 - Outros"));

            cbCST.SetItemsSource(cst);
        }
    }
}