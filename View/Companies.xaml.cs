using Absolut.Model;
using Absolut.ViewModel;
using System;
using System.Windows;

namespace Absolut.View
{
    /// <summary>
    /// Interaction logic for Companies.xaml
    /// </summary>
    public partial class Companies : Window
    {
        public Companies()
        {
            InitializeComponent();

            DataContext = new VMCompanies(PresenterModel.Model,new DialogService());

            if (DataContext is VMCompanies vm)
            {
                vm.Message += Vm_Message;
                vm.OpenWindowMainMenu += Vm_OpenWindowMainMenu;
            }
        }

        private void Vm_OpenWindowMainMenu(object? sender, bool e)
        {
            if (e)
            {
                MainMenu mm = new MainMenu();
                mm.Show();
                this.Close();
            }
        }

        private void Vm_Message(object? sender, string e)
        {
            MessageBox.Show(e);
        }
    }
}
