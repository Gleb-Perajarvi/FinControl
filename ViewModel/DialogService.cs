using System;
using System.Windows;

namespace Absolut.ViewModel
{
    public class DialogService : IDialogService
    {
        public bool YesOrNo(string message, string caption)
        {
            if (MessageBox.Show(message, caption, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
