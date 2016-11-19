using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class Configuration
    {
        public static string application { get; set; }
        public static string server { get; set; }

        public static int port { get; set; }
               
        public static string GetApplication
        {
            get
            {
                return ("http://" + server + ":" + port + "/" + application + "/");                
            }
        }
    }
}
