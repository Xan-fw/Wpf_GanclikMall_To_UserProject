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
    /// Interaction logic for Page_Entertainment.xaml
    /// </summary>
    public partial class Page_Entertainment : Page
    {
        public Page_Entertainment()
        {
            InitializeComponent();

            for (int i = 0; i < FileMenager.Data.EntertainmentPlaces.Count; i++)
            {
                UIElementBuilder uIElementBuilder = new UIElementBuilder(
                    FileMenager.Data.EntertainmentPlaces[i], WrapPanel_Page_Entertainment
                    );
            }
        }
    }
}
