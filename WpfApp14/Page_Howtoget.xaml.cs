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
using System.Diagnostics;
using System.Security.Policy;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Page_Howtoget.xaml
    /// </summary>
    public partial class Page_Howtoget : Page
    {
        public Page_Howtoget()
        {
            InitializeComponent();
        }

        private void OpenWebsite_ToView_Larger_Map(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo{
                    FileName = "https://www.google.com/maps?ll=40.400131,49.852945&z=15&t=m&hl=" +
                    "en-US&gl=US&mapclient=embed&q=Ganjlik+Mall+14+Fatali+Khan+Khoyski+Baku",
                    UseShellExecute = true});
            }
            catch (Exception ex)
            {
                throw;
            }
        } 
        
        private void OpenWebsite_ToDirections(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo{
                    FileName = "https://www.google.com/maps/dir//Ganjlik+Mall+14+Fatali+Khan" +
                    "+Khoyski+Baku/@40.4001309,49.8529446,15z/data=!4m8!4m7!1m0!1m5!1m1!1s0x" +
                    "40307d5d5b6c9daf:0xf5f41eb459d5e093!2m2!1d49.8529446!2d40.4001309?entry=ttu",
                    UseShellExecute = true});
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Hyperlink_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Hyperlink)sender).TextDecorations = TextDecorations.Underline;
        }

        private void Hyperlink_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Hyperlink)sender).TextDecorations = null;
        }

    }
}

