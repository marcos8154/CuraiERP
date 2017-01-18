/* 27/11/2016 09:25:41 */
/* AUTO-GENERATED CLASS */
/* DOES NOT ADD PROPERTIES */
/* DOES NOT CHANGE NAME OF PROPERTIES */
/* IMPLEMENTS INTERFACES OR ABSTRACT CLASSES DOES NOT CHANGE THE OPERATION */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EM3
{
    public class Tabelas_precos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Data_inicio { get; set; }
        public string Data_inativacao { get; set; }
        public int Forma_pagamento_id { get; set; }
        public bool Inativo { get; set; }

        public Formas_pagamento Formas_pagamento { get; set; }
    }
}
