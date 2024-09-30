using System;
using System.Windows;
using System.Windows.Input;
using Absolut.ViewModel;
using Absolut.Model;

namespace Absolut.View
{
    /// <summary>
    /// Interaction logic for MenuActivity.xaml
    /// </summary>
    public partial class MenuActivity : Window
    {
        public MenuActivity()
        {
            InitializeComponent();

            DataContext = new VMMenuActivity(PresenterModel.Model);

            if (DataContext is VMMenuActivity menuActivity)
            {
                menuActivity.OpenWindow += OpenWindow;
            }
        }

        private void OpenWindow(object? sender, byte e)
        {
            switch(e)
            {
                case 0:
                    {
                        Companies companies = new Companies();
                        companies.Show();
                        this.Close();
                    }
                    break;
                case 1:
                    {
                        Activities activities = new Activities();
                        activities.Show();
                        this.Close();
                    }
                    break;
                case 2:
                    {
                        ControlExpInc control = new ControlExpInc(1);
                        control.Show();
                        this.Close();
                    }
                    break;
                case 3:
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
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
            }
        }

        private void OpenExpense(object sender, MouseButtonEventArgs e)
        {
            HistoryExpAct historyExpAct = new HistoryExpAct();
            historyExpAct.Show();
        }

        private void OpenIncome(object sender, MouseButtonEventArgs e)
        {
            HistoryIncAct historyIncAct = new HistoryIncAct();
            historyIncAct.Show();
        }
    }
}
