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
    /// Interaction logic for HistoryIncAct.xaml
    /// </summary>
    public partial class HistoryIncAct : Window
    {
        public HistoryIncAct()
        {
            InitializeComponent();

            DataContext = new VMHistoryIncAct(PresenterModel.Model, new DialogService());

            if (DataContext is VMHistoryIncAct incAct)
            {
                incAct.Message += ExpAct_Message;
                incAct.OpenCompanies += ExpAct_OpenCompanies;
            }
        }

        private void ExpAct_Message(object? sender, string e)
        {
            MessageBox.Show(e);
        }

        private void ExpAct_OpenCompanies(object? sender, byte e)
        {
            switch (e)
            {
                case 0:
                    {
                        Window history = Application.Current.MainWindow as HistoryExpAct;

                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window != history)
                            {
                                window.Close();
                            }
                        }

                        Activities act = new Activities();
                        act.Show();
                        history.Close();
                    }
                    break;
                case 1:
                    {
                        Window company = Application.Current.MainWindow as HistoryIncAct;

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
                    break;
            }
        }
    }
}
