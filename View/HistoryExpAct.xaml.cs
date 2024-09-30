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
using Absolut.ViewModel;
using Absolut.Model;

namespace Absolut.View
{
    /// <summary>
    /// Interaction logic for HistoryExpAct.xaml
    /// </summary>
    public partial class HistoryExpAct : Window
    {
        public HistoryExpAct()
        {
            InitializeComponent();

            DataContext = new VMHistoryExpAct(PresenterModel.Model, new DialogService());

            if (DataContext is  VMHistoryExpAct expAct)
            {
                expAct.Message += ExpAct_Message;
                expAct.OpenCompanies += ExpAct_OpenCompanies;
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
                        Window company = Application.Current.MainWindow as HistoryExpAct;

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
