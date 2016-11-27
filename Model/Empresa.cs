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
    public class Empresa 
    {
        public int Id { get; set; }
        public string Nome_fantasia { get; set; }
        public string Razao_social { get; set; }
        public string Cnpj { get; set; }
        public int Crt { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Responsavel { get; set; }
        public bool Ativo { get; set; }
        public int Tipo { get; set; }
        public int Endereco_id { get; set; }
        public Enderecos Enderecos { get; set; }
        public int Foto_id { get; set; }
    }
}
