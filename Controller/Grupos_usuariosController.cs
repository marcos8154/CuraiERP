using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
   public  class Grupos_usuariosController
    {
        public static List<Grupos_usuarios> Search(string searchTerm)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", searchTerm);
            rh.Send("gusr-search");

            List<Grupos_usuarios> result = EntityLoader<List<Grupos_usuarios>>.Load(rh.Result);
            return result;
        }

        internal static Grupos_usuarios Find(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("gusr-get");

            Grupos_usuarios grupo = EntityLoader<Grupos_usuarios>.Load(rh.Result);
            return grupo;
        }

        public static bool Save(Grupos_usuarios grupo)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", grupo.Id);
            rh.AddParameter("nome", grupo.Nome);
            rh.Send("gusr-save");

            return rh.HasSuccess;
        }

        public static bool Remove(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("gusr-rem");

            return rh.HasSuccess;
        }
    }
}
