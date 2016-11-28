using EM3.Enums;
using EM3.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class UsuariosController
    {
        public static Usuarios UsuarioAtual { get; set; }

        public static DateTime DataBase { get; set; }

        public static int Empresa { get; set; }

        public static bool EfetuaLogin(string nome, string senha, DateTime data, int empresa)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("nome", nome);
            rh.AddParameter("senha", senha);
            rh.Send("usr-login");

            if (rh.Result.status == (int)StatusRetorno.OPERACAO_OK)
            {
                UsuarioAtual = EntityLoader<Usuarios>.Load(rh.Result) ?? new Usuarios();

                DataBase = data;
                Empresa = empresa;
                return LicenceController.Authorize(UsuarioAtual.Id);
            }
            else
            {
                if (rh.Result.status == 100)
                    return false;
                new MsgAlerta(rh.Result.message);
            }
            return false;
        }

        public static Usuarios Find(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("usr-get");

            return EntityLoader<Usuarios>.Load(rh.Result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo">0 - Todos; 1 - Somente Ativos; 2 - Somente inativos;</param>
        /// <returns></returns>
        internal static int GetCount(int tipo)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("tipo", tipo);
            rh.Send("usr-count");

            return EntityLoader<int>.Load(rh.Result);
        }

        public static bool ValidaPermissao(string tela_id, TipoPermissao permissao)
        {
            if (UsuarioAtual.Admin) return true;

            RequestHelper rh = new RequestHelper();
            rh.AddParameter("tela", tela_id);
            rh.AddParameter("usuario", UsuarioAtual.Id);
            rh.AddParameter("tipo_permissao", (int)permissao);
            rh.Send("usr-validperm");

            int i = EntityLoader<int>.Load(rh.Result);
            return (i == 1);
        }

        public static Usuarios Save(Usuarios usuario)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", usuario.Id);
            rh.AddParameter("nome", usuario.Nome);
            rh.AddParameter("senha", usuario.Senha);
            rh.AddParameter("admin", usuario.Admin.ToString());
            rh.AddParameter("ativo", usuario.Ativo.ToString());
            rh.AddParameter("grupo_usuarios_id", usuario.Grupo_usuarios_id);
            rh.Send("usr-save");

            if (rh.HasSuccess)
                return EntityLoader<Usuarios>.Load(rh.Result);
            else
                return null;
        }

        public static bool Delete(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id);
            rh.Send("usr-rem");

            if(rh.HasSuccess)
            {
                LicenceController.RemoveUser(id);
                return true;
            }
            return false;
        }

        public static List<Usuarios> Search(string query, int tipo = 0)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", query);
            rh.AddParameter("tipo", tipo);
            rh.Send("usr-search");

            List<Usuarios> result = EntityLoader<List<Usuarios>>.Load(rh.Result);
            return result;
        }
    }
}

