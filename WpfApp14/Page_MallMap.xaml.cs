using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using WpfApp2;
using WpfApp3;

namespace WpfApp10
{
    /// <summary>
    /// Interaction logic for Page_MallMap.xaml
    /// </summary>
    public partial class Page_MallMap : Page
    {
        public static TextBlock? TextBlockClickedNew = null;
        public static TextBlock? TextBlockClickedOld = null;
        private static Int16 MyTimeSpan = 220;

        public static Page? Page_Mallmap;

        public static ListView? brandsToFloor;

        public static StackPanel? stackPanel;
        public static Canvas? stackPanelBr;
        public Page_MallMap()
        {
            InitializeComponent();

            Page_Mallmap = this;

            stackPanel = brand_info;
            stackPanelBr = stackPanelBrands;
            brandsToFloor = BrandsToFloor;

            MainWindow.button_Map.Content = "▼ Close map";

            MainWindow.isOpenMap = true;

            Frame_floor.NavigationService.Navigate(new Page__1floor());

        }

        private void OpenPageToIndex(object sender, RoutedEventArgs e)
        {
            Page_MallMap.stackPanel.Visibility = Visibility.Collapsed;
            Page_MallMap.stackPanelBr.Visibility = Visibility.Visible;

            TextBlock textBlock = ((TextBlock)sender);
            sbyte pageIndex = sbyte.Parse(textBlock.Text);
          

            if (pageIndex == -1)
            {
                Frame_floor.NavigationService.Navigate(new Page__1floor());
            }if (pageIndex == 1)
            {
                Frame_floor.NavigationService.Navigate(new Page_1floor());
            }if (pageIndex == 2)
            {
                Frame_floor.NavigationService.Navigate(new Page_2floor());
            }if (pageIndex == 3)
            {
                Frame_floor.NavigationService.Navigate(new Page_3floor());
            }

            FloorToCanvas.Text = pageIndex.ToString();
            //=======================================================

            TextBlockClickedOld = TextBlockClickedNew;
            TextBlockClickedNew = textBlock;
            if (TextBlockClickedOld != null | textBlock != TextBlockClickedNew)
            {
                TextBlockClickedOld.Foreground = new SolidColorBrush(Colors.Gray);
            }
            textBlock.Foreground = new SolidColorBrush(Colors.Black);

            MainWindow.isOpenMap = true;

        }


        public void ShowBrandInformation(in PlaceInfo placeInfo)
        {
            Brand_image.Source = new BitmapImage(new Uri(placeInfo.News_Image_Path));
            Brand_Name.Text = placeInfo.Name;
            Brand_floor.Text = "Floor: " + placeInfo.Floor.ToString();
            Brand_PhoneNumber.Text = "Telephone: " + placeInfo.Telephone;
            Brand_Description.Text = placeInfo.Description;
        }

        public static void ShowBrandInformation_ToMap(in PlaceInfo placeInfo)
        {
            Page_MallMap page_MallMap = new Page_MallMap();

            MainWindow.window.BrendsFrame.NavigationService.Navigate(page_MallMap);

            if (placeInfo.Floor == -1)
            {
                page_MallMap.Frame_floor.NavigationService.Navigate(new Page__1floor());
            }
            if (placeInfo.Floor == 1)
            {
                page_MallMap.Frame_floor.NavigationService.Navigate(new Page_1floor());
            }
            if (placeInfo.Floor == 2)
            {
                page_MallMap.Frame_floor.NavigationService.Navigate(new Page_2floor());
            }
            if (placeInfo.Floor == 3)
            {
                page_MallMap.Frame_floor.NavigationService.Navigate(new Page_3floor());
            }

            page_MallMap.ShowBrandInformation(placeInfo);
            //=====================================================
            page_MallMap.brand_info.Visibility = Visibility.Visible;
            page_MallMap.stackPanelBrands.Visibility = Visibility.Collapsed;
        }
    }
}
