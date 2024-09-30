using Absolut.Model;
using Absolut.ViewModel;
using System;
using System.Windows;

namespace Absolut.View
{
    /// <summary>
    /// Interaction logic for Activities.xaml
    /// </summary>
    public partial class Activities : Window
    {
        public Activities()
        {
            InitializeComponent();

            DataContext = new VMActivities(PresenterModel.Model, new DialogService());

            if (DataContext is VMActivities mActivities)
            {
                mActivities.Message += Message;
                mActivities.OpenMenuActivity += OpenActivity;
            }
        }

        private void OpenActivity(object? sender, bool e)
        {
            if (e)
            {
                if (CheckIsWinOpen.IsWinActOpen)
                {
                    this.Close();
                }
                else
                {
                    MenuActivity ma = new MenuActivity();
                    ma.Show();
                    this.Close();
                }
            }
        }

        private void Message(object? sender, string e)
        {
            MessageBox.Show(e, "Результат");
        }

        private void ClosindActivities(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CheckIsWinOpen.IsWinActOpen = false;
        }
    }
}
