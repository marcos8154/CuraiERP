/* 27/11/2016 09:25:36 */
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
    public class Operadoras_cartao 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Tipo { get; set; }
        public int Tipo_recebimento { get; set; }
        public int Prazo_recebimento { get; set; }
        public decimal Taxa { get; set; }
        public bool Inativo { get; set; }

        public TIPO_RECEBIMENTO GetTipo_recebimento()
        {
            switch(Tipo_recebimento)
            {
                case 0: return TIPO_RECEBIMENTO.DIAS;
                case 1: return TIPO_RECEBIMENTO.HORAS;
            }

            return TIPO_RECEBIMENTO.DIAS;
        }

        public enum TIPO_RECEBIMENTO
        {
            DIAS = 0,
            HORAS = 1
        }

        public TIPO_OPERACAO GetTipo_operacao()
        {
            switch (Tipo)
            {
                case 0: return TIPO_OPERACAO.CREDITO;
                case 1: return TIPO_OPERACAO.DEBITO;
            }

            return TIPO_OPERACAO.CREDITO;
        }

        public enum TIPO_OPERACAO
        {
            CREDITO = 0,
            DEBITO = 1
        }
    }
}
