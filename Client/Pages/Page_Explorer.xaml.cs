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
using Models;

namespace Client.Pages
{
    /// <summary>
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    public partial class Page_Explorer : Page
    {
        public List<Routine> routines = new List<Routine>();

        public Page_Explorer()
        {
            InitializeComponent();
            FillRountinesList();
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

        private void Button_Like_Clic(object sender, MouseButtonEventArgs e)
        {

        }

        private void ListRoutines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
