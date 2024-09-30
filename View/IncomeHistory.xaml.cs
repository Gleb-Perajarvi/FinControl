using Absolut.Model;
using Absolut.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for IncomeHistory.xaml
    /// </summary>
    public partial class IncomeHistory : Window
    {
        public IncomeHistory()
        {
            InitializeComponent();

            DataContext = new VMHistoryIncome(PresenterModel.Model, new DialogService());

            if (DataContext is VMHistoryIncome historyIncome)
            {
                historyIncome.Message += ShowMessage;
                historyIncome.OpenCompanies += OpenCompanies;
            }
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

        private void ShowMessage(object? sender, string e)
        {
            MessageBox.Show(e);
        }
    }
}
