using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class PermissoesController
    {
        public static List<Permissoes> ListByGrupo(int grupo_id)
        {
            List<Permissoes> result = new List<Permissoes>();

            RequestHelper rh = new RequestHelper();
            rh.AddParameter("grupo_id", grupo_id);
            rh.Send("perms-listbygrupo");

            if (rh.HasSuccess)
                result = EntityLoader<List<Permissoes>>.Load(rh.Result);
            return result;
        }

        public static bool Clear(int grupo_usuarios_id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("grupo_id", grupo_usuarios_id);
            rh.Send("perms-clear");

            return rh.HasSuccess;
        }

        internal static bool Add(Permissoes permissao)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("grupo_usuarios_id", permissao.Grupo_usuarios_id);
            rh.AddParameter("telas_id", permissao.Telas_id);
            rh.AddParameter("acesso", permissao.Acesso);
            rh.AddParameter("inserir", permissao.Inserir);
            rh.AddParameter("atualizar", permissao.Atualizar);
            rh.AddParameter("excluir", permissao.Excluir);
            rh.Send("perms-add");

            return rh.HasSuccess;
        }
    }
}
