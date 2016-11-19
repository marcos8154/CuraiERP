using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class OperationResult
    {
        public int status { get; set; }

        public string message { get; set; }

        public object entity { get; set; }
    }
}
