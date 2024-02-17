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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2;
using WpfApp3;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for Page_LeasingInquiries.xaml
    /// </summary>

    #region ----- Comments -----
    //InitialTextProperty - это зависимое свойство(dependency property), которое будет ассоциироваться
    //с элементами TextBox.Оно предоставляет механизм для хранения начального текста для TextBox.

    //GetInitialText - это метод, который позволяет получить значение ассоциированного свойства InitialText для указанного элемента TextBox.

    //SetInitialText - это метод, который позволяет установить значение ассоциированного свойства InitialText для указанного элемента TextBox.

    //Как это работает:

    //Вы определяете ассоциированное свойство InitialText, которое будет использоваться для хранения начального текста для TextBox.

    //Затем, в вашей разметке XAML, вы можете использовать это свойство, чтобы присвоить начальный текст каждому TextBox, как показано в предыдущих примерах.

    //В обработчиках событий GotFocus и LostFocus, вы можете использовать методы GetInitialText и SetInitialText для получения
    //и установки начального текста для конкретного TextBox. Это позволяет вам сохранять и восстанавливать начальный текст для каждого TextBox отдельно.
    #endregion
    public class TextBoxFocusParams
    {
        public static readonly DependencyProperty InitialTextProperty = DependencyProperty.RegisterAttached(
            "InitialText", typeof(string), typeof(TextBoxFocusParams), new PropertyMetadata(null));

        public static string GetInitialText(in DependencyObject obj)
        {
            return ((string)obj.GetValue(InitialTextProperty));
        }

        public static void SetInitialText(DependencyObject obj, in string value)
        {
            obj.SetValue(InitialTextProperty, value);
        }
    }

    public partial class Page_LeasingInquiries : Page
    {
        public Page_LeasingInquiries()
        {
            InitializeComponent();
        }

        #region ----- Text Boxes Focus Params -----

        #region ----- Margin Animation -----
        private static void DecreaseMargin(in TextBox textBox)
        {
            textBox.BeginAnimation(Grid.MarginProperty, new ThicknessAnimation(
               textBox.Margin, new Thickness(10), TimeSpan.FromMilliseconds(200)
               ));

        }

        private static void IncreaseMargin(in TextBox textBox)
        {
            textBox.BeginAnimation(Grid.MarginProperty, new ThicknessAnimation(
                textBox.Margin, new Thickness(15, 10, 15, 10), TimeSpan.FromMilliseconds(200)
                ));
        }

        #endregion

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = ((TextBox)sender);


            IncreaseMargin(textBox);

            if (textBox.Text == TextBoxFocusParams.GetInitialText(textBox))
            {
                textBox.Text = "";
                textBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = ((TextBox)sender);

            DecreaseMargin(textBox);


            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
                textBox.Text = TextBoxFocusParams.GetInitialText(textBox);
            }
        }


        #endregion

        private void Button_Add_RentalApplication(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] NameSurname = txtNameSurname.Text.Split(',', StringSplitOptions.RemoveEmptyEntries);

                FileMenager.RentalApplicationWriteToFile(
                new RentalApplication(
                    new PersonalInfo(
                        txtBrand.Text,
                        new Person(NameSurname[0], NameSurname[1]),
                        new IdentifyingInformation(
                            txtPhoneNumber.Text, txtEmail.Text
                        )
                    ),
                    txtProductType.Text,
                    new TradeArea(
                        double.Parse(txtCommercial.Text),
                        double.Parse(txtServiceRoom.Text),
                        double.Parse(txtStorage.Text)),
                        txtUtilitiesRequirement.Text,
                    new RetailOutlet(
                        byte.Parse(cmbOperationType.Text),
                        txtAddress.Text, txtOperationType.Text),
                    txtAdditionalQuestionsAndComments.Text));

                MainWindow.window.BrendsFrame.NavigationService.Navigate(new Page_ThanksPage());
            }
            catch (Exception)
            {

                return;
            }
        }
        public static string[] SplitStringBySpaces(string input)
        {
            return input.Split(',', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
