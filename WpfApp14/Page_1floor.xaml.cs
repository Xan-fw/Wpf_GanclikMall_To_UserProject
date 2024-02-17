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
using WpfApp10;
using WpfApp2;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for Page_1floor.xaml
    /// </summary>
    public partial class Page_1floor : Page
    {
        public Page_1floor()
        {
            InitializeComponent();

            if (Page_MallMap.brandsToFloor != null)
            {
                MainWindow.placeInfos = InfoManager.FindPlacesByFloor(((sbyte)1));
                Page_MallMap.brandsToFloor.ItemsSource = MainWindow.placeInfos;

                Page_MallMap.brandsToFloor.SelectionChanged += BrandsToFloor_SelectionChanged;
            }


        }
        private void BrandsToFloor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Page_MallMap.brandsToFloor.SelectedIndex != -1)
            {
                Page_MallMap.ShowBrandInformation_ToMap(
                MainWindow.placeInfos[Page_MallMap.brandsToFloor.SelectedIndex]
                );
            }
        }
    }
}
