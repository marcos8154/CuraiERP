using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class Produtos_precosController
    {
        public static bool Save(Produtos_precos pp)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", pp.Id);
            rh.AddParameter("produto_id", pp.Produto_id);
            rh.AddParameter("tabela_id", pp.Tabela_id);
            rh.AddParameter("preco_base", pp.Preco_base);
            rh.AddParameter("margem", pp.Margem);
            rh.AddParameter("valor", pp.Valor);
            rh.AddParameter("preco_padrao", pp.Preco_padrao);
            rh.AddParameter("uf", pp.Uf);
            rh.AddParameter("faixa", pp.Faixa);
            rh.Send("prdp-save");

            return rh.HasSuccess;
        }

        public static Produtos_precos Find(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("prdp-find");

            return (rh.HasSuccess
                ? EntityLoader<Produtos_precos>.Load(rh.Result)
                : new Produtos_precos());
        }

        public static bool Remove(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("prdp-rem");

            return rh.HasSuccess;
        }

        public static List<Produtos_precos> ListByProduto(int produto_id, int tabela_ignorar)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("produto_id", produto_id);
            rh.AddParameter("tabela_ignorar", tabela_ignorar);
            rh.Send("prdp-listbyproduto");

            return EntityLoader<List<Produtos_precos>>.Load(rh.Result);
        }

        public static List<Produtos_precos> ListByTabela(int tabela_id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("tabela_id", tabela_id);
            rh.Send("prdp-listbytabela");

            return EntityLoader<List<Produtos_precos>>.Load(rh.Result);
        }

        public static List<Produtos_precos> ListByProdutoTabela(int produto_id, int tabela_id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("produto_id", produto_id);
            rh.AddParameter("tabela_id", tabela_id);
            rh.Send("prdp-listbyprodutotabela");

            return EntityLoader<List<Produtos_precos>>.Load(rh.Result);
        }
    }
}
