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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Button_Like_Clic(object sender, MouseButtonEventArgs e)
        {

        }

        private void ListRoutines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
