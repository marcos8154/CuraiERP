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
    /// Interação lógica para ComboBox.xam
    /// </summary>
    public partial class ComboBox : UserControl
    {
        public int Index
        {
            get
            {
                return combobox.TabIndex;
            }
            set
            {
                combobox.TabIndex = value;
            }
        }

        public string Title
        {
            get
            {
                return lbTitle.Content.ToString();
            }
            set
            {
                lbTitle.Content = value;
            }
        }

        public ComboBox()
        {
            InitializeComponent();
            this.MinHeight = 56;
        }

        public string Items
        {
            set
            {
                string[] items = value.Split(';');
                foreach (string str in items)
                {
                    if (string.IsNullOrWhiteSpace(str))
                        continue;
                    combobox.Items.Add(str);
                }

                if (combobox.Items.Count > 0)
                {
                    combobox.SelectedIndex = 0;
                    combobox.Text = combobox.Items[0].ToString();
                }
            }
        }

        public void SetItemsSource(System.Collections.IEnumerable item)
        {
            combobox.ItemsSource = item;
        }

        public System.Collections.IEnumerable GetItemsSource()
        {
            return combobox.Items;
        }

        public void AddItem(object item)
        {
            combobox.Items.Add(item);
        }

        public object SelectedItem
        {
            get
            {
                return combobox.SelectedItem;
            }
        }

        public int SelectedIndex
        {
            get
            {
                return combobox.SelectedIndex;
            }
            set
            {
                combobox.SelectedIndex = value;
            }
        }

        public object SelectedValue
        {
            get
            {
                return combobox.SelectedValue;
            }
        }

    }
}
