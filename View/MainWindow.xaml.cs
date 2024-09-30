using Absolut.Model;
using Absolut.View;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Absolut
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ViewModel.VMMainWindow();

            if (DataContext is ViewModel.VMMainWindow viewmodel)
            {
                viewmodel.Message += Viewmodel_Message;
                viewmodel.Status += Enter;
            }
        }

        private void Enter(object? sender, bool e)
        {
            if (e)
            {
                Companies companies = new Companies();
                companies.Show();
                this.Close();
            }
        }

        private void Viewmodel_Message(object? sender, string e)
        {
            MessageBox.Show(e);
        }

        private void EnterPassword(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModel.VMMainWindow vm)
            {
                vm.Password = ((PasswordBox)sender).SecurePassword;
            }
        }
    }
}
