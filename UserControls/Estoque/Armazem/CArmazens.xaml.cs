using EM3.Controller;
using EM3.Windows.Selecao;
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

namespace EM3.UserControls.Estoque.Armazem
{
    /// <summary>
    /// Interação lógica para CArmazens.xam
    /// </summary>
    public partial class CArmazens : UserControl
    {
        public delegate void Complete();
        public event Complete OnComplete;

        private Armazens Armazem;

        public CArmazens()
        {
            InitializeComponent();
        }

        private void btSalvar_OnClick()
        {
            Salvar(true);
        }

        private void Salvar(bool close)
        {
            if (Armazem == null) Armazem = new Armazens();

            Armazem.Id = int.Parse(txCodigo.Text);
            Armazem.Nome = txNome.Text;
            Armazem.Tipo_armazem = cbTipo.SelectedIndex;
            Armazem.Empresa_id = txCod_empresa.Value;

            if (ArmazensController.Save(Armazem))
            {
                if (close)
                    Close();
                else
                    LimparCampos();
            }
        }

        private void LimparCampos()
        {
            txCodigo.Text = "0";
            txNome.Text = string.Empty;
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

        private void txCod_empresa_CallSearch()
        {
            SelecionarEmpresa se = new SelecionarEmpresa();
            se.ShowDialog();

            txCod_empresa.Text = se.Selecionado.Id.ToString();
        }

        internal void Load(int id)
        {
            Armazem = ArmazensController.Find(id);

            txCodigo.Text = Armazem.Id.ToString();
            txNome.Text = Armazem.Nome;
            cbTipo.SelectedIndex = Armazem.Tipo_armazem;
            txCod_empresa.Text = Armazem.Empresa_id.ToString();

            cabecalho.Title = "Alterar armazém (" + Armazem.Nome + ")";
        }
    }
}
