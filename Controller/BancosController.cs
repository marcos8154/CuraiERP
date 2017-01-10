using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
   public  class BancosController
    {
        public static List<Bancos> Search(string searchTerm)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", searchTerm);
            rh.Send("bco-search");

            return EntityLoader<List<Bancos>>.Load(rh.Result);
        }
    }
}
