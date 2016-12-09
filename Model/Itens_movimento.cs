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
    public class Itens_movimento 
    {
        public int Id { get; set; }
        public int Tipo_item { get; set; }
        public int Produto_id { get; set; }
        public int Servico_id { get; set; }
        public decimal Quant { get; set; }
        public decimal Valor_unit { get; set; }
        public decimal Base_icms { get; set; }
        public decimal Total_icms { get; set; }
        public decimal Base_icms_st { get; set; }
        public decimal Total_icms_st { get; set; }
        public decimal Icms_mva { get; set; }
        public decimal Base_ipi { get; set; }
        public decimal Total_ipi { get; set; }
        public decimal Base_pis { get; set; }
        public decimal Total_pis { get; set; }
        public decimal Base_cofins { get; set; }
        public decimal Total_cofins { get; set; }
        public int Icms_perc { get; set; }
        public int Icms_perc_st { get; set; }
        public int Cred_icms_perc { get; set; }
        public int Total_cred_icms { get; set; }
        public decimal Desconto_perc { get; set; }
        public decimal Desconto_valor { get; set; }
        public decimal Comissao_perc { get; set; }
        public decimal Comissao_valor { get; set; }
        public decimal Valor_outros { get; set; }
        public decimal Valor_total { get; set; }
        public int Funcionario_id { get; set; }
        public string Cfop_id { get; set; }
        public int Movimento_id { get; set; }
    }
}
