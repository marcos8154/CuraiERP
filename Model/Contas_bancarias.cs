/* 27/11/2016 09:25:39 */
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
    public class Contas_bancarias 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Agencia { get; set; }
        public string Dv_agencia { get; set; }
        public string Conta { get; set; }
        public string Dv_conta { get; set; }
        public string Carteira { get; set; }
        public string Correntista { get; set; }
        public decimal Saldo_atual { get; set; }
        public decimal Limite_credito { get; set; }
        public int Banco_id { get; set; }
        public bool Inativo { get; set; }
        public Bancos Bancos { get; set; }
    }
}
