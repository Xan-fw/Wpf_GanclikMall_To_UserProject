using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1;
using WpfApp10;
using WpfApp3;
using WpfApp6;

namespace WpfApp2
{
    #region ----- Creat Brand Info ---- 
    public class UIElementBuilder
    {
        public UIElementBuilder() { }

        public UIElementBuilder(PlaceInfo item, in WrapPanel wrapPanel)
        {
            // Create Grid
            Grid grid_furst = CreateGrid(Colors.White, new Thickness(13), 200, 200);
            // Add RowDefinition
            grid_furst.RowDefinitions.Add(new RowDefinition());
            grid_furst.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });

            grid_furst.MouseDown += (sender, e) => Page_MallMap.ShowBrandInformation_ToMap(item);

            // Create Grid_1
            Grid grid_1 = new Grid();
            AddElementToGrid(grid_furst, grid_1, 1);

            // Create TopGrid
            Grid topGrid = CreateTopGrid(49.8);
            grid_1.Children.Add(topGrid);

            // Create Image
            Image image = CreateImage(item.News_Image_Path);
            AddElementToGrid(grid_furst, image, 0);

            // Create TextBlock Brand Name
            TextBlock textBlock1 = CreateTextBlock(
                $"{item.Name}", 17,
                FontWeights.Bold,
                new Thickness(10, 2, 0, 0),
                Brushes.Black);
            AddElementToGrid(grid_furst, textBlock1, 1);

            // Create TextBlock Floor && Phone Number
            TextBlock textBlock2 = CreateTextBlock(
                $"Floor {item.Floor} / T: {item.Telephone}", 14,
                FontWeights.Normal,
                new Thickness(10, 20, 0, 0), Brushes.Gray);
            AddElementToGrid(grid_furst, textBlock2, 1);

            // Apply shadow effect
            ShadowEffect(grid_furst, (Color)ColorConverter.ConvertFromString("#0094c9"), 270, 0.4, 40);

