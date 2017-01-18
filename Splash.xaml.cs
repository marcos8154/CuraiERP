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
using System.Windows.Shapes;

namespace EM3
{
    /// <summary>
    /// Lógica interna para Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public Splash()
        {
            InitializeComponent();
            Configuration.LoadFromLocalSettings();
            if(Configuration.quiet_mode)
            {
                dispatcherTimer.Stop();
                MainWindow main = new MainWindow();
                this.Close();
                GC.Collect();
            }
        }

        int secconds = 0;

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            secconds++;
            if (secconds == 3)
            {
                dispatcherTimer.Stop();
                MainWindow main = new MainWindow();
                this.Close();
                GC.Collect();
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
    }
}
