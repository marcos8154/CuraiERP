using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class Formas_pagamentoController
    {
        public static bool Save(Formas_pagamento f)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", f.Id);
            rh.AddParameter("descricao", f.Descricao);
            rh.AddParameter("tipo_pagamento", f.Tipo_pagamento);
            rh.AddParameter("conta_bancaria_id", f.Conta_bancaria_id);
            rh.AddParameter("operadora_cartao_id", f.Operadora_cartao_id);
            rh.AddParameter("permite_entrada", f.Permite_entrada);
            rh.AddParameter("tipo_intervalo", f.Tipo_intervalo);
            rh.AddParameter("intervalo", f.Intervalo);
            rh.AddParameter("dia_base", f.Dia_base);
            rh.AddParameter("tolerancia_dias", f.Tolerancia_dias);
            rh.AddParameter("juros_atraso", f.Juros_atraso);
            rh.AddParameter("parcelas", f.Parcelas);
            rh.AddParameter("inativo", f.Inativo);
            rh.Send("fpg-save");

            return rh.HasSuccess;
        }

        public static bool Remove(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("fpg-rem");

            return rh.HasSuccess;
        }

        public static Formas_pagamento Find(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("fpg-find");

            return (rh.HasSuccess
                ? EntityLoader<Formas_pagamento>.Load(rh.Result)
                : new Formas_pagamento());
        }

        public static List<Formas_pagamento> Search(string query, int tipo)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", query);
            rh.AddParameter("tipo", tipo);
            rh.Send("fpg-search");

            List<Formas_pagamento> result = EntityLoader<List<Formas_pagamento>>.Load(rh.Result);
            return result;
        }
    }
}
