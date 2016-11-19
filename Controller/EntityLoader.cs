using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class EntityLoader<T>
    {
        public static T Load(OperationResult result)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(result.entity.ToString());
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
