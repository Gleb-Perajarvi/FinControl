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

namespace Absolut.View
{
    /// <summary>
    /// Interaction logic for TestReport.xaml
    /// </summary>
    public partial class TestReport : Window
    {
        public TestReport()
        {
            InitializeComponent();

            DataContext = new VMReport();

            if (DataContext is VMReport rep)
            {
                rep.Message += ShowMessage;
            }
        }

        private void ShowMessage(object? sender, string e)
        {
            MessageBox.Show(e);
        }
    }
}
