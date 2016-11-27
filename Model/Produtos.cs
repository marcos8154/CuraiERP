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
    public class Produtos 
    {
        public int Id { get; set; }
        public string Ean { get; set; }
        public string Referencia { get; set; }
        public string Descricao { get; set; }
        public string Ncm { get; set; }
        public string Anp { get; set; }
        public string Data_cadastro { get; set; }
        public string Ultima_alteracao { get; set; }
        public bool Fabricado { get; set; }
        public bool Insumo { get; set; }
        public bool Fracionado { get; set; }
        public bool Inativo { get; set; }
        public bool Para_balanca { get; set; }
        public string Marca { get; set; }
        public string Fabricante { get; set; }
        public string Cod_fabricante { get; set; }
        public int Unidade1 { get; set; }
        public int Fator_conversao { get; set; }
        public int Unidade2 { get; set; }
        public decimal Custo_medio { get; set; }
        public int Classes_imposto_id { get; set; }
        public int Cest_id { get; set; }
    }
}
