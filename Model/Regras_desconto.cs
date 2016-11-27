/* 27/11/2016 09:25:44 */
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
    public class Regras_desconto 
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Grupo_clientes_id { get; set; }
        public int Cliente_id { get; set; }
        public int Tabela_preco_id { get; set; }
        public int Tipo_movimento_id { get; set; }
        public int Forma_pagamento_id { get; set; }
        public decimal Faixa_valores { get; set; }
        public int Tipo_faixa_vlr { get; set; }
        public decimal Desconto_perc { get; set; }
        public bool Inativo { get; set; }
        public string Data_final { get; set; }
    }
}
