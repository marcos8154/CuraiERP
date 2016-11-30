using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class CaracteristicasController
    {
        public static List<Caracteristicas> Search(string searchTerm = "")
        {
            List<Caracteristicas> result = new List<Caracteristicas>();

            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", searchTerm);
            rh.Send("caract-search");

            if (rh.HasSuccess)
                result = EntityLoader<List<Caracteristicas>>.Load(rh.Result);
            return result;
        }

        public static Caracteristicas Find(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("caract-get");

            if (rh.HasSuccess)
                return EntityLoader<Caracteristicas>.Load(rh.Result);
            else
                return new Caracteristicas();
        }

        public static bool Save(Caracteristicas caracts)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", caracts.Id);
            rh.AddParameter("atributo", caracts.Atributo);
            rh.AddParameter("valor", caracts.Valor);
            rh.Send("caract-save");

            return (rh.HasSuccess);
        }

        internal static bool Delete(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("caract-rem");

            return (rh.HasSuccess);
        }
    }
}
