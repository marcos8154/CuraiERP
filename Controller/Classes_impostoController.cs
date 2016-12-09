using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class Classes_impostoController
    {
        public static bool Delete(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("climp-rem");

            return rh.HasSuccess;
        }

        public static List<Classes_imposto> Search(string searchTerm)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", searchTerm);
            rh.Send("climp-search");

            return EntityLoader<List<Classes_imposto>>.Load(rh.Result);
        }

        public static bool Save(Classes_imposto classe)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", classe.Id);
            rh.AddParameter("nome", classe.Nome);
            rh.AddParameter("data_alteracao", classe.Data_alteracao);
            rh.Send("climp-save");

            return rh.HasSuccess;
        }

        public static Classes_imposto Find(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("climp-get");

            return (rh.HasSuccess
                ? EntityLoader<Classes_imposto>.Load(rh.Result)
                : new Classes_imposto());
        }
    }
}
