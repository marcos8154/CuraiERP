using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Model
{
    public class Itens_pagamento
    {
        public int Id { get; set; }
        public decimal Desconto_perc { get; set; }
        public decimal Desconto_valor { get; set; }
        public decimal Acrescimo_perc { get; set; }
        public decimal Acrescimo_valor { get; set; }
        public int Forma_pagamento_id { get; set; }
        public int Movimento_id { get; set; }

        public Movimentos Movimento { get; set; }
        public Formas_pagamento FormaPagamento { get; set; }
    }
}
