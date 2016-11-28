using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Model
{
    public class Movimentacoes_caixas
    {
        public int Id { get; set; }
        public String Data_abertura { get; set; }
        public int Usuario_abertura { get; set; }
        public String Data_fechamento { get; set; }
        public int Usuario_fechamento { get; set; }
        public decimal Dinheiro { get; set; }
        public decimal Cartao { get; set; }
        public decimal Cheque { get; set; }
        public decimal Credito_cliente { get; set; }
        public decimal Boleto { get; set; }
        public decimal Troco { get; set; }
        public int Caixa_id { get; set; }

        public Usuarios UsuarioAbertura { get; set; }
        public Usuarios UsuarioFechamento { get; set; }
    }
}
