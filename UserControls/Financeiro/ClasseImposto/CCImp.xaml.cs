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

namespace EM3.UserControls.Financeiro.ClasseImposto
{
    /// <summary>
    /// Interação lógica para CCImp.xam
    /// </summary>
    public partial class CCImp : UserControl
    {
        public delegate void Complete();
        public event Complete OnComplete;

        Classes_imposto Classe_imp = new Classes_imposto();

        public CCImp()
        {
            InitializeComponent();
        }

        private void btCancelar_OnClick()
        {
            Close();
        }

        private void btSalvar_OnClick()
        {
            Salvar(true);
        }

        private void btSalvarEContinuar_OnClick()
        {
            Salvar(false);
        }

        private void Salvar(bool close)
        {
            if (Classe_imp == null) Classe_imp = new Classes_imposto();

            Classe_imp.Id = int.Parse(txCod.Text);
            Classe_imp.Nome = txNome.Text;
            Classe_imp.Data_alteracao = Commons.ServerDate.ToShortDateString();

            if(Classes_impostoController.Save(Classe_imp))
            {
                if (close)
                    Close();
                else
                    LimparCampos();
            }
        }

        private void LimparCampos()
        {
            txCod.Text = "0";
            txNome.Text = string.Empty;

            Classe_imp = new Classes_imposto();
        }

        private void Close()
        {
            if (OnComplete != null) OnComplete();
        }

        public void Load(int id)
        {
            Classe_imp = Classes_impostoController.Find(id);

            txCod.Text = Classe_imp.Id.ToString();
            txNome.Text = Classe_imp.Nome;

            cabecalho.Title = "Alterar classe de imposto (" + Classe_imp.Nome + ")";
        }
    }
}
