/* 27/11/2016 09:25:42 */
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
    public class Bloqueios_clientes 
    {
        public int Id { get; set; }
        public string Data_bloqueio { get; set; }
        public string Data_desbloqueio { get; set; }
        public int Gatilho_desbloqueio { get; set; }
        public int Cliente_id { get; set; }
    }
}
