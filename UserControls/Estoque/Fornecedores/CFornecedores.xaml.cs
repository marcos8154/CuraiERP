using EM3.Controller;
using EM3.Extensions;
using EM3.Windows;
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

namespace EM3.UserControls.Estoque.FornecedoresModulo
{
    /// <summary>
    /// Interação lógica para CFornecedores.xam
    /// </summary>
    public partial class CFornecedores : UserControl
    {
        public delegate void Complete();
        public event Complete OnComplete;

        List<Contatos_fornecedores> contatos = new List<Contatos_fornecedores>();

        public CFornecedores()
        {
            InitializeComponent();

            dataGrid.AplicarPadroes(false);
            dataGrid.ItemsSource = contatos;
        }

        private void btSalvar_OnClick()
        {
            Salvar(true);
        }

        private void Salvar(bool close)
        {
            if (!ValidContatos())
                return;

            if (close)
                Close();
            else
                LimparCampos();
        }

        private bool ValidContatos()
        {
            foreach (Contatos_fornecedores cf in contatos)
            {
                int linha = contatos.IndexOf(cf) + 1;
                if (cf.Filial == 0 || cf.Filial > 9999)
                {
                    new MsgAlerta("Erro de validação em contatos \n\nLinha: " + linha + "\nFilial não pode ser 0 ou maior que 9999");
                    return false;
                }

                if (string.IsNullOrEmpty(cf.Pessoa_contato))
                {
                    new MsgAlerta("Erro de validação em contatos \n\nLinha: " + linha + "\nNome da pessoa não pode estar em branco ou possuir mais de 100 caracteres");
                    return false;
                }

                if (cf.Pessoa_contato.Length > 100)
                {
                    new MsgAlerta("Erro de validação em contatos \n\nLinha: " + linha + "\nNome da pessoa não pode estar em branco ou possuir mais de 100 caracteres");
                    return false;
                }

                if (string.IsNullOrEmpty(cf.Telefone))
                {
                    new MsgAlerta("Erro de validação em contatos \n\nLinha: " + linha + "\nTelefone não pode estar em branco ou possuir mais de 20 caracteres");
                    return false;
                }

                if (cf.Telefone.Length > 20)
                {
                    new MsgAlerta("Erro de validação em contatos \n\nLinha: " + linha + "\nTelefone não pode estar em branco ou possuir mais de 20 caracteres");
                    return false;
                }

                if (!string.IsNullOrEmpty(cf.Setor))
                {
                    if (cf.Setor.Length > 50)
                    {
                        new MsgAlerta("Erro de validação em contatos \n\nLinha: " + linha + "\nSetor não pode possuir mais de 50 caracteres");
                        return false;
                    }
                }
            }

            return true;
        }

        private void LimparCampos()
        {

        }

        private void Close()
        {
            if (OnComplete != null) OnComplete();
        }

        private void btSalvarEContinuar_OnClick()
        {
            Salvar(false);
        }

        private void btCancelar_OnClick()
        {
            Close();
        }
    }
}
