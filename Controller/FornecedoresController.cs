using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class FornecedoresController
    {
        public static List<Fornecedores> Search(string searchTerm)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", searchTerm);
            rh.Send("forn-search");

            List<Fornecedores> result = EntityLoader<List<Fornecedores>>.Load(rh.Result) ?? new List<Fornecedores>();
            return result;
        }
    }
}
