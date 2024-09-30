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
using System.IO;

namespace Absolut.View
{
    /// <summary>
    /// Interaction logic for HelperAdmin.xaml
    /// </summary>
    public partial class HelperAdmin : Window
    {
        public HelperAdmin()
        {
            InitializeComponent();

            this.Loaded += LoadWindow;
        }

        private void LoadWindow(object sender, RoutedEventArgs e)
        {
            string currentDir = Directory.GetCurrentDirectory();
            string reportPath = System.IO.Path.Combine(currentDir, "HelpAdm.mht");

            Uri uri = new Uri(reportPath);
            Web.Navigate(uri);
        }
    }
}
