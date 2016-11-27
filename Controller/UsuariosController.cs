﻿using EM3.Enums;
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
                return LicenceController.Autorize(UsuarioAtual.Id);
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
            return UsuarioAtual;
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

        public static void Salvar(Usuarios usuario)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("nome", usuario.Nome);
            rh.AddParameter("senha", usuario.Senha);
            rh.AddParameter("admin", usuario.Admin.ToString());
            rh.AddParameter("ativo", usuario.Ativo.ToString());
        }

        public static List<Usuarios> Search(string query)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", query);
            rh.Send("usr-search");

            List<Usuarios> result = EntityLoader<List<Usuarios>>.Load(rh.Result);
            return result;
        }
    }
}

