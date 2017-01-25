using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EM3.Windows
{
    /// <summary>
    /// Lógica interna para Help.xaml
    /// </summary>
    public partial class Help : Window
    {
        public Help(string helpName)
        {
            InitializeComponent();
            ExecuteHelp(helpName);
        }

        private void ExecuteHelp(string fileHelpName)
        {
            if (string.IsNullOrEmpty(fileHelpName))
                return;

            if (fileHelpName.EndsWith(".chelp"))
                throw new Exception($"[{fileHelpName}]: Não é permitido informar a extenção do arquivo de help");

            string file = Directory.GetCurrentDirectory() + $@"\Files\Help\{fileHelpName}.chelp";
            if (!File.Exists(file))
            {
                MsgAlerta.Show($"[{fileHelpName}.chelp]: Arquivo de help não localizado");
                return;
            }

            string contentHelp = File.ReadAllText(file);
            textBox.Text = contentHelp;
            ShowDialog();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
