using EM3.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class UnidadesController
    {
        public static List<Unidades> Search(string searchTerm = "")
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", searchTerm);
            rh.Send("und-search");

            List<Unidades> result = EntityLoader<List<Unidades>>.Load(rh.Result) ?? new List<Unidades>();
            return result;
        }

        public static Unidades Find(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("und-get");

            if (rh.HasSuccess)
                return EntityLoader<Unidades>.Load(rh.Result);
            else
                return new Unidades();
        }

        public static bool Save(Unidades unidade)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", unidade.Id);
            rh.AddParameter("sigla", unidade.Sigla);
            rh.AddParameter("descricao", unidade.Descricao);
            rh.Send("und-save");

            return rh.HasSuccess;
        }

        public static bool Delete(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("und-del");

            return rh.HasSuccess;
        }
    }
}
