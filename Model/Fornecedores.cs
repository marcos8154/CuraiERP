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
    public class Fornecedores 
    {
        public int Id { get; set; }
        public int Loja_fornecedor { get; set; }
        public string Razao_social { get; set; }
        public string Nome_fantasia { get; set; }
        public string Telefone1 { get; set; }
        public string Inscr_municipal { get; set; }
        public string Inscr_estadual { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Observacoes { get; set; }
        public string Website { get; set; }
        public bool Ativo { get; set; }
        public decimal Credito { get; set; }
        public string Ultima_compra { get; set; }
        public int Tipo_pessoa { get; set; }
        public int Endereco_id { get; set; }
        public int Documento_id { get; set; }
        public int Banco_id { get; set; }
    }
}
