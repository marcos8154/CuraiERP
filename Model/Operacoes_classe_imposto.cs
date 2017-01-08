/* 27/11/2016 09:25:43 */
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
    public class Operacoes_classe_imposto
    {
        public int Id { get; set; }
        public string Icms_cst { get; set; }
        public decimal Icms_perc { get; set; }
        public decimal Icms_perc_st { get; set; }
        public decimal Icms_mva { get; set; }
        public string Impressora_fiscal_tipo { get; set; }
        public decimal Impressora_fiscal_perc { get; set; }
        public string Pis_cofins_tipo { get; set; }
        public decimal Pis_perc { get; set; }
        public decimal Pis_perc_st { get; set; }
        public decimal Cofins_perc { get; set; }
        public decimal Cofins_perc_st { get; set; }
        public decimal Perc_credito_icms { get; set; }
        public int Classe_imposto_id { get; set; }
        public int Tipos_movimento_id { get; set; }
        public string Cfop_id { get; set; }
        public string Uf { get; set; }
        public Tipos_movimento Tipos_movimento { get; set; }
        public Classes_imposto Classes_imposto { get; set; }
        public Cfop Cfop { get; set; }
    }
}
