using EM3.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EM3.UserControls.Configuracoes.Empresas
{
    /// <summary>
    /// Interação lógica para CEmpresas.xam
    /// </summary>
    public partial class CEmpresas : UserControl
    {
        public delegate void Complete();
        public event Complete OnComplete;

        private Empresa empresa = new Empresa();

        public CEmpresas()
        {
            InitializeComponent();
            txNome_fantasia.SetFocused();
        }

        public void Load(int id)
        {
            Empresa e = EmpresasController.Find(id);

            txCod.Text = e.Id.ToString();
            txNome_fantasia.Text = e.Nome_fantasia;
            txRazao_social.Text = e.Razao_social;
            txEmail.Text = e.Email;
            txTel1.Text = e.Telefone1;
            txTel2.Text = e.Telefone2;
            txCel.Text = e.Celular;
            cbAtivo.SelectedIndex = (e.Ativo ? 0 : 1);
            txCNPJ.Text = e.Cnpj;
            txCep.Text = e.Enderecos.Cep;
            txLogradouro.Text = e.Enderecos.Logradouro;
            txUF.Text = e.Enderecos.Uf;
            txBairro.Text = e.Enderecos.Bairro;
            txMunicipio.Text = e.Enderecos.Municipio;
            txNumero.Text = e.Enderecos.Numero.ToString();
            txCompl.Text = e.Enderecos.Complemento;
            txResponsavel.Text = e.Responsavel;

            cabecalho.Title = "Alterar empresa (" + e.Razao_social + ")";
            this.empresa = e;
        }

        private void btSalvar_OnClick()
        {
            Salvar(false);
        }

        public void Close()
        {
            if (OnComplete != null) OnComplete();
        }

        private void btCancelar_OnClick()
        {
            Close();
        }

        private void Salvar(bool continuar)
        {
            empresa.Nome_fantasia = txNome_fantasia.Text;
            empresa.Razao_social = txRazao_social.Text;
            empresa.Telefone1 = txTel1.Text;
            empresa.Telefone2 = txTel2.Text;
            empresa.Celular = txCel.Text;
            empresa.Cnpj = txCNPJ.Text;
            empresa.Email = txEmail.Text;
            empresa.Crt = (cbCRT.SelectedIndex + 1);
            empresa.Ativo = (cbAtivo.SelectedIndex == 0 ? true : false);
            empresa.Responsavel = txResponsavel.Text;

            if (empresa.Enderecos == null)
                empresa.Enderecos = new Enderecos();

            empresa.Enderecos.Logradouro = txLogradouro.Text;
            empresa.Enderecos.Bairro = txBairro.Text;
            empresa.Enderecos.Municipio = txMunicipio.Text;
            empresa.Enderecos.Numero = int.Parse(txNumero.Text);
            empresa.Enderecos.Pais = "Brasil";
            empresa.Enderecos.Uf = txUF.Text;
            empresa.Enderecos.Cep = txCep.Text;
            empresa.Enderecos.Complemento = txCompl.Text;

            if (EmpresasController.Save(empresa))
            {
                LimparCampos();
                if (!continuar)
                    Close();
            }
        }

        private void LimparCampos()
        {
            txCod.Text = "0";
            txNome_fantasia.Text = string.Empty;
            txRazao_social.Text = string.Empty;
            txCNPJ.Text = string.Empty;
            txTel1.Text = string.Empty;
            txTel2.Text = string.Empty;
            txCel.Text = string.Empty;
            txEmail.Text = string.Empty;
            txResponsavel.Text = string.Empty;
            txCep.Text = string.Empty;
            txLogradouro.Text = string.Empty;
            txBairro.Text = string.Empty;
            txMunicipio.Text = string.Empty;
            txNumero.Text = "0";
            txUF.Text = string.Empty;
            txCompl.Text = string.Empty;

            txNome_fantasia.SetFocused();
        }

        private void btContinuar_OnClick()
        {
            Salvar(true);
        }
    }
}
