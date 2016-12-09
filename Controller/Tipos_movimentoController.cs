using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class Tipos_movimentoController
    {
        public static bool Save(Tipos_movimento tmv)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", tmv.Id);
            rh.AddParameter("descricao", tmv.Descricao);
            rh.AddParameter("movimentacao_financeiro", tmv.Movimentacao_financeiro);
            rh.AddParameter("movimentacao_estoque", tmv.Movimentacao_estoque);
            rh.AddParameter("gera_comissao", tmv.Gera_comissao);
            rh.AddParameter("gera_nfe", tmv.Gera_nfe);
            rh.AddParameter("gera_nfce", tmv.Gera_nfce);
            rh.AddParameter("inativo", tmv.Inativo);
            rh.Send("tmv-save");

            return rh.HasSuccess;
        }

        public static bool Delete(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("tmv-rem");

            return rh.HasSuccess;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="tipo">0 - Somente inativos; 1 - Somente ativos; 2 - Todos</param>
        /// <returns></returns>
        public static List<Tipos_movimento> Search(string searchTerm, int tipo)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", searchTerm);
            rh.AddParameter("tipo", tipo);
            rh.Send("tmv-search");

            return EntityLoader<List<Tipos_movimento>>.Load(rh.Result);
        }

        public static Tipos_movimento Find(int id)
        {
            Tipos_movimento tmv = new Tipos_movimento();

            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("tmv-get");

            if (rh.HasSuccess)
                tmv = EntityLoader<Tipos_movimento>.Load(rh.Result);

            return tmv;
        }
    }
}
