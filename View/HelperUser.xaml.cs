using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for HelperUser.xaml
    /// </summary>
    public partial class HelperUser : Window
    {
        public HelperUser()
        {
            InitializeComponent();

            this.Loaded += LoadWindow;
        }

        private void LoadWindow(object sender, RoutedEventArgs e)
        {
            string currentDir = Directory.GetCurrentDirectory();
            string reportPath = System.IO.Path.Combine(currentDir, "HelpUser.mht");

            Uri helpUri = new Uri(reportPath);
            Web.Navigate(helpUri);
        }
    }
}
