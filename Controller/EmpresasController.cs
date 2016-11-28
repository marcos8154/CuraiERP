using EM3.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Controller
{
    public class EmpresasController
    {
        public static bool Save(Empresa emp)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", emp.Id);
            rh.AddParameter("nome_fantasia", emp.Nome_fantasia);
            rh.AddParameter("razao_social", emp.Razao_social);
            rh.AddParameter("cnpj", emp.Cnpj);
            rh.AddParameter("crt", emp.Crt);
            rh.AddParameter("telefone1", emp.Telefone1);
            rh.AddParameter("telefone2", emp.Telefone2);
            rh.AddParameter("celular", emp.Celular);
            rh.AddParameter("email", emp.Email);
            rh.AddParameter("responsavel", emp.Responsavel);
            rh.AddParameter("ativo", emp.Ativo);
            rh.AddParameter("enderecos.id", emp.Enderecos.Id);
            rh.AddParameter("enderecos.logradouro", emp.Enderecos.Logradouro);
            rh.AddParameter("enderecos.cep", emp.Enderecos.Cep);
            rh.AddParameter("enderecos.bairro", emp.Enderecos.Bairro);
            rh.AddParameter("enderecos.municipio", emp.Enderecos.Municipio);
            rh.AddParameter("enderecos.uf", emp.Enderecos.Uf);
            rh.AddParameter("enderecos.numero", emp.Enderecos.Numero);
            rh.AddParameter("enderecos.pais", emp.Enderecos.Pais);
            rh.AddParameter("enderecos.complemento", emp.Enderecos.Complemento);
            rh.Send("emp-save");

            return rh.HasSuccess;
        }

        public static List<Empresa> Search(string term)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("query", term);
            rh.Send("emp-search");

            List<Empresa> result = EntityLoader<List<Empresa>>.Load(rh.Result) ?? new List<Empresa>();
            return result;
        }

        public static Empresa Find(int id)
        {
            RequestHelper rh = new RequestHelper();
            rh.AddParameter("id", id.ToString());
            rh.Send("emp-find");

            return EntityLoader<Empresa>.Load(rh.Result) ??new Empresa();
        }
    }
}
