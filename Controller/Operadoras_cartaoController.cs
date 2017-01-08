using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class Operadoras_cartaoController
    {
        public static bool Save(Operadoras_cartao o)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", o.Id);
            rh.AddParameter("nome", o.Nome);
            rh.AddParameter("tipo", o.Tipo);
            rh.AddParameter("tipo_recebimento", o.Tipo_recebimento);
            rh.AddParameter("prazo_recebimento", o.Prazo_recebimento);
            rh.AddParameter("taxa", o.Taxa);
            rh.AddParameter("inativo", o.Inativo);
            rh.Send("opct-save");

            return rh.HasSuccess;
        }

        public static bool Remove(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("opct-rem");

            return rh.HasSuccess;
        }

        public static List<Operadoras_cartao> Search(string searchTerm)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", searchTerm);
            rh.Send("opct-search");

            return EntityLoader<List<Operadoras_cartao>>.Load(rh.Result);
        }

        public static Operadoras_cartao Find(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("opct-find");

            return EntityLoader<Operadoras_cartao>.Load(rh.Result);
        }
    }
}
