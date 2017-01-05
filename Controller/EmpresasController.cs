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
            rh.AddParameter("tipo", emp.Tipo);
            rh.AddParameter("tipo_ie", emp.Tipo_ie);
            rh.AddParameter("inscr_estadual", emp.Inscr_estadual);
            rh.AddParameter("inscr_municipal", emp.Inscr_municipal);
            rh.AddParameter("optante_simples", emp.Optante_simples);
            rh.AddParameter("nfe_cert_serie", emp.Nfe_cert_serie);
            rh.AddParameter("nfe_serie", emp.Nfe_serie);
            rh.AddParameter("nfe_modelo", emp.Nfe_modelo);
            rh.AddParameter("nfe_ambiente", emp.Nfe_ambiente);
            rh.AddParameter("nfce_serie", emp.Nfce_serie);
            rh.AddParameter("nfce_modelo", emp.Nfce_modelo);
            rh.AddParameter("nfce_ambiente", emp.Nfce_ambiente);
            rh.AddParameter("nfce_token", emp.Nfce_token);
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

        public static void CriarEmpresaTeste()
        {
            Empresa e = new Empresa();

            e.Razao_social = "Empresa Teste";
            e.Nome_fantasia = "Empresa teste";
            e.Cnpj = "00.000.000/0000-00";
            e.Crt = 1;
            e.Ativo = false;
            e.Enderecos = new Enderecos()
            {
                Logradouro = "Rua 0",
                Bairro = "Bairro 0",
                Cep = "00.000-000",
                Complemento = "",
                Municipio = "Rio de Janeiro",
                Numero = 100,
                Pais = "Brasil",
                Uf = "RJ"
            };

            e.Tipo = 0;
            e.Responsavel = "Responsável";
            e.Telefone1 = "0000-0000";
            e.Telefone2 = "0000-0000";
            e.Celular = "00000-0000";
            e.Optante_simples = true;

            Save(e);
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

            if (rh.HasSuccess)
            {
                if(rh.Result.message.Equals("no_tables"))
                {
                    Configuration.Setup();
                    Find(id);
                }
                return EntityLoader<Empresa>.Load(rh.Result);
            }
            return new Empresa();
        }
    }
}
