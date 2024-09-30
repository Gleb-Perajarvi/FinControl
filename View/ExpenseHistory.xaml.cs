using Absolut.Model;
using Absolut.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Absolut.View
{
    /// <summary>
    /// Interaction logic for ExpenseHistory.xaml
    /// </summary>
    public partial class ExpenseHistory : Window
    {
        public ExpenseHistory()
        {
            InitializeComponent();

            DataContext = new VMExpenseHistory(PresenterModel.Model, new DialogService());

            if (DataContext is VMExpenseHistory vM)
            {
                vM.OpenCompanies += OpenCompanies;
                vM.Message += ShowMessage;
            }
        }

        private void ShowMessage(object? sender, string e)
        {
            MessageBox.Show(e);
        }

        private void OpenCompanies(object? sender, bool e)
        {
            if (e)
            {
                Window company = Application.Current.MainWindow as IncomeHistory;

                foreach (Window window in Application.Current.Windows)
                {
                    if (window != company)
                    {
                        window.Close();
                    }
                }

                Companies comp = new Companies();
                comp.Show();
                company.Close();
            }
        }
    }
}
