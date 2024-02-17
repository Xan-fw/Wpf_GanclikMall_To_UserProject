using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using WpfApp10;
using WpfApp2;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for Page_Stores.xaml
    /// </summary>

    public partial class Page_Stores : Page
    {
        static InfoManager manager = FileMenager.Data;

        public Page_Stores()
        {
            InitializeComponent();

            foreach (var item in manager.StorePlaces)
            {
                UIElementBuilder uIElementBuilder = new UIElementBuilder(item, WrapPanel_Stories);
            }


        }

    }
}

