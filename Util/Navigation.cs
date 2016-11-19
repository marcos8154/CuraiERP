using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace EM3.Util
{
    public class Navigation
    {
        public static void AddTabItem(TabControl tabControl, UserControl controlToAdd, string title)
        {
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
