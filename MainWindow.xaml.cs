using EM3.Controller;
using EM3.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace EM3
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Closed += MainWindow_Closed;

            if(Configuration.LoadFromLocalSettings())
            {
                this.Hide();
                if (!LicenceController.Connect())
                {
                    MessageBox.Show("Não foi possível conectar com o servidor de licenças. \nO sistema será encerrado.", "Licence Server não localizado", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Login login = new Login();
                login.ShowDialog();
            }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btEncerrar_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btConectar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Startup();
            }
            catch (Exception ex)
            {

            }
        }

        private void Startup()
        {
            this.Hide();
            if (!LicenceController.Connect())
            {
                MessageBox.Show("Não foi possível conectar com o servidor de licenças. \nO sistema será encerrado.", "Licence Server não localizado", MessageBoxButton.OK, MessageBoxImage.Error);
                System.Environment.Exit(0);
            }

            LicenceController.server = txServidor.Text;
            Configuration.application = txApp.Text;
            Configuration.server = txServidor.Text;
            Configuration.port = int.Parse(txPorta.Text);

            Login login = new Login();
            login.ShowDialog();
        }
    }
}
