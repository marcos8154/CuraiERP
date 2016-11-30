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
    public class Locais_estoque
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Largura { get; set; }
        public string Unidade_largura { get; set; }
        public double Altura { get; set; }
        public string Unidade_altura { get; set; }
        public double Comprimento { get; set; }
        public string Unidade_compr { get; set; }
        public int Armazem_id { get; set; }
        public Armazens Armazem { get; set; }
    }
}
