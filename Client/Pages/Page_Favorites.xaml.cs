using Logic;
using Models;
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

namespace Client.Pages
{
    /// <summary>
    /// Lógica de interacción para Page_Favorites.xaml
    /// </summary>
    public partial class Page_Favorites : Page
    {

        public List<Routine> favoritesRoutines = new List<Routine>();

        public Page_Favorites()
        {
            InitializeComponent();
            FillMyRoutinesList();
        }


        private async void FillMyRoutinesList()
        {
            try
            {
                favoritesRoutines = await RoutineLogic.GetRoutinesFollowed(MainWindow.UserLogged.Username);
                List_FavoritesRoutines.ItemsSource = favoritesRoutines;
                if (favoritesRoutines != null)
                {
                    List_FavoritesRoutines.ItemsSource = favoritesRoutines  ;
                    if (favoritesRoutines.Count == 0)
                    {
                        LabelRoutinesEmpty.Visibility = Visibility.Visible;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        private void ListRoutines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_FavoritesRoutines.SelectedItem != null)
            {
                Routine selectedRoutine = (Routine)List_FavoritesRoutines.SelectedItem;
                Page_RoutineDetails.routineDetails = selectedRoutine;
                MainWindow.Instance.Frame_Page.Navigate(new Uri("/Pages/Page_RoutineDetails.xaml", UriKind.Relative));
            }
        }

        private async void Button_Dislike_Clic(object sender, MouseButtonEventArgs e)
        {
            List_FavoritesRoutines.IsEnabled = false;
            Image button = (Image)sender;
            DependencyObject container = VisualTreeHelper.GetParent(button);

            while (!(container is ListBoxItem) && container != null)
            {
                container = VisualTreeHelper.GetParent(container);
            }

            if (container is ListBoxItem listBoxItem)
            {
                Routine routineSelected = (Routine)List_FavoritesRoutines.ItemContainerGenerator.ItemFromContainer(listBoxItem);

                int statusCode = await RoutineLogic.UnfollowRoutine(MainWindow.UserLogged.Username, routineSelected.Id);

                if(statusCode == 200)
                {
                    FillMyRoutinesList();
                    List_FavoritesRoutines.IsEnabled = true;
                }
            }
        }
    }
}
