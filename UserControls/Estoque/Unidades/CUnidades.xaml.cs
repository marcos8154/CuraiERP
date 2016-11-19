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

namespace EM3.UserControls.Estoque.UnidadesModulo
{
    /// <summary>
    /// Interação lógica para CUnidades.xam
    /// </summary>
    public partial class CUnidades : UserControl
    {
        public delegate void Complete();
        public event Complete OnComplete;

        private Unidades Unidade = new Unidades();

        public CUnidades()
        {
            InitializeComponent();
        }

        public void Load(int id)
        {
            Unidade = UnidadesController.Find(id);

            txCod.Text = Unidade.Id.ToString();
            txSigla.Text = Unidade.Sigla;
            txDescricao.Text = Unidade.Descricao;
        }

        private void btSalvarEContinuar_OnClick()
        {
            Salvar(false);
        }

        private void LimparCampos()
        {
            txCod.Text = "0";
            txDescricao.Text = string.Empty;
            txSigla.Text = string.Empty;
        }

        private void Salvar(bool close)
        {
            Unidade.Sigla = txSigla.Text;
            Unidade.Descricao = txDescricao.Text;

            if (UnidadesController.Save(Unidade))
            {
                if (close)
                    Close();
                else
                    LimparCampos();
            }
        }

        private void Close()
        {
            if (OnComplete != null) OnComplete();
        }

        private void btCancelar_OnClick()
        {
            Close();
        }

        private void btSalvar_OnClick()
        {
            Salvar(true);
        }
    }
}
