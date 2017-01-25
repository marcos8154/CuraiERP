using EM3.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class Tabelas_precoController
    {
        public static int Save(Tabelas_precos tabela)
        {
            if(tabela.Data_inicio.EndsWith("0001"))
            {
                MsgAlerta.Show("Data de início inválida");
                return 0;
            }

            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", tabela.Id);
            rh.AddParameter("nome", tabela.Nome);
            rh.AddParameter("data_inicio", tabela.Data_inicio);
            rh.AddParameter("data_inativacao", tabela.Data_inativacao);
            rh.AddParameter("forma_pagamento_id", tabela.Forma_pagamento_id);
            rh.AddParameter("inativo", tabela.Inativo);
            rh.Send("tprc-save");

            return (rh.HasSuccess
                ? EntityLoader<int>.Load(rh.Result)
                : 0);
        }

        public static List<Tabelas_precos> Search(string searchTerm, int tipo)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", searchTerm);
            rh.AddParameter("tipo", tipo);
            rh.Send("tprc-search");

            return EntityLoader<List<Tabelas_precos>>.Load(rh.Result);
        }

        public static bool Remove(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("tprc-rem");

            return rh.HasSuccess;
        }

        public static Tabelas_precos Find(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("tprc-find");

            return (rh.HasSuccess
                ? EntityLoader<Tabelas_precos>.Load(rh.Result)
                : new Tabelas_precos());
        }
    }
}
