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

        private int Classe_imposto_id { get; set; }

        public COp_classeImp(int classe_imposto_id)
        {
            InitializeComponent();

            Classe_imposto_id = classe_imposto_id;
            LoadInfo();
        }

        private void LoadInfo()
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

            Classes_imposto classe = Classes_impostoController.Find(Classe_imposto_id);
            txClasse_imp_id.Text = classe.Id.ToString();
            txDescricao_ClImp.Text = classe.Nome;

            Commons.GetList_Ufs().ForEach(uf => cbUF.AddItem(uf));
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

            Operacao.Id = int.Parse(txCod.Text);
            Operacao.Tipos_movimento_id = int.Parse(txCod_tipo_mov.Text);
            Operacao.Classe_imposto_id = int.Parse(txClasse_imp_id.Text);
            Operacao.Cfop_id = txCod_CFOP.Text;
            Operacao.Icms_cst = cbCST.SelectedValue.ToString();
            Operacao.Icms_perc = txIcms_perc.GetDecimal;
            Operacao.Icms_perc_st = txIcms_ST_perc.GetDecimal;
            Operacao.Icms_mva = txMVA.GetDecimal;
            Operacao.Pis_cofins_tipo = cbTipo_Pis.SelectedValue.ToString();
            Operacao.Pis_perc = txPis_perc.GetDecimal;
            Operacao.Pis_perc_st = txPis_st_perc.GetDecimal;
            Operacao.Cofins_perc = txCofins_perc.GetDecimal;
            Operacao.Cofins_perc_st = txCofins_ST_perc.GetDecimal;
            Operacao.Uf = cbUF.Text;

            if (Operacoes_classeImpostoController.Save(Operacao))
            {
                if (close)
                    Close();
                else
                    LimparCampos();
            }
        }

        private void Close()
        {
            if (OnComplete != null) OnComplete();
        }

        private void LimparCampos()
        {
            try
            {
                cbUF.SelectedIndex++;
            }
            catch { }

            txIcms_perc.Text = "0";
            txIcms_ST_perc.Text = "0";
            txPis_perc.Text = "0";
            txPis_st_perc.Text = "0";
            txCofins_perc.Text = "0";
            txCofins_ST_perc.Text = "0";
            txMVA.Text = "0";
            txCod.Text = "0";
            txIcms_perc.SetFocused();

            Operacao = new Operacoes_classe_imposto();
        }

        private void txCod_CFOP_CallSearch()
        {
            SelecionarCFOP sCfop = new SelecionarCFOP();
            sCfop.ShowDialog();
            txCod_CFOP.Text = sCfop.Selecionado.Id;
            txDescricao_cfop.Text = sCfop.Selecionado.Descricao;
        }

        public void Load(int id)
        {
            Operacao = Operacoes_classeImpostoController.Find(id);

            txCod.Text = Operacao.Id.ToString();
            cbUF.Text = Operacao.Uf;
            txCod_tipo_mov.Text = Operacao.Tipos_movimento.Id.ToString();
            txDescricao_Tmv.Text = Operacao.Tipos_movimento.Descricao;
            txClasse_imp_id.Text = Operacao.Classes_imposto.Id.ToString();
            txDescricao_ClImp.Text = Operacao.Classes_imposto.Nome;
            txCod_CFOP.Text = Operacao.Cfop.Id;
            txDescricao_cfop.Text = Operacao.Cfop.Descricao;
            cbCST.SelectedValue = Operacao.Icms_cst;
            txIcms_perc.Text = Operacao.Icms_perc.ToString("N2");
            txIcms_ST_perc.Text = Operacao.Icms_perc_st.ToString("N2");
            txMVA.Text = Operacao.Icms_mva.ToString("N2");
            cbTipo_Pis.SelectedValue = Operacao.Pis_cofins_tipo;
            txPis_perc.Text = Operacao.Pis_perc.ToString("N2");
            txPis_st_perc.Text = Operacao.Pis_perc_st.ToString("N2");
            txCofins_perc.Text = Operacao.Cofins_perc.ToString("N2");
            txCofins_ST_perc.Text = Operacao.Cofins_perc_st.ToString("N2");

            cabecalho.Title = "Alterar operação da classe de imposto";
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
            cst.Add(new KeyValuePair<int, string>(10, "10 - Tributado com cobrança do ICMS por Substituição Tributária"));
            cst.Add(new KeyValuePair<int, string>(20, "20 - Com redução de Base de Cálculo"));
            cst.Add(new KeyValuePair<int, string>(30, "30 - Isenta ou não tributada com ICMS por Substituição Tributária"));
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