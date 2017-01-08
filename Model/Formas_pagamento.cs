/* 27/11/2016 09:25:37 */
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
    public class Formas_pagamento 
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Tipo_pagamento { get; set; }
        public int Conta_bancaria_id { get; set; }
        public int Operadora_cartao_id { get; set; }
        public bool Permite_entrada { get; set; }
        public string Tipo_intervalo { get; set; }
        public int Intervalo { get; set; }
        public int Dia_base { get; set; }
        public int Tolerancia_dias { get; set; }
        public decimal Juros_atraso { get; set; }
        public int Parcelas { get; set; }
        public bool Inativo { get; set; }

        public Contas_bancarias Contas_bancarias { get; set; }
        public Operadoras_cartao Operadoras_cartao { get; set; }

        public TIPO_INTERVALO GetTipo_intervalo()
        {
            switch(Tipo_intervalo)
            {
                case "I": return TIPO_INTERVALO.INTERVALO;
                case "D": return TIPO_INTERVALO.DIA;
            }

            return TIPO_INTERVALO.INTERVALO;
        }

        public enum TIPO_INTERVALO
        {
            INTERVALO = 0,
            DIA = 1
        }

        public TIPO_PAGAMENTO GetTipoPG()
        {
            switch(Tipo_pagamento)
            {
                case 0: return TIPO_PAGAMENTO.DINHEIRO;
                case 1: return TIPO_PAGAMENTO.CARTAO;
                case 2: return TIPO_PAGAMENTO.CHEQUE;
                case 3: return TIPO_PAGAMENTO.CREDITO_CLIENTE;
                case 4: return TIPO_PAGAMENTO.BOLETO;
            }

            return TIPO_PAGAMENTO.DINHEIRO;
        }

        public enum TIPO_PAGAMENTO
        {
            DINHEIRO = 0,
            CARTAO = 1,
            CHEQUE = 2,
            CREDITO_CLIENTE = 3,
            BOLETO = 4
        }
    }
}
