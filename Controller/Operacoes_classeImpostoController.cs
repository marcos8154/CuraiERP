using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class Operacoes_classeImpostoController
    {
        public static bool Save(Operacoes_classe_imposto operacao)
        {
            RequestHelper rh = new RequestHelper();

            rh.AddParameter("id", operacao.Id);
            rh.AddParameter("icms_cst", operacao.Icms_cst);
            rh.AddParameter("icms_perc", operacao.Icms_perc);
            rh.AddParameter("icms_perc_st", operacao.Icms_perc_st);
            rh.AddParameter("icms_mva", operacao.Icms_mva);
            rh.AddParameter("impressora_fiscal_tipo", 0);
            rh.AddParameter("impressora_fiscal_perc", 0);
            rh.AddParameter("pis_cofins_tipo", operacao.Pis_cofins_tipo);
            rh.AddParameter("pis_perc", operacao.Pis_perc);
            rh.AddParameter("pis_perc_st", operacao.Pis_perc_st);
            rh.AddParameter("cofins_perc", operacao.Cofins_perc);
            rh.AddParameter("cofins_perc_st", operacao.Cofins_perc_st);
            rh.AddParameter("perc_credito_icms", 0);
            rh.AddParameter("classe_imposto_id", operacao.Classe_imposto_id);
            rh.AddParameter("tipos_movimento_id", operacao.Tipos_movimento_id);
            rh.AddParameter("cfop_id", operacao.Cfop_id);
            rh.AddParameter("uf", operacao.Uf);

            rh.Send("oclimp-save");

            return rh.HasSuccess;
        }

        public static List<Operacoes_classe_imposto> ListAll(int classe_imposto_id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("classe_imposto_id", classe_imposto_id);
            rh.Send("oclimp-listall");

            return EntityLoader<List<Operacoes_classe_imposto>>.Load(rh.Result);
        }

        public static Operacoes_classe_imposto Find(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("oclimp-find");

            if (rh.HasSuccess)
                return EntityLoader<Operacoes_classe_imposto>.Load(rh.Result);

            return new Operacoes_classe_imposto();
        }

        public static bool Remove(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("oclimp-rem");

            return rh.HasSuccess;
        }
    }
}
