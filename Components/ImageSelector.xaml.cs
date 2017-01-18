using Microsoft.Win32;
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

namespace EM3.Components
{
    /// <summary>
    /// Interação lógica para ImageSelector.xam
    /// </summary>
    public partial class ImageSelector : UserControl
    {
        public ImageSelector()
        {
            InitializeComponent();
        }

        private void btSelecionarImg_OnClick()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Arquivos de imagem (*.jpg)|*.jpg|Arquivos de imagem (*.png)|*.png";
            ofd.ShowDialog();
            LoadImage(ofd.FileName);
        }

        public string FileName
        {
            get
            {
                return txCaminhoImg.Text;
            }
        }

        public void Reset()
        {
            txCaminhoImg.Text = string.Empty;
            image.Source = null;
        }
        
        public void LoadImage(string fileName)
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
    }
}
