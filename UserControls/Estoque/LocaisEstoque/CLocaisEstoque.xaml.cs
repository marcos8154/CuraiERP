using EM3.Controller;
using EM3.Windows;
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

namespace EM3.UserControls.Estoque.LocaisEstoque
{
    /// <summary>
    /// Interação lógica para CLocaisEstoque.xam
    /// </summary>
    public partial class CLocaisEstoque : UserControl
    {
        public delegate void Complete();
        public event Complete OnComplete;

        Locais_estoque Local_estoque = new Locais_estoque();

        public CLocaisEstoque()
        {
            InitializeComponent();
            LimparCampos();
        }

        public void Load(int id)
        {
            Local_estoque = Locais_estoqueController.Find(id);

            txCod.Text = Local_estoque.Id.ToString();
            txNome.Text = Local_estoque.Nome;
            txCod_armazem.Text = Local_estoque.Armazem_id.ToString();
            txAltura.Text = string.Format("{0:0,0.00}", Local_estoque.Altura);
            cbUn_altura.Text = Local_estoque.Unidade_altura;
            txLargura.Text = string.Format("{0:0,0.00}", Local_estoque.Largura);
            cbUn_largura.Text = Local_estoque.Unidade_largura;
            txComprimento.Text = string.Format("{0:0,0.00}", Local_estoque.Comprimento);
            cbUn_compr.Text = Local_estoque.Unidade_compr;

            cabecalho.Title = "Alterar local de estoque (" + Local_estoque.Nome + ")";
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
            new MsgAlerta(txAltura.GetDouble.ToString());
            Salvar(false);
        }

        private void Salvar(bool close)
        {
            if (Local_estoque == null) Local_estoque = new Locais_estoque();

            Local_estoque.Id = int.Parse(txCod.Text);
            Local_estoque.Nome = txNome.Text;
            Local_estoque.Armazem_id = txCod_armazem.Value;
            Local_estoque.Altura = txAltura.GetDouble;
            Local_estoque.Unidade_altura = cbUn_altura.SelectedValue.ToString();
            Local_estoque.Comprimento = txComprimento.GetDouble;
            Local_estoque.Unidade_compr = cbUn_compr.SelectedValue.ToString();
            Local_estoque.Largura = txLargura.GetDouble;
            Local_estoque.Unidade_largura = cbUn_largura.SelectedValue.ToString();

            if (Locais_estoqueController.Save(Local_estoque))
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
            txCod.Text = "0";
            txNome.Text = string.Empty;
            txAltura.Text = "0";
            txLargura.Text = "0";
            txComprimento.Text = "0";
        }

        private void txCod_armazem_CallSearch()
        {
            SelecionarArmazem sa = new SelecionarArmazem();
            sa.ShowDialog();

            txCod_armazem.Text = sa.Selecionado.Id.ToString();
        }
    }
}
