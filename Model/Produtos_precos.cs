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
    public class Produtos_precos 
    {
        public int Id { get; set; }
        public int Produto_id { get; set; }
        public int Tabela_id { get; set; }
        public decimal Valor { get; set; }
        public bool Preco_padrao { get; set; }
        public string Uf { get; set; }

        public Produtos Produtos { get; set; }
        public Tabelas_precos Tabelas_precos { get; set; }
    }
}
