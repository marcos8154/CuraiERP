using EM3.Controller;
using EM3.Extensions;
using EM3.Windows;
using EM3.Windows.Selecao;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

namespace EM3.UserControls.Estoque.Produto
{
    /// <summary>
    /// Interação lógica para CProdutos.xam
    /// </summary>
    public partial class CProdutos : UserControl
    {
        public delegate void Complete();
        public event Complete OnComplete;

        List<Caracteristicas> caracteristicas = new List<Caracteristicas>();
        Produtos produto;
        public CProdutos()
        {
            InitializeComponent();

            dataGrid_caract.AplicarPadroes();
            dataGrid_caract.ItemsSource = caracteristicas;
            txOrigem.Text = "0";
            txDesc_origem.Text = "Nacional";
        }

        private void btSalvar_OnClick()
        {
            Salvar(true);
        }

        private void btSalvarEContinuar_OnClick()
        {
            Salvar(false);
        }

        private void btCancelar_OnClick()
        {
            Fechar();
        }

        private void Salvar(bool close)
        {
            if(txLocal_padrao.GetInt == 0)
            {
                MsgAlerta.Show("Informe o local de estoque padrão");
                return;
            }
            produto = new Produtos();

            produto.Id = txCod.GetInt;
            produto.Descricao = txDescricao.Text;
            produto.Referencia = txReferencia.Text;
            produto.Ean = txEAN.Text;
            produto.Ncm = txNcm.Text;
            produto.Origem = txOrigem.GetInt;
            produto.Anp = txANP.GetInt;
            produto.Classe_imposto_id = txCod_classe_imposto.GetInt;
            produto.Grupo_produtos_id = txGrupo.GetInt;
            produto.Unidade1 = txUnidade.GetInt;
            produto.Fator_conversao = txFator_conv.GetInt;
            produto.Unidade2 = txUnidade2.GetInt;
            produto.Marca_id = txMarca.GetInt;
            produto.Ponto_pedido = txPonto_ped.GetDecimal;
            produto.Preco_venda = txPreco_venda.GetDecimal;
            produto.Ult_preco = txUlt_preco.GetDecimal;
            produto.Custo_standard = txCusto_standard.GetDecimal;
            produto.Comissao = txComissao.GetDecimal;
            produto.Peso_liquido = txPeso_liquido.GetDecimal;
            produto.Fabricante = txFabricante.Text;
            produto.Cod_fabricante = txCod_fabricante.Text;
            produto.Prod_equivalente = txProd_equiv.GetInt;
            produto.Ultima_compra = txUlt_compra.Value.ToShortDateString();
            produto.Garantia_loja = txGarant_loja.GetInt;
            produto.Garantia_fornecedor = txGarant_forn.GetInt;
            produto.Fornecedor_padrao = txForn_padrao.GetInt;
            produto.Empresa_padrao = txEmpresa_padrao.GetInt;
            produto.Inativo = (cbInativo.SelectedIndex == 1);
            produto.Fracionado = (cbFracionavel.SelectedIndex == 1);
            produto.Para_balanca = (cbBalanca.SelectedIndex == 1);
            produto.Insumo = (cbInsumo.SelectedIndex == 1);
            produto.Fabricado = (cbFabricado.SelectedIndex == 1);

            int result = ProdutosController.Save(produto);

            if (result > 0)
            {
                /*
                        TODO:
                        * salvar local do estoque padrão
                        * salvar caracteristocas (excluir tudo e inserir denovo)
                        * salvar tabela de preco (caso informado)
                */
                if (close)
                    Fechar();
                else
                    LimparCampos();
            }
        }

        private void Fechar()
        {
            if (OnComplete != null) OnComplete();
        }

        private void LimparCampos()
        {

        }

