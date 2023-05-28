using Logic;
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
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Navigator_Page(object sender, SelectionChangedEventArgs e)
        {
            if (TabItem_Explorer.IsSelected)
            {
                Frame_Page.Navigate(new Uri("/Pages/Page_Explorer.xaml", UriKind.Relative));
            }
            else if (TabItem_MyLists.IsSelected)
            {
                Frame_Page.Navigate(new Uri("/Pages/Page_MyLists.xaml", UriKind.Relative));
            }
            else if (TabItem_AddRoutine.IsSelected)
            {
                Frame_Page.Navigate(new Uri("/Pages/Page_AddRoutine.xaml", UriKind.Relative));
            }
            else if(TabItem_Favorites.IsSelected)
            {
                Frame_Page.Navigate(new Uri("/Pages/Page_Favorites.xaml", UriKind.Relative));
            }
            else if(TabItem_Profile.IsSelected)
            {
                Frame_Page.Navigate(new Uri("/Pages/Page_Login.xaml", UriKind.Relative));
            }
        }

    }
}

