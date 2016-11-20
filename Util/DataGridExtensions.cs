﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EM3.Extensions
{
    public static class Datagrid
    {
        public static void AplicarPadroes(this DataGrid dt, bool isRead = true)
        {
            dt.AutoGenerateColumns = false;
            dt.IsReadOnly = isRead;
            dt.CanUserAddRows = !isRead;
            dt.CanUserDeleteRows = !isRead;
            dt.CanUserResizeRows = false;
            dt.SelectionMode = DataGridSelectionMode.Single;
            dt.SelectionUnit = DataGridSelectionUnit.FullRow;
            dt.AlternatingRowBackground = Brushes.Lavender;
            dt.FontSize = 18;
            dt.Cursor = Cursors.Hand;
            dt.HorizontalGridLinesBrush = Brushes.LightGray;
            dt.VerticalGridLinesBrush = Brushes.LightGray;
        }
    }
}
