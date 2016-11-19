/* 15/11/2016 17:01:18 */
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
    public class Tipos_movimento 
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Movimentacao_financeiro { get; set; }
        public int Movimentacao_estoque { get; set; }
        public bool Gera_comissao { get; set; }
        public bool Gera_nfe { get; set; }
        public bool Gera_nfce { get; set; }
        public bool Inativo { get; set; }
    }
}
