using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class Locais_estoqueController
    {
        public static List<Locais_estoque> Search(string searchTerm)
        {
            List<Locais_estoque> result = new List<Locais_estoque>();

            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", searchTerm);
            rh.Send("loest-search");

            if (rh.HasSuccess)
                result = EntityLoader<List<Locais_estoque>>.Load(rh.Result);
            return result;
        }

        public static bool Save(Locais_estoque le)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", le.Id);
            rh.AddParameter("nome", le.Nome);
            rh.AddParameter("largura", le.Largura);
            rh.AddParameter("unidade_largura", le.Unidade_largura);
            rh.AddParameter("altura", le.Altura);
            rh.AddParameter("unidade_altura", le.Unidade_altura);
            rh.AddParameter("comprimento", le.Comprimento);
            rh.AddParameter("unidade_compr", le.Unidade_compr);
            rh.AddParameter("armazem_id", le.Armazem_id);
            rh.Send("loest-save");
            return (rh.HasSuccess);
        }

        public static Locais_estoque Find(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("loest-get");
            if (rh.HasSuccess)
                return EntityLoader<Locais_estoque>.Load(rh.Result);
            return new Locais_estoque();
        }

        public static bool Delete(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("loest-rem");
            return (rh.HasSuccess);
        }
    }
}
