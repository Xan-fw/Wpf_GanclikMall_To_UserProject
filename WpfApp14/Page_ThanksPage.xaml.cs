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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2;
using WpfApp6;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for Page_ThanksPage.xaml
    /// </summary>
    public partial class Page_ThanksPage : Page
    {
        public Page_ThanksPage()
        {
            InitializeComponent();
        }

        private void ButtonClick_LeasingInquiries(object sender, RoutedEventArgs e)
        {
            MainWindow.window.BrendsFrame.NavigationService.Content = new Page_LeasingInquiries();
        }
    }
}
