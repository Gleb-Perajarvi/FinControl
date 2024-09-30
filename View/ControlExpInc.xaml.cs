using System;
using System.Windows;
using Absolut.Model;
using Absolut.ViewModel;

namespace Absolut.View
{
    /// <summary>
    /// Interaction logic for ControlExpInc.xaml
    /// </summary>
    public partial class ControlExpInc : Window
    {
        private byte ind;

        public ControlExpInc(byte index)
        {
            InitializeComponent();

            ind = index;

            DataContext = new VMControlExpInc(PresenterModel.Model, new DialogService());

            if (DataContext is VMControlExpInc control)
            {
                control.Message += ShowMessage;
                control.OpenBorders += OpenBorders;
            }
        }

        private void OpenBorders(object? sender, byte e)
        {
            switch (e)
            {
                case 0:
                    {
                        if (AddAccount.Visibility == Visibility.Collapsed)
                            AddAccount.Visibility = Visibility.Visible;
                        else
                            AddAccount.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 1:
                    {
                        if (AddIncome.Visibility == Visibility.Collapsed)
                            AddIncome.Visibility = Visibility.Visible;
                        else
                            AddIncome.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 2:
                    {
                        if (AddExpense.Visibility == Visibility.Collapsed)
                            AddExpense.Visibility = Visibility.Visible;
                        else
                            AddExpense.Visibility = Visibility.Collapsed;
                    }
                    break;
            }
        }

        private void ShowMessage(object? sender, string e)
        {
            MessageBox.Show(e);
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            AddAccount.Visibility = Visibility.Collapsed;
            AddExpense.Visibility = Visibility.Collapsed;
            AddIncome.Visibility = Visibility.Collapsed;
        }

        private void Closed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            switch (ind)
            {
                case 0:
                    {
                        MainMenu menucomp = new MainMenu();
                        menucomp.Show();
                    }
                    break;
                case 1:
                    {
                        MenuActivity menuact = new MenuActivity();
                        menuact.Show();
                    }
                    break;
            }
        }
    }
}
