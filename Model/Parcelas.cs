/* 15/11/2016 17:01:19 */
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
    public class Parcelas 
    {
        public int Id { get; set; }
        public int Tipo { get; set; }
        public string Parcela_numero { get; set; }
        public int Plano_conta_id { get; set; }
        public int Forma_pagamento_id { get; set; }
        public int Conta_bancaria_id { get; set; }
        public int Operadora_cartao_id { get; set; }
        public int Cheque_numero { get; set; }
        public string Data_emissao { get; set; }
        public string Data_vencimento { get; set; }
        public decimal Valor { get; set; }
        public bool Pago { get; set; }
        public int Movimento_id { get; set; }
    }
}
