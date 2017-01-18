using EM3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class MarcasController
    {
        public static bool Save(Marcas marca)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", marca.Id);
            rh.AddParameter("nome", marca.Nome);
            rh.AddParameter("foto_id", marca.Foto_id);
            rh.Send("mrc-save");

            return rh.HasSuccess;
        }

        public static Marcas Find(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("mrc-find");

            return (rh.HasSuccess
                ? EntityLoader<Marcas>.Load(rh.Result)
                : new Marcas());
        }

        public static bool Remove(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("mrc-rem");
            
            return rh.HasSuccess;
        }

        public static List<Marcas> Search(string searchTerm)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", searchTerm);
            rh.Send("mrc-search");

            return EntityLoader<List<Marcas>>.Load(rh.Result);
        }
    }
}
