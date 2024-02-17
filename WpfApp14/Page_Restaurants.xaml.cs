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

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for Page_Restaurants.xaml
    /// </summary>
    public partial class Page_Restaurants : Page
    {
        public Page_Restaurants()
        {
            InitializeComponent();

            for (int i = 0; i < FileMenager.Data.RestaurantPlaces.Count; i++)
            {
                UIElementBuilder uIElementBuilder = new UIElementBuilder(
                    FileMenager.Data.RestaurantPlaces[i], WrapPanel_Restaurants
                    );
            }
        }
    }
}
