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
using Logic;
using Microsoft.Xaml.Behaviors.Core;
using Models;

namespace Client.Pages
{
    /// <summary>
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    public partial class Page_Explorer : Page
    {
        public List<Routine> routines = new List<Routine>();
        private List<string> idsRoutinesFollowed;

        public Page_Explorer()
        {
            InitializeComponent();
            FillRountinesList();
            if(MainWindow.UserLogged != null)
            {
                GetFollowedRoutines();
                
            }
        }


        private async void FillRountinesList()
        {
            try
            {
                routines = await RoutineLogic.GetRoutines();
                List_Routines.ItemsSource = routines.Where(r => r.Visibility.Equals("public"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
       

        private void TexBox_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox TextBox = (TextBox)sender;
            TextBox.Padding = new Thickness(60, 0, 60, 0); // Ajusta el valor del Padding para el espacio deseado
            TextBox.VerticalContentAlignment = VerticalAlignment.Center;
        }


        private void Button_Filters_Click(object sender, MouseButtonEventArgs e)
        {

        }


        private async void Button_Like_Clic(object sender, MouseButtonEventArgs e)
        {
            List_Routines.IsEnabled = false;
            Image button = (Image)sender;
            DependencyObject container = VisualTreeHelper.GetParent(button);

            while (!(container is ListBoxItem) && container != null)
            {
                container = VisualTreeHelper.GetParent(container);
            }

            if (container is ListBoxItem listBoxItem)
            {
                Routine routineSelected = (Routine)List_Routines.ItemContainerGenerator.ItemFromContainer(listBoxItem);

                if(MainWindow.UserLogged != null)
                {
                    if (!idsRoutinesFollowed.Contains(routineSelected.Id))
                    {
                        int statusCode = await RoutineLogic.FollowRoutine(MainWindow.UserLogged.Username, routineSelected.Id);

                        if (statusCode == 200)
                        {
                            MainWindow.Instance.NavigateToFavoritesList();
                        }
                        else
                        {
                            List_Routines.IsEnabled = true;
                        }
                    }
                    else
                    {
                        List_Routines.IsEnabled = true;
                    }
                }
                else
                {
                    MainWindow.Instance.NavigateToLogin();
                }
            }
        }


        private void ListRoutines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_Routines.SelectedItem != null)
            {
                Routine selectedRoutine = (Routine)List_Routines.SelectedItem;
                Page_RoutineDetails.routineDetails = selectedRoutine;
                MainWindow.Instance.Frame_Page.Navigate(new Uri("/Pages/Page_RoutineDetails.xaml", UriKind.Relative));
            }
        }


        private async void GetFollowedRoutines()
        {
            idsRoutinesFollowed = new List<string>();
            List<Routine> routines = await RoutineLogic.GetRoutinesFollowed(MainWindow.UserLogged.Username);
            foreach (Routine item in routines)
            {
                idsRoutinesFollowed.Add(item.Id);
            }
            CheckLikedRoutines();
        }

        private void CheckLikedRoutines()
        {
            
            foreach(Routine item in List_Routines.Items)
            {
                var emptyHeart = FindChild<Image>(List_Routines.ItemContainerGenerator.ContainerFromItem(item), "Image_EmptyHeart");
                var fullHeart = FindChild<Image>(List_Routines.ItemContainerGenerator.ContainerFromItem(item), "Image_FullHeart");

                if (idsRoutinesFollowed.Contains(item.Id))
                {
                    if(emptyHeart != null && fullHeart != null)
                    {
                        emptyHeart.Visibility = Visibility.Hidden;
                        fullHeart.Visibility = Visibility.Visible;
                    }
                }
            }
        }


        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null)
                return null;

            T foundChild = null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T childType && (child as FrameworkElement).Name == childName)
                {
                    foundChild = childType;
                    break;
                }
                else
                {
                    foundChild = FindChild<T>(child, childName);
                    if (foundChild != null)
                        break;
                }
            }

            return foundChild;
        }
    }
}
