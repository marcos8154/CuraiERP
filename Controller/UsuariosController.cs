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
                return true;
            }
            else
            {
                if (rh.Result.status == 100)
                    return false;
                new MsgAlerta(rh.Result.message);
            }
            return false;
        }

        public static void Salvar(Usuarios usuario)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("nome", usuario.Nome);
            rh.AddParameter("senha", usuario.Senha);
            rh.AddParameter("admin", usuario.Admin.ToString());
            rh.AddParameter("ativo", usuario.Ativo.ToString());


        }
    }
}

