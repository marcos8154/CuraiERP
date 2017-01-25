using EM3.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace EM3.Util
{
    public class Navigation
    {
        public static void AddTabItem(TabControl tabControl, UserControl controlToAdd, string title)
        {
            if (Configuration.nav_mode == 1) // Windows
            {
                Window window = new Window();
                window.Width = 1024;
                window.Height = 600;
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Content = controlToAdd;
                window.Title = title;

                window.Show();
                return;
            }

            TabItem item = new TabItem();
            item.Header = title;
            item.Content = controlToAdd;

            controlToAdd.Height = item.Height;
            controlToAdd.Width = item.Width;
            tabControl.Items.Add(item);
            tabControl.SelectedItem = item;
        }
    }
}
