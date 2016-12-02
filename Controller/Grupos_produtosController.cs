using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class Grupos_produtosController
    {
        public static Grupos_produtos Find (int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("gprod-get");

            if (rh.HasSuccess)
                return EntityLoader<Grupos_produtos>.Load(rh.Result);
            return new Grupos_produtos();
        }

        public static List<Grupos_produtos> Search(string searchTerm = "", int tipo = 0)
        {
            List<Grupos_produtos> result = new List<Grupos_produtos>();

            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", searchTerm);
            rh.AddParameter("tipo", tipo);
            rh.Send("gprod-search");

            return (rh.HasSuccess
                ? EntityLoader<List<Grupos_produtos>>.Load(rh.Result)
                : result);
        }

        public static bool Save(Grupos_produtos grupo)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", grupo.Id);
            rh.AddParameter("descricao", grupo.Descricao);
            rh.AddParameter("inativo", grupo.Inativo);
            rh.AddParameter("foto_id", grupo.Foto_id);
            rh.Send("gprod-save");
            return (rh.HasSuccess);
        }

        public static bool Delete(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("gprod-rem");

            return (rh.HasSuccess);
        }
    }
}
