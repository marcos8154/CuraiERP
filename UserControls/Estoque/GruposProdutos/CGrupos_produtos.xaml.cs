using EM3.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EM3.UserControls.Estoquev.GruposProdutos
{
    /// <summary>
    /// Interação lógica para CGrupos_produtos.xam
    /// </summary>
    public partial class CGrupos_produtos : System.Windows.Controls.UserControl
    {
        public delegate void Complete();
        public event Complete OnComplete;

        private Grupos_produtos Grupo_produtos;

        public CGrupos_produtos()
        {
            InitializeComponent();
        }

        private void btSalvar_OnClick()
        {
            Salvar(true);
        }

        private void Salvar(bool close)
        {
            if (Grupo_produtos == null) Grupo_produtos = new Grupos_produtos();

            Grupo_produtos.Id = int.Parse(txCod.Text);
            Grupo_produtos.Descricao = txNome.Text;
            Grupo_produtos.Inativo = !(cbInativo.SelectedIndex == 0);

            if (!string.IsNullOrEmpty(txCaminhoImg.Text))
            {
                int i = FotoController.Save(txCaminhoImg.Text, Grupo_produtos.Foto_id);
                Grupo_produtos.Foto_id = i;
            }

            if (Grupos_produtosController.Save(Grupo_produtos))
            {
                if (close)
                    Close();
                else
                    LimparCampos();
            }
        }

        private void LimparCampos()
        {
            Grupo_produtos = new Grupos_produtos();
            txCod.Text = "0";
            txNome.Text = string.Empty;
            image.Source = null;
            txCaminhoImg.Text = string.Empty;
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

        private void btSelecionarImg_OnClick()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Arquivos de imagem (*.jpg)|*.jpg|Arquivos de imagem (*.png)|*.png";
            ofd.ShowDialog();
            LoadImage(ofd.FileName);
        }

        private void LoadImage(string fileName)
        {
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    BitmapImage src = new BitmapImage();
                    src.BeginInit();
                    src.UriSource = new Uri(fileName, UriKind.Relative);
                    src.CacheOption = BitmapCacheOption.OnLoad;
                    src.EndInit();

                    image.Source = src;
                    txCaminhoImg.Text = fileName;
                }
            }
            catch { }
        }

        public void Load(int id)
        {
            Grupo_produtos = Grupos_produtosController.Find(id);

            txCod.Text = Grupo_produtos.Id.ToString();
            txNome.Text = Grupo_produtos.Descricao;
            cbInativo.SelectedIndex = (Grupo_produtos.Inativo ? 1 : 0);

            if (Grupo_produtos.Foto_id > 0)
            {
                string file = FotoController.GetFile("gprod", Grupo_produtos.Foto_id);
                LoadImage(file);
            }
        }
    }
}
