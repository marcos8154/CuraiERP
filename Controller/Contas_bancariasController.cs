using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class Contas_bancariasController
    {
        public static bool Save(Contas_bancarias conta)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", conta.Id);
            rh.AddParameter("nome", conta.Nome);
            rh.AddParameter("agencia", conta.Agencia);
            rh.AddParameter("dv_agencia", conta.Dv_agencia);
            rh.AddParameter("conta", conta.Conta);
            rh.AddParameter("dv_conta", conta.Dv_conta);
            rh.AddParameter("carteira", conta.Carteira);
            rh.AddParameter("correntista", conta.Correntista);
            rh.AddParameter("saldo_atual", conta.Saldo_atual);
            rh.AddParameter("limite_credito", conta.Limite_credito);
            rh.AddParameter("banco_id", conta.Banco_id);
            rh.AddParameter("inativo", conta.Inativo);
            rh.Send("cntb-save");

            return rh.HasSuccess;
        }

        public static Contas_bancarias Find(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("cntb-find");

            return (rh.HasSuccess
                ? EntityLoader<Contas_bancarias>.Load(rh.Result)
                : new Contas_bancarias());
        }

        public static bool Remove(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("cntb-rem");

            return rh.HasSuccess;
        }

        public static List<Contas_bancarias> Search(string searchTerm, int tipo)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", searchTerm);
            rh.AddParameter("tipo", tipo);
            rh.Send("cntb-search");

            return EntityLoader<List<Contas_bancarias>>.Load(rh.Result);
        }
    }
}
