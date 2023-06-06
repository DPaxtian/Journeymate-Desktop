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
    /// Lógica de interacción para Page_MyLists.xaml
    /// </summary>
    public partial class Page_MyLists : Page
    {
        public List<Routine> myRoutines = new List<Routine>();

        public Page_MyLists()
        {
            InitializeComponent();
            FillMyRoutinesList();
        }


        private async void FillMyRoutinesList()
        {
            try
            {
                myRoutines = await RoutineLogic.GetRoutinesCreated(MainWindow.UserLogged.Username);
                List_MyRoutines.ItemsSource = myRoutines;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void ListRoutines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(List_MyRoutines.SelectedItem != null)
            {
                Routine selectedRoutine = (Routine)List_MyRoutines.SelectedItem;
                Page_RoutineDetails.routineDetails = selectedRoutine;
                MainWindow.Instance.Frame_Page.Navigate(new Uri("/Pages/Page_RoutineDetails.xaml", UriKind.Relative));
            }
        }

        private void Button_Delete_Clic(object sender, MouseButtonEventArgs e)
        {
            Image button = (Image)sender;
            DependencyObject container = VisualTreeHelper.GetParent(button);

            while (!(container is ListBoxItem) && container != null)
            {
                container = VisualTreeHelper.GetParent(container);
            }

            if (container is ListBoxItem listBoxItem)
            {
                int index = List_MyRoutines.ItemContainerGenerator.IndexFromContainer(listBoxItem);
                Console.WriteLine(index.ToString());
            }
        }

        private void Button_Edit_Clic(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
