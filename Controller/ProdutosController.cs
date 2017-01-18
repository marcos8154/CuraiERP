using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class ProdutosController
    {
        public static List<Produtos> Search(string searchTerm, int tipo, int page)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", searchTerm);
            rh.AddParameter("tipo", tipo);
            rh.AddParameter("page", page);
            rh.Send("prd-search");

            return EntityLoader<List<Produtos>>.Load(rh.Result);
        }

        public static Produtos Find(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("prd-find");

            return (rh.HasSuccess
                ? EntityLoader<Produtos>.Load(rh.Result)
                : new Produtos());
        }
    
        public static int Count()
        {
            RequestHelper rh = new RequestHelper();
            rh.Send("prd-count");
            return EntityLoader<int>.Load(rh.Result);
        }

        public static int Save(Produtos produto)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", produto.Id);
            rh.AddParameter("ean", produto.Ean);
            rh.AddParameter("referencia", produto.Referencia);
            rh.AddParameter("descricao", produto.Descricao);
            rh.AddParameter("ncm", produto.Ncm);
            rh.AddParameter("fabricado", produto.Fabricado);
            rh.AddParameter("insumo", produto.Insumo);
            rh.AddParameter("fracionado", produto.Fracionado);
            rh.AddParameter("inativo", produto.Inativo);
            rh.AddParameter("para_balanca", produto.Para_balanca);
            rh.AddParameter("fabricante", produto.Fabricante);
            rh.AddParameter("cod_fabricante", produto.Cod_fabricante);
            rh.AddParameter("unidade1", produto.Unidade1);
            rh.AddParameter("fator_conversao", produto.Fator_conversao);
            rh.AddParameter("unidade2", produto.Unidade2);
            rh.AddParameter("classe_imposto_id", produto.Classe_imposto_id);
            rh.AddParameter("cest_id", 0);
            rh.AddParameter("grupo_produtos_id", produto.Classe_imposto_id);
            rh.AddParameter("marca_id", produto.Marca_id);
            rh.AddParameter("validade", produto.Validade);
            rh.AddParameter("peso_liquido", produto.Peso_liquido);
            rh.AddParameter("ponto_pedido", produto.Ponto_pedido);
            rh.AddParameter("fornecedor_padrao", produto.Fornecedor_padrao);
            rh.AddParameter("custo_standard", produto.Custo_standard);
            rh.AddParameter("preco_venda", produto.Preco_venda);
            rh.AddParameter("ult_preco", produto.Ult_preco);
            rh.AddParameter("comissao", produto.Comissao);
            rh.AddParameter("ultima_compra", produto.Ultima_alteracao);
            rh.AddParameter("prod_equivalente", produto.Prod_equivalente);
            rh.AddParameter("empresa_padrao", produto.Empresa_padrao);
            rh.AddParameter("garantia_loja", produto.Garantia_loja);
            rh.AddParameter("garantia_forn", produto.Garantia_fornecedor);
            rh.AddParameter("foto_id", produto.Foto_id);

            rh.Send("prd-save");

            return (rh.HasSuccess
                ? EntityLoader<int>.Load(rh.Result)
                : 0);
        }

        public static bool Remove(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("prd-rem");

            return rh.HasSuccess;
        }
    }
}
