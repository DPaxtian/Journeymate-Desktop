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
                if(myRoutines != null)
                {
                    List_MyRoutines.ItemsSource = myRoutines;
                    if (myRoutines.Count == 0)
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
            if(List_MyRoutines.SelectedItem != null)
            {
                Routine selectedRoutine = (Routine)List_MyRoutines.SelectedItem;
                Page_RoutineDetails.routineDetails = selectedRoutine;
                MainWindow.Instance.Frame_Page.Navigate(new Uri("/Pages/Page_RoutineDetails.xaml", UriKind.Relative));
            }
        }

        private async void Button_Delete_Clic(object sender, MouseButtonEventArgs e)
        {
            List_MyRoutines.IsEnabled = false;
            Image button = (Image)sender;
            DependencyObject container = VisualTreeHelper.GetParent(button);
            int statusCode = 500;

            while (!(container is ListBoxItem) && container != null)
            {
                container = VisualTreeHelper.GetParent(container);
            }

            if (container is ListBoxItem listBoxItem)
            {
                Routine routineSelected = (Routine)List_MyRoutines.ItemContainerGenerator.ItemFromContainer(listBoxItem);


                var messageResult = MessageBox.Show("¿Desea eliminar la rutina? Esta accion no se puede deshacer", "Eliminar rutina", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (messageResult == MessageBoxResult.Yes)
                {
                    statusCode = await RoutineLogic.DeleteRoutine(routineSelected.Id);


                    if (statusCode == 200)
                    {
                        MessageBox.Show("Rutina eliminada con exito!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                        FillMyRoutinesList();
                    }
                    else
                    {
                        MessageBox.Show("Hubo un problema al eliminar la rutina", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                List_MyRoutines.IsEnabled = true;
            }
        }


        private void Button_Edit_Clic(object sender, MouseButtonEventArgs e)
        {
            List_MyRoutines.IsEnabled = false;
            Image button = (Image)sender;
            DependencyObject container = VisualTreeHelper.GetParent(button);

            while (!(container is ListBoxItem) && container != null)
            {
                container = VisualTreeHelper.GetParent(container);
            }

            if (container is ListBoxItem listBoxItem)
            {
                Routine routineSelected = (Routine)List_MyRoutines.ItemContainerGenerator.ItemFromContainer(listBoxItem);

                Page_EditRoutine.routine = routineSelected;
                MainWindow.Instance.Frame_Page.Navigate(new Uri("/Pages/Page_EditRoutine.xaml", UriKind.Relative));
            }
        }
    }
}
