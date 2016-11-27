/* 27/11/2016 09:25:37 */
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
    public class Formas_pagamento 
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Tipo_pagamento { get; set; }
        public int Banco_id { get; set; }
        public int Operadora_cartao_id { get; set; }
        public bool Permite_entrada { get; set; }
        public int Dia_base { get; set; }
        public int Maximo_parcelas { get; set; }
    }
}
