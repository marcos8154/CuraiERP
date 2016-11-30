using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class ArmazensController
    {
        public static List<Armazens> Search(int empresa_id, string searchTerm = "")
        {
            List<Armazens> result = new List<Armazens>();

            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", searchTerm);
            rh.AddParameter("empresa_id", empresa_id);
            rh.Send("armz-search");
           
            if (rh.HasSuccess)
                result = EntityLoader<List<Armazens>>.Load(rh.Result);
            return result;
        }

        internal static bool Save(Armazens armazem)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", armazem.Id);
            rh.AddParameter("nome", armazem.Nome);
            rh.AddParameter("tipo_armazem", armazem.Tipo_armazem);
            rh.AddParameter("empresa_id", armazem.Empresa_id);
            rh.Send("armz-save");

            return (rh.HasSuccess);
        }

        internal static bool Delete(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("armz-rem");

            return (rh.HasSuccess);
        }

        internal static Armazens Find(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("armz-get");

            if (rh.HasSuccess)
                return EntityLoader<Armazens>.Load(rh.Result);
            return new Armazens();
        }
    }
}
