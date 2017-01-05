using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public  class CfopController
    {
        public static List<Cfop> Search(string searchTerm)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", searchTerm);
            rh.Send("cfop-search");

            return EntityLoader<List<Cfop>>.Load(rh.Result);
        }
    }
}
