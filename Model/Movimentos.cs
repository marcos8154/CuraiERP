/* 27/11/2016 09:25:38 */
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
    public class Movimentos 
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public bool Parcelado { get; set; }
        public bool Efetivado { get; set; }
        public string Obs { get; set; }
        public decimal Valor_frete { get; set; }
        public decimal Base_icms { get; set; }
        public decimal Total_icms { get; set; }
        public decimal Base_icms_st { get; set; }
        public decimal Total_icms_st { get; set; }
        public decimal Base_pis { get; set; }
        public decimal Total_pis { get; set; }
        public decimal Base_cofins { get; set; }
        public decimal Total_cofins { get; set; }
        public decimal Base_ipi { get; set; }
        public decimal Total_ipi { get; set; }
        public decimal Valor_outro { get; set; }
        public int Fornecedor_id { get; set; }
        public int Cliente_id { get; set; }
        public int Transportadora_id { get; set; }
        public int Nf_id { get; set; }
        public int Usuario_id { get; set; }
        public int Caixa_id { get; set; }
        public int Empresa_id { get; set; }
        public int Tipo_movimento_id { get; set; }
    }
}
