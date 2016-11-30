/* 27/11/2016 09:25:45 */
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
    public class Estoque 
    {
        public int Id { get; set; }
        public int Produto_id { get; set; }
        public int Local_estoque_id { get; set; }
        public decimal Quant { get; set; }
        public bool Local_padrao { get; set; }
    }
}
