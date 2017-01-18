using EM3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class NcmController
    {
        public static List<Ncm> Search(string searchTerm, int page)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", searchTerm);
            rh.AddParameter("page", page);
            rh.Send("ncm-search");

            return EntityLoader<List<Ncm>>.Load(rh.Result);
        }

        public static int Count
        {
            get
            {
                RequestHelper rh = new RequestHelper();
                rh.Send("ncm-count");

                return EntityLoader<int>.Load(rh.Result);
            }
        }
    }
}