        private void ComboBox_OnChange(object sender, SelectionChangedEventArgs e)
        {
            switch (cbOpcoes.SelectedIndex)
            {
                case 1:
                    Process.Start("Calc.exe");
                    break;

                case 2:
                    //Imprimir ficha do produto
                    break;

                case 3:
                    //Inserir produto em uma tabela de preço
                    break;

                case 4:
                    //Selecionar produto para copiar
                    break;
            }

            cbOpcoes.SelectedIndex = 0;
        }

        private void SearchBox_CallSearch()
        {
            SelecionarCaracteristica sc = new SelecionarCaracteristica();
            sc.ShowDialog();

            if (sc.Selecionado.Id == 0)
                return;
            txCod_caracteristica.Text = sc.Selecionado.Id.ToString();
            Caracteristicas crt = CaracteristicasController.Find(sc.Selecionado.Id);
            if (caracteristicas.FirstOrDefault(c => c.Id == crt.Id) == null)
            {
                caracteristicas.Add(crt);
                dataGrid_caract.Items.Refresh();
            }
            txCod_caracteristica.Text = "0";
        }

        private void dataGrid_caract_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                RemoveCaracteristica();
            }
        }

        private void RemoveCaracteristica()
        {
            Caracteristicas c = (Caracteristicas)dataGrid_caract.SelectedItem;
            if (c == null)
                return;
            if (c.Id == 0)
                return;
            caracteristicas.Remove(c);
            dataGrid_caract.Items.Refresh();
        }

        private void ImageButton_OnClick()
        {
            RemoveCaracteristica();
        }

        private void txNcm_CallSearch()
        {
            SelecionarNcm sn = new SelecionarNcm();
            sn.ShowDialog();

            txNcm.Text = sn.Selecionado.Cod_ncm;
        }

        private void txOrigem_CallSearch()
        {
            SelecionarOrigemProduto so = new SelecionarOrigemProduto();
            so.ShowDialog();

            txOrigem.Text = so.Selecionado.Id.ToString();
            txDesc_origem.Text =  so.Selecionado.Descricao;
        }

        private void txUnidade_CallSearch()
        {
            SelecionarUnidade su = new SelecionarUnidade();
            su.ShowDialog();

            txUnidade.Text = su.Selecionado.Id.ToString();
            txDesc_unidade.Text = (su.Selecionado.Id == 0
                ? "Não selecionado"
                : su.Selecionado.Descricao); 
        }

        private void txUnidade2_CallSearch()
        {
            SelecionarUnidade su = new SelecionarUnidade();
            su.ShowDialog();

            txUnidade2.Text = su.Selecionado.Id.ToString();
            txDesc_unidade2.Text = (su.Selecionado.Id == 0
                ? "Não selecionado"
                : su.Selecionado.Descricao);
        }

        private void txCod_classe_imposto_CallSearch()
        {
            SelecionarClasseImposto sci = new SelecionarClasseImposto();
            sci.ShowDialog();

            txCod_classe_imposto.Text = sci.Selecionado.Id.ToString();
            txDesc_classeImp.Text = (sci.Selecionado.Id == 0
                ? "Não selecionado"
                : sci.Selecionado.Nome);
        }

        private void txGrupo_CallSearch()
        {
            SelecionarGrupoProdutos sgp = new SelecionarGrupoProdutos();
            sgp.ShowDialog();

            txGrupo.Text = sgp.Selecionado.Id.ToString();
            txDesc_grupo.Text = (sgp.Selecionado.Id == 0
                ? "Não selecionado"
                : sgp.Selecionado.Descricao);
        }

        private void txLocal_padrao_CallSearch()
        {
            SelecionarLocalEstoque sle = new SelecionarLocalEstoque();
            sle.ShowDialog();

            txLocal_padrao.Text = sle.Selecionado.Id.ToString();
        }

        private void txEmpresa_padrao_CallSearch()
        {
            SelecionarEmpresa se = new SelecionarEmpresa();
            se.ShowDialog();

            txEmpresa_padrao.Text = se.Selecionado.Id.ToString();
        }
    }
}
