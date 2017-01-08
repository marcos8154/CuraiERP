using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class Commons
    {
        public static DateTime ServerDate
        {
            get
            {
                RequestHelper rh = new RequestHelper();
                rh.Send("serverdate");

                string result = rh.Result.message;
                return Convert.ToDateTime(result);
            }
        }

        public static string GetDB_Type
        {
            get
            {
                RequestHelper rh = new RequestHelper();
                rh.Send("getdb_type");

                return rh.Result.message;
            }
        }
    
       public static List<string> GetList_Ufs()
        {
            RequestHelper rh = new RequestHelper();
            rh.Send("list-ufs");
            return EntityLoader<List<string>>.Load(rh.Result);
        }
    }
}
