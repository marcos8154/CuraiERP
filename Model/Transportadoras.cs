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
    public class Transportadoras 
    {
        public int Id { get; set; }
        public string Razao_social { get; set; }
        public string Nome_fantasia { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Observacoes { get; set; }
        public bool Inativo { get; set; }
        public int Endereco_id { get; set; }
        public int Documento_id { get; set; }
    }
}
