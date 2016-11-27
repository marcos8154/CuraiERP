/* 27/11/2016 09:25:40 */
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
    public class Movimento_nfs 
    {
        public int Id { get; set; }
        public string Chave_acesso { get; set; }
        public int Tipo_documento { get; set; }
        public int Tipo_emissao { get; set; }
        public int Modelo { get; set; }
        public int Serie { get; set; }
        public int Lote { get; set; }
        public string Data_emissao { get; set; }
        public int Ambiente { get; set; }
        public string Protocolo_autorizacao { get; set; }
        public string Protocolo_denegado { get; set; }
        public int Codigo_status { get; set; }
        public string Descricao_status { get; set; }
        public string Data_autorizacao { get; set; }
        public bool Cancelado { get; set; }
        public string Data_cancelamento { get; set; }
        public string Protocolo_cancelamento { get; set; }
        public bool Inutilizado { get; set; }
        public string Data_inutilizacao { get; set; }
    }
}
