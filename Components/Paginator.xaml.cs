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
    /// Interação lógica para Paginator.xam
    /// </summary>
    public partial class Paginator : UserControl
    {
        public delegate void ChangeEvent(int page);
        public event ChangeEvent OnPageChange;

        private int currentPage = 0;
        public int MaxPages { get; set; }
        public int IntervalChangeNumber { get; set; }

        public int CurrentPage
        {
            get
            {
                return currentPage;
            }
        }

        public Paginator()
        {
            InitializeComponent();
            txPageNumber.Text = "0";
        }

        public void SetPageNumber(int pageNumber)
        {
            txPageNumber.Text = pageNumber.ToString();
            currentPage = 0;
        }

        private void btPrevious_OnClick()
        {
            int pageNumber = txPageNumber.GetInt;
            if ((pageNumber - 1) < 0)
                return;

            currentPage = IntervalChangeNumber * (pageNumber - 1);
            txPageNumber.Text = (pageNumber - 1).ToString();

            if (OnPageChange != null) OnPageChange(CurrentPage);
        }

        private void btNext_OnClick()
        {
            int pageNumber = txPageNumber.GetInt;
            if ((pageNumber + 1) > MaxPages)
                return;

            currentPage = IntervalChangeNumber * (pageNumber + 1);
            txPageNumber.Text = (pageNumber + 1).ToString();

            if (OnPageChange != null) OnPageChange(CurrentPage);
        }

        private void txPageNumber_InputKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                int pageNumber = txPageNumber.GetInt;
                if ((pageNumber) < 0)
                    return;
                if ((pageNumber) > MaxPages)
                {
                    txPageNumber.Text = MaxPages.ToString();
                    return;
                }
                currentPage = IntervalChangeNumber * (pageNumber);
                txPageNumber.Text = (pageNumber).ToString();

                if (OnPageChange != null) OnPageChange(CurrentPage);
            }
        }
    }
}
