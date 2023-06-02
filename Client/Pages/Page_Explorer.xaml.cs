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
using Models;

namespace Client.Pages
{
    /// <summary>
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    public partial class Page_Explorer : Page
    {
        public List<Routine> routines = new List<Routine>
        {
           new Routine
           {
               Name = "Cafeterias drag",
               Country  = "Xalapa",
               Routine_Description = "Antros gay(drag) para agarrar un macanon",
               Visibility = "private",
               label_category = "Party",
               State_Country = "Veracruz",
               Town = "Xalapa",
               Creator = "Elva J. Fernández",
               Budget = "$300"
            },
           new Routine
           {
               Name = "Cafeterias drag",
               Country  = "Xalapa",
               Routine_Description = "Antros gay(drag) para agarrar un macanon",
               Visibility = "private",
               label_category = "Party",
               State_Country = "Veracruz",
               Town = "Xalapa",
               Creator = "Elva J. Fernández",
               Budget = "$300"
            },
           new Routine
           {
               Name = "Cafeterias drag",
               Country  = "Xalapa",
               Routine_Description = "Antros gay(drag) para agarrar un macanon",
               Visibility = "private",
               label_category = "Party",
               State_Country = "Veracruz",
               Town = "Xalapa",
               Creator = "Elva J. Fernández",
               Budget = "$300"
            },
           new Routine
           {
               Name = "Cafeterias drag",
               Country  = "Xalapa",
               Routine_Description = "Antros gay(drag) para agarrar un macanon",
               Visibility = "private",
               label_category = "Party",
               State_Country = "Veracruz",
               Town = "Xalapa",
               Creator = "Elva J. Fernández",
               Budget = "$300"
            },
           new Routine
           {
               Name = "Cafeterias drag",
               Country  = "Xalapa",
               Routine_Description = "Antros gay(drag) para agarrar un macanon",
               Visibility = "private",
               label_category = "Party",
               State_Country = "Veracruz",
               Town = "Xalapa",
               Creator = "Elva J. Fernández",
               Budget = "$300"
            },
           new Routine
           {
               Name = "Cafeterias drag",
               Country  = "Xalapa",
               Routine_Description = "Antros gay(drag) para agarrar un macanon",
               Visibility = "private",
               label_category = "Party",
               State_Country = "Veracruz",
               Town = "Xalapa",
               Creator = "Elva J. Fernández",
               Budget = "$300"
            },
           new Routine
           {
               Name = "Cafeterias drag",
               Country  = "Xalapa",
               Routine_Description = "Antros gay(drag) para agarrar un macanon",
               Visibility = "private",
               label_category = "Party",
               State_Country = "Veracruz",
               Town = "Xalapa",
               Creator = "Elva J. Fernández",
               Budget = "$300"
            }
        };
        public Page_Explorer()
        {
            InitializeComponent();
            List_Routines.ItemsSource = routines;
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
