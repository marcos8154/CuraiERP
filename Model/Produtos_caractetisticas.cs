/* 27/11/2016 09:25:36 */
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
    public class Produtos_caractetisticas 
    {
        public int Id { get; set; }
        public int Caracteristica_id { get; set; }
        public int Produto_id { get; set; }
        public Caracteristicas Caracteristicas { get; set; }
        public Produtos Produtos { get; set; }
    }
}
