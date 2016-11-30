using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class TelasController
    {
        public static List<Telas> ListAll()
        {
            List<Telas> result = new List<Telas>();

            RequestHelper rh = new RequestHelper();
            rh.Send("telas-list");

            if (rh.HasSuccess)
                result = EntityLoader<List<Telas>>.Load(rh.Result);
            return result;
        }
    }
}
