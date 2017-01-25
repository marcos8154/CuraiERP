using EM3.Controller;
using EM3.Model;
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

namespace EM3.UserControls.Estoquev.Marca
{
    /// <summary>
    /// Interação lógica para CMarcas.xam
    /// </summary>
    public partial class CMarcas : UserControl
    {
        public delegate void Complete();
        public event Complete OnComplete;

        private Marcas marca;
        public CMarcas()
        {
            InitializeComponent();
        }

        public void Load(int id)
        {
            marca = MarcasController.Find(id);
            txCod.Text = marca.Id.ToString();
            txNome.Text = marca.Nome;
            foto.LoadImage(FotoController.GetFile("marca", marca.Foto_id));
            txNome.SetFocused();
        }

        private void btCancelar_OnClick()
        {
            Fechar();
        }

        private void btSalvarEContinuar_OnClick()
        {
            Salvar(false);
        }

        private void btSalvar_OnClick()
        {
            Salvar(true);
        }

        private void Salvar(bool close)
        {
            if (marca == null)
                marca = new Marcas();

            marca.Id = txCod.GetInt;
            marca.Nome = txNome.Text;
            marca.Foto_id = FotoController.Save(foto.FileName, marca.Foto_id);

            if (MarcasController.Save(marca))
            {
                if (close)
                    Fechar();
                else
                    LimparCampos();
            }
        }

        private void LimparCampos()
        {
            txCod.Text = "0";
            txNome.Text = string.Empty;
            foto.Reset();
            txNome.SetFocused();
        }

        private void Fechar()
        {
            if (OnComplete != null) OnComplete();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            txNome.SetFocused();
        }
    }
}
