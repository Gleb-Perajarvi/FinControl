using Absolut.Model;
using Absolut.ViewModel;
using System;
using System.Windows;

namespace Absolut.View
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
            
            DataContext = new VMMainMenu(PresenterModel.Model);
            
            if (DataContext is VMMainMenu vmMainMenu)
            {
                vmMainMenu.Message += VmMainMenu_Message;
                vmMainMenu.OpenWindow += OpenWindow;
            }
        }

        private void VmMainMenu_Message(object? sender, string e)
        {
            MessageBox.Show(e);
        }

        private void OpenWindow(object? sender, byte e)
        {
            switch (e)
            {
                case 0:
                    {
                        if (!CheckIsWinOpen.IsWinActOpen)
                        {
                            Activities act = new Activities();
                            act.Show();
                            CheckIsWinOpen.IsWinActOpen = true;
                        }
                    }
                    break;
                case 1:
                    {
                        ControlExpInc control = new ControlExpInc(0);
                        control.Show();
                        this.Close();
                    }
                    break;
                case 2:
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    break;
                case 3:
                    {
                        Companies companies = new Companies();
                        companies.Show();
                        this.Close();
                    }
                    break;
                case 4:
                    {
                        HelperUser helperUser = new HelperUser();
                        helperUser.Show();
                    }
                    break;
                case 5:
                    {
                        HelperAdmin helperAdmin = new HelperAdmin();
                        helperAdmin.Show();
                    }
                    break;
                case 6:
                    {
                        TestReport report= new TestReport();
                        report.Show();
                    }
                    break;
            }
        }

        private void ToHistoryIncome(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IncomeHistory incomeHistory = new IncomeHistory();
            incomeHistory.Show();
        }

        private void ToHistoryExpense(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ExpenseHistory expenseHistory = new ExpenseHistory();
            expenseHistory.Show();
        }
    }
}
