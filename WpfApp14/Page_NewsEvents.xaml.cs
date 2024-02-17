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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp10;
using WpfApp2;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for Page_NewsEvents.xaml
    /// </summary>
    class NewsBilder
    {
        public NewsBilder(News item, WrapPanel wrapPanel)
        {
            // Create Grid
            Grid grid_furst = UIElementBuilder.CreateGrid(Colors.White, new Thickness(13), 200, 200);
            // Add RowDefinition
            grid_furst.RowDefinitions.Add(new RowDefinition());
            grid_furst.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });

            grid_furst.MouseDown += (sender, e) =>
            {
                ShowNewsInformation(item.Content);
            };

            // Create Grid_1
            Grid grid_1 = new Grid();
            UIElementBuilder.AddElementToGrid(grid_furst, grid_1, 1);

            // Create TopGrid
            Grid topGrid = UIElementBuilder.CreateTopGrid(49.8);
            grid_1.Children.Add(topGrid);

            // Create Image
            Image image = UIElementBuilder.CreateImage(item.News_Image_Path);
            UIElementBuilder.AddElementToGrid(grid_furst, image, 0);

            TextBlock textBlock1 = UIElementBuilder.CreateTextBlock(
                $"{item.Headline}", 20,
                FontWeights.Bold,
                new Thickness(10, 13, 0, 0),
                Brushes.Black);
            UIElementBuilder.AddElementToGrid(grid_furst, textBlock1, 1);

            // Apply shadow effect
            UIElementBuilder.ShadowEffect(grid_furst, (Color)ColorConverter.ConvertFromString("#0094c9"), 230, 0.4, 40);

            // Add the Grid to WrapPanel_Stories
            wrapPanel.Children.Add(grid_furst);
        }
        private void ShowNewsInformation(in string content)
        {

            // Создание первого Grid
            Grid firstGrid = new Grid();
            firstGrid.Background = new SolidColorBrush(Color.FromArgb(167, 0xF7, 0xF7, 0xF7));
            Grid.SetZIndex(firstGrid, 10);

            // Добавление обработчика событий для закрытия первого Grid
            firstGrid.MouseDown += (sender, e) =>
            {
                MainWindow.window.MainGrid.Children.Remove(firstGrid);
                firstGrid = null;
            };

            // Добавление первого Grid к MainGrid
            MainWindow.window.MainGrid.Children.Add(firstGrid);

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
                Height = 190,
                Background = new SolidColorBrush(Colors.White)
            };

            DropShadowEffect shadowEffect = new DropShadowEffect
            {
                Color = Colors.Black,
                Direction = 230,
                Opacity = 0.5,
                BlurRadius = 50
            };

            // Применение эффекта тени к Ellipse
            grid_borderEffect.Effect = shadowEffect;

            // Добавление второго Grid к первому
            firstGrid.Children.Add(grid_borderEffect);
            firstGrid.Children.Add(secondGrid);

            TextBlock NewsText = new TextBlock() {
                Text = "News",
                FontWeight = FontWeights.Bold,
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(50,-3,0,0)
            };
            secondGrid.Children.Add(NewsText);


            TextBlock newsContent = new TextBlock() {
                Text = content,
                TextWrapping = TextWrapping.Wrap,
                Padding = new Thickness(3),
                Width = 300, Height = 150,
                Background = new SolidColorBrush(Colors.AliceBlue)
                
            };
            secondGrid.Children.Add(newsContent);
        }
    }
    public partial class Page_NewsEvents : Page
    {
        public Page_NewsEvents()
        {
            InitializeComponent();

            //FileMenager.Data.News.Add(new News(
                
            //    "Headline","content" , "https://bucketeer-06ddb2e0-9e6b-41fd-887f-2e5fabcd2cc6.s3.amazonaws.com/49165f6e-3b85-4b9b-8d03-cd715c2f0095.jpeg"));

            //FileMenager.BrandsWriteToFile();
            for (int i = 0; i < FileMenager.Data.News.Count; i++)
            {
                NewsBilder newsBilder = new NewsBilder(FileMenager.Data.News[i],WrapPanel_News_Events);

            }
        }
    }
}
