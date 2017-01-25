using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class EstoqueController
    {
        public static bool Save(Estoque estoque)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", estoque.Id);
            rh.AddParameter("local_estoque_id", estoque.Local_estoque_id);
            rh.AddParameter("local_padrao", estoque.Local_padrao);
            rh.AddParameter("produto_id", estoque.Produto_id);
            rh.AddParameter("quant", estoque.Quant);
            rh.Send("est-save");

            return rh.HasSuccess;
        }

        public static Estoque GetEstoquePadrao(int id_produto)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("produto_id", id_produto);
            rh.Send("est-padrao");

            return EntityLoader<Estoque>.Load(rh.Result) ?? new Estoque();
        }

        public static void Remove(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("est-rem");
        }
    }
}