            // Add the Grid to WrapPanel_Stories
            wrapPanel.Children.Add(grid_furst);
        }

        public static void GridAnimate(Grid grid)
        {
            double originalHeight = grid.Height;

            grid.MouseEnter += (sender, e) =>
            {
                DoubleAnimation heightAnimation = new DoubleAnimation
                {
                    To = 190,
                    Duration = TimeSpan.FromSeconds(0.20)
                };

                grid.BeginAnimation(FrameworkElement.HeightProperty, heightAnimation);
            };

            grid.MouseLeave += (sender, e) =>
            {
                DoubleAnimation heightAnimation = new DoubleAnimation
                {
                    To = originalHeight,
                    Duration = TimeSpan.FromSeconds(0.07)
                };

                grid.BeginAnimation(FrameworkElement.HeightProperty, heightAnimation);
            };
        }

        public static Grid CreateGrid(
            in Color color,
            in Thickness thickness,
            in double width,
            in double height)
        {
            var grid = new Grid
            {
                Background = new SolidColorBrush(color),
                Width = width,
                Height = height,
                Margin = thickness
            };

            GridAnimate(grid);

            return grid;
        }

        public static Grid CreateTopGrid(in double height)
        {
            var topGrid = new Grid
            {
                Background = new SolidColorBrush(Colors.Gray),
                Margin = new Thickness(0, 0, 0, height)
            };
            return topGrid;
        }

        public static Image CreateImage(in string imageUrl)
        {
            var image = new Image
            {
                Source = new BitmapImage(new Uri(imageUrl))
            };
            return image;
        }

        public static TextBlock CreateTextBlock(
            in string text,
            in double fontSize,
            in FontWeight fontWeight,
            in Thickness padding,
            in Brush foreground)
        {
            var textBlock = new TextBlock
            {
                Text = text,
                FontSize = fontSize,
                FontWeight = fontWeight,
                Padding = padding,
                Foreground = foreground
            };
            return textBlock;
        }

        public static void AddElementToGrid(
            in Grid parentGrid,
            in UIElement childElement,
            int row)
        {
            Grid.SetRow(childElement, row);
            parentGrid.Children.Add(childElement);
        }

        public static void ShadowEffect(
            in UIElement element,
            in Color color,
            in double direction,
            in double opacity,
            in double blurRadius)
        {
            var dropShadowEffect = new DropShadowEffect
            {
                Color = color,
                Direction = direction,
                Opacity = opacity,
                BlurRadius = blurRadius
            };
            element.Effect = dropShadowEffect;
        }

    }
    #endregion
    public partial class MainWindow : Window
    {
        public static List<PlaceInfo>? placeInfos = null;
        static InfoManager manager = FileMenager.Data;
        public MainWindow()
        {
            InitializeComponent();

            BrendsFrame.NavigationService.Navigate(new Page_MainPage());

            window = this;
            button_Map = Button_Map;
            FileMenager fileMenager = new FileMenager();
            FileMenager.BrandsReadToFile();

            manager = FileMenager.Data;

            FileMenager.RentalApplicationReadAllFiles();

            //RentalApplication rentalApplication = FileMenager.RentalApplicationList[1];

            //PlaceInfo newBrand = new PlaceInfo(
            //     rentalApplication.PersonalInfo.TradeMark,
            //     2,
            //     rentalApplication.ProductOrServiceType,
            //     "http://i.otzovik.com/objects/b/320000/315209.png",
            //     rentalApplication.PersonalInfo.Identifying._PhoneNumber
            //     );
            //manager.StorePlaces.Add(newBrand);
            //RentalApplication rentalApplication1 = FileMenager.RentalApplicationList[2];

            //PlaceInfo newBrand1 = new PlaceInfo(
            //     rentalApplication.PersonalInfo.TradeMark,
            //     2,
            //     rentalApplication.ProductOrServiceType,
            //     "http://i.otzovik.com/objects/b/320000/315209.png",
            //     rentalApplication.PersonalInfo.Identifying._PhoneNumber
            //     );
            //manager.StorePlaces.Add(newBrand);

            //// ===================== N2 =====================
            //FileMenager.RentalApplicationWriteToFile(new RentalApplication(
            //    new PersonalInfo(
            //        "ALDO",
            //        new Person("John", "Doe"),
            //        new IdentifyingInformation(
            //            "165-486-7590", "john.doe@example.com"
            //        )
            //    ),
            //    "Product or Service Type",
            //    new TradeArea(100.0, 50.0, 25.0),
            //    "",
            //    new RetailOutlet(1, "123 Store St", "Retail"),
            //    "Additional questions and comments"
            //));

            //RentalApplication rentalApplication1 = FileMenager.RentalApplicationList[0];

            //PlaceInfo newBrand1 = new PlaceInfo(
            //     rentalApplication1.PersonalInfo.TradeMark,
            //     3,
            //     rentalApplication1.ProductOrServiceType,
            //     "http://i.otzovik.com/objects/b/320000/315209.png",
            //      rentalApplication1.PersonalInfo.Identifying._PhoneNumber
            //     );

            //manager.StorePlaces.Add(newBrand1);
            //FileMenager.BrandsWriteToFile();

        }


        #region ---- Button Click Prop -----
        private static Int16 MyTimeSpan = 220;

        static Button? ButtonClickedNew = null;
        static Button? ButtonClickedOld = null;

        private void Button_ChangeAppearance(object sender, RoutedEventArgs e)
        {
            Button button = ((Button)sender);

            if (button == ButtonClickedNew) { return; }

            button.Background = new SolidColorBrush(Color.FromArgb(255, 45, 28, 103));
            button.Foreground = new SolidColorBrush();

            ColorAnimation foregroundAnimation = new ColorAnimation
            {
                To = Colors.White,
                Duration = TimeSpan.FromMilliseconds(MyTimeSpan),
            };

            button.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, foregroundAnimation);
        }

        private void Button_ChangeToOriginal(object sender, RoutedEventArgs e)
        {

            Button button = ((Button)sender);

            if (button == ButtonClickedNew) { return; }

            button.Background = new SolidColorBrush();
            button.Foreground = new SolidColorBrush(Color.FromArgb(255, 45, 28, 103));

            ColorAnimation foregroundAnimation = new ColorAnimation
            {
                To = Color.FromArgb(255, 45, 28, 103),
                Duration = TimeSpan.FromMilliseconds(MyTimeSpan),
            };


            button.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, foregroundAnimation);
        }

        public void HandleCategoryButtonClick(Button button, RoutedEventArgs e)
        {
            ToggleMapVisibility();
            ButtonClickedOld = ButtonClickedNew;
            ButtonClickedNew = button;
            if (ButtonClickedOld != null)
            {
                Button_ChangeToOriginal(ButtonClickedOld, e);
            }
            Button_ChangeAppearance(button, e);
        }

        #endregion

        #region ----- Open Page -----
        private void Button_OpenPage_Stores(object sender, RoutedEventArgs e)
        {
            HandleCategoryButtonClick(((Button)sender), e);
            BrendsFrame.NavigationService.Content = new Page_Stores();
        }

        private void Button_OpenPage_Restaurants(object sender, RoutedEventArgs e)
        {
            HandleCategoryButtonClick(((Button)sender), e);
            BrendsFrame.NavigationService.Content = new Page_Restaurants();
        }

        private void Button_OpenPage_Entertainment(object sender, RoutedEventArgs e)
        {
            HandleCategoryButtonClick(((Button)sender), e);
            BrendsFrame.NavigationService.Content = new Page_Entertainment();
        }

        private void Button_OpenPage_News_Events(object sender, RoutedEventArgs e)
        {
            HandleCategoryButtonClick(((Button)sender), e);
            BrendsFrame.NavigationService.Content = new Page_NewsEvents();
        }

        #endregion

        #region ----- Business Info -----
        private void ShowWindowToWorkingHourInfo()
        {
            WorkingHours.WorkingHour workingHour = new WorkingHours.WorkingHour();

            string workHoursText = "Work hours";

            string shopsAndRestaurantsText = "Shops and restaurants";

            string shopsAndRestaurantsTimeText = manager.WorkingHour.StoresAndRestaurants;

            string supermarketText = "Supermarket";

            string supermarketTimeText = manager.WorkingHour.Supermarket;

            string cinemaText = "Cinema";

            string cinemaTimeText = manager.WorkingHour.Cinema;

            // Создание первого Grid
            Grid firstGrid = new Grid();
            firstGrid.Background = new SolidColorBrush(Color.FromArgb(167, 0xF7, 0xF7, 0xF7));
            Grid.SetZIndex(firstGrid, 10);

            // Добавление обработчика событий для закрытия первого Grid
            firstGrid.MouseDown += (sender, e) =>
            {
                MainGrid.Children.Remove(firstGrid);
                firstGrid = null;
            };

            // Добавление первого Grid к MainGrid
            MainGrid.Children.Add(firstGrid);

            // Создание второго Grid и установка его свойств
            Grid secondGrid = new Grid()
            {
                Width = 300,
                Height = 200,
                Background = new SolidColorBrush(Colors.White)
            };
            
            
            Grid grid_borderEffect = new Grid()
            {
                Width = 300,
                Height = 200,
                Background = new SolidColorBrush(Colors.White)
            };

            DropShadowEffect shadowEffect = new DropShadowEffect
            {
                Color = Colors.Black,
                Direction = 270,
                Opacity = 0.5,
                BlurRadius = 50
            };

            // Применение эффекта тени к Ellipse
            grid_borderEffect.Effect = shadowEffect;    

            // Добавление второго Grid к первому
            firstGrid.Children.Add(grid_borderEffect);
            firstGrid.Children.Add(secondGrid);
            // Создание TextBlock и установка его свойств
            TextBlock workHoursBlock = new TextBlock()
            {
                Text = workHoursText,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 200,
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(10, 0, 0, 0)
            };


            
            secondGrid.Children.Add(workHoursBlock);

            TextBlock shopsAndRestaurantsBlock = new TextBlock()
            {
                Text = shopsAndRestaurantsText,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Gray,
                Margin = new Thickness(10, 40, 0, 0)
            };
            secondGrid.Children.Add(shopsAndRestaurantsBlock);

            TextBlock shopsAndRestaurantsTimeBlock = new TextBlock()
            {
                Text = shopsAndRestaurantsTimeText,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(15, 55, 0, 0)
            };
            secondGrid.Children.Add(shopsAndRestaurantsTimeBlock);

            TextBlock supermarketBlock = new TextBlock()
            {
                Text = supermarketText,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Gray,
                Margin = new Thickness(10, 90, 0, 0)
            };
            secondGrid.Children.Add(supermarketBlock);

            TextBlock supermarketTimeBlock = new TextBlock()
            {
                Text = supermarketTimeText,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(15, 105, 0, 0)
            };
            secondGrid.Children.Add(supermarketTimeBlock);

            TextBlock cinemaBlock = new TextBlock()
            {
                Text = cinemaText,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Gray,
                Margin = new Thickness(10, 145, 0, 0)
            };
            secondGrid.Children.Add(cinemaBlock);

            TextBlock cinemaTimeBlock = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Text = cinemaTimeText,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(15, 160, 0, 0)
            };
            secondGrid.Children.Add(cinemaTimeBlock);

        }
        
        private void ShowWindowToContactInfoInfo()
        {

            ContactInfo.Contact contact = FileMenager.Data.ContactInformation;

            string contactHeaderText = "Contacts";
            string phoneLabelText = "Phone";
            string phoneText = contact.Phone;
            string emailLabelText = "Email";
            string emailText = contact.Email;
            string addressLabelText = "Address";
            string addressText = contact.Address;

            // Создание первого Grid
            Grid firstGrid = new Grid();
            firstGrid.Background = new SolidColorBrush(Color.FromArgb(167, 0xF7, 0xF7, 0xF7));
            Grid.SetZIndex(firstGrid, 10);

            // Добавление обработчика событий для закрытия первого Grid
            firstGrid.MouseDown += (sender, e) =>
            {
                MainGrid.Children.Remove(firstGrid);
                firstGrid = null;
            };

            // Добавление первого Grid к MainGrid
            MainGrid.Children.Add(firstGrid);

            // Создание второго Grid и установка его свойств
            Grid secondGrid = new Grid()
            {
                Width = 300,
                Height = 200,
                Background = new SolidColorBrush(Colors.White)
            };

            DropShadowEffect shadowEffect = new DropShadowEffect
            {
                Color = Colors.Black,
                Direction = 270,
                Opacity = 0.5,
                BlurRadius = 50
            };

            Grid grid_borderEffect = new Grid()
            {
                Width = 300,
                Height = 200,
                Background = new SolidColorBrush(Colors.White)
            };
            // Применение эффекта тени к Ellipse
            grid_borderEffect.Effect = shadowEffect;

            // Добавление второго Grid к первому
            firstGrid.Children.Add(grid_borderEffect);
            firstGrid.Children.Add(secondGrid);

            // Создание TextBlock и установка его свойств
            // Создание TextBlock и установка его свойств
            TextBlock contactHeaderTextBlock = new TextBlock()
            {
                Text = contactHeaderText,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 200,
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(10, 0, 0, 0)
            };
            secondGrid.Children.Add(contactHeaderTextBlock);

            TextBlock phoneLabelBlock = new TextBlock()
            {
                Text = phoneLabelText,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 200,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Gray,
                Margin = new Thickness(10, 40, 0, 0)
            };
            secondGrid.Children.Add(phoneLabelBlock);

            TextBlock phoneTextBlock = new TextBlock()
            {
                Text = phoneText,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 200,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(15, 55, 0, 0)
            };
            secondGrid.Children.Add(phoneTextBlock);

            TextBlock emailLabelBlock = new TextBlock()
            {
                Text = emailLabelText,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 200,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Gray,
                Margin = new Thickness(10, 90, 0, 0)
            };
            secondGrid.Children.Add(emailLabelBlock);

            TextBlock emailTextBlock = new TextBlock()
            {
                Text = emailText,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 200,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(15, 105, 0, 0)
            };
            secondGrid.Children.Add(emailTextBlock);

            TextBlock addressLabelBlock = new TextBlock()
            {
                Text = addressLabelText,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 200,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Gray,
                Margin = new Thickness(10, 145, 0, 0)
            };
            secondGrid.Children.Add(addressLabelBlock);

            TextBlock addressTextBlock = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 300,
                Text = addressText,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(15, 160, 0, 0)
            };
            secondGrid.Children.Add(addressTextBlock);


        }

        private void ButtonClick_WorkHoursInfo(object sender, RoutedEventArgs e)
        {
            ShowWindowToWorkingHourInfo();
        }

        //------------------------------------------

        private void ButtonClick_ContactInformation(object sender, RoutedEventArgs e)
        {
            ShowWindowToContactInfoInfo();

        }
        private void ToggleMapButtonContent()
        {
            ToggleMapVisibility();
            ButtonClickedOld = ButtonClickedNew;
            ButtonClickedNew = null;
            if (ButtonClickedOld != null)
            {
                Button_ChangeToOriginal(ButtonClickedOld, null);
            }
        }

        //------------------------------------------ 
        private void ButtonClick_HowToGet(object sender, RoutedEventArgs e)
        {
            ToggleMapButtonContent();
            BrendsFrame.NavigationService.Content = new Page_Howtoget();
        }

        //------------------------------------------

        private void ButtonClick_LeasingInquiries(object sender, RoutedEventArgs e)
        {
            ToggleMapButtonContent();
            BrendsFrame.NavigationService.Content = new Page_LeasingInquiries();
        }

        #endregion

        #region ----- Mall Map ----- 
        public static bool isOpenMap = false;

        public static MainWindow? window;

        public static Button? button_Map;
        public void ToggleMapVisibility()
        {
            isOpenMap = false;
            Button_Map.Content = "■ Interactive map";
        }
        private void Button_ClickOpenToMap(object sender, RoutedEventArgs e)
        {
            if (isOpenMap)
            {
                ToggleMapButtonContent();
                BrendsFrame.NavigationService.Navigate(new Page_MainPage());

            }
            else
            {
                BrendsFrame.NavigationService.Navigate(new Page_MallMap());
            }
            #endregion
        }
    }
}
