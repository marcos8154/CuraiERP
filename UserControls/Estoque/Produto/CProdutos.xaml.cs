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
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EM3.UserControls.Estoquev.Produto
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

        private bool IsEditMode = false;

        public CProdutos()
        {
            InitializeComponent();

            dataGrid_caract.AplicarPadroes();
            dataGrid_caract.ItemsSource = caracteristicas;
            txOrigem.Text = "0";
            txDesc_origem.Text = "Nacional";
        }

        public void Load(int id)
        {
            produto = ProdutosController.Find(id);

            txCod.Text = produto.Id.ToString();
            txDescricao.Text = produto.Descricao;
            txReferencia.Text = produto.Referencia;
            txEAN.Text = produto.Ean;
            txNcm.Text = produto.Ncm;
            txOrigem.Text = produto.Origem.ToString();
            txDesc_origem.Text = SelecionarOrigemProduto.GetDescricao(produto.Origem);
            txANP.Text = produto.Anp.ToString();
            txCod_classe_imposto.Text = produto.Classe_imposto_id.ToString();
            txDesc_classeImp.Text = Classes_impostoController.Find(produto.Classe_imposto_id).Nome;

            if (produto.Grupo_produtos_id > 0)
            {
                txGrupo.Text = produto.Grupo_produtos_id.ToString();
                txDesc_grupo.Text = Grupos_produtosController.Find(produto.Grupo_produtos_id).Descricao;
            }

            txUnidade.Text = produto.Unidade1.ToString();
            txDesc_unidade.Text = UnidadesController.Find(produto.Unidade1).Descricao;

            if (produto.Unidade2 > 0)
            {
                txUnidade2.Text = produto.Unidade2.ToString();
                txDesc_unidade2.Text = UnidadesController.Find(produto.Unidade2).Descricao;
            }

            txFator_conv.Text = produto.Fator_conversao.ToString();
            txPonto_ped.Text = produto.Ponto_pedido.ToString();
            txPreco_venda.Text = produto.Preco_venda.ToString();
            txUlt_preco.Text = produto.Ult_preco.ToString();

            if (produto.Marca_id > 0)
            {
                txMarca.Text = produto.Marca_id.ToString();
                txDesc_marca.Text = MarcasController.Find(produto.Marca_id).Nome;
            }

            txCusto_standard.Text = produto.Custo_standard.ToString();
            txComissao.Text = produto.Comissao.ToString();
            txPeso_liquido.Text = produto.Peso_liquido.ToString();
            txFabricante.Text = produto.Fabricante;
            txCod_fabricante.Text = produto.Cod_fabricante;
            txProd_equiv.Text = produto.Prod_equivalente.ToString();

            try
            {
                txUlt_compra.Value = Convert.ToDateTime(produto.Ultima_compra);
            }
            catch { }

            txGarant_loja.Text = produto.Garantia_loja.ToString();
            txGarant_forn.Text = produto.Garantia_forn.ToString();
            txForn_padrao.Text = produto.Fornecedor_padrao.ToString();
            txEmpresa_padrao.Text = produto.Empresa_padrao.ToString();
            cbInativo.SelectedIndex = (produto.Inativo ? 1 : 0);
            cbFracionavel.SelectedIndex = (produto.Fracionado ? 1 : 0);
            cbBalanca.SelectedIndex = (produto.Para_balanca ? 1 : 0);
            cbInsumo.SelectedIndex = (produto.Insumo ? 1 : 0);
            cbFabricado.SelectedIndex = (produto.Fabricado ? 1 : 0);
            txLocal_padrao.Text = EstoqueController.GetEstoquePadrao(produto.Id).Id.ToString();
            IsEditMode = true;

            List<Produtos_caractetisticas> list = ProdutosController.ListCaracts(produto.Id);

            if (list != null)
                list.ForEach(e => caracteristicas.Add(e.Caracteristicas));
            dataGrid_caract.ItemsSource = caracteristicas;

            foto.LoadImage(FotoController.GetFile("produto", produto.Foto_id));
            cabecalho.Title = $"Alterar produto ({produto.Descricao})";
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
            if (txLocal_padrao.GetInt == 0)
            {
                MsgAlerta.Show("Informe o local de estoque padrão");
                return;
            }

            if (produto == null)
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
            produto.Garantia_forn = txGarant_forn.GetInt;
            produto.Fornecedor_padrao = txForn_padrao.GetInt;
            produto.Empresa_padrao = txEmpresa_padrao.GetInt;
            produto.Inativo = (cbInativo.SelectedIndex == 1);
            produto.Fracionado = (cbFracionavel.SelectedIndex == 1);
            produto.Para_balanca = (cbBalanca.SelectedIndex == 1);
            produto.Insumo = (cbInsumo.SelectedIndex == 1);
            produto.Fabricado = (cbFabricado.SelectedIndex == 1);
            produto.Foto_id = FotoController.Save(foto.FileName, produto.Foto_id);

            int id_produto = ProdutosController.Save(produto);

            if (id_produto > 0)
            {
                if (IsEditMode)
                {
                    ProdutosController.LimparCaracteristicas(id_produto);

                    Estoque estoqueAtual = EstoqueController.GetEstoquePadrao(id_produto);
                    estoqueAtual.Local_estoque_id = txLocal_padrao.GetInt;
                    EstoqueController.Save(estoqueAtual);
                }
                else
                {
                    Estoque e = new Estoque();
                    e.Local_estoque_id = txLocal_padrao.GetInt;
                    e.Produto_id = id_produto;
                    e.Local_padrao = true;
                    e.Quant = 0;

                    EstoqueController.Save(e);
                }

                foreach (Caracteristicas c in caracteristicas)
                {
                    Produtos_caractetisticas pc = new Produtos_caractetisticas();
                    pc.Produto_id = id_produto;
                    pc.Caracteristica_id = c.Id;

                    ProdutosController.AdicionarCaracteristica(pc);
                }

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
            txDesc_origem.Text = so.Selecionado.Descricao;
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

        private void txMarca_CallSearch()
        {
            SelecionarMarca sm = new SelecionarMarca();
            sm.ShowDialog();

            txMarca.Text = sm.Selecionado.Id.ToString();
            txDesc_marca.Text = (sm.Selecionado.Id == 0
                ? "Não selecionado"
                : sm.Selecionado.Nome);
        }
    }
}
