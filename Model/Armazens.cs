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
    public class Armazens 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Tipo_armazem { get; set; }
        public bool Armazem_padrao { get; set; }
        public decimal Largura { get; set; }
        public string Unidade_largura { get; set; }
        public decimal Altura { get; set; }
        public string Unidade_altura { get; set; }
        public decimal Comprimento { get; set; }
        public string Unidade_compr { get; set; }
    }
}
