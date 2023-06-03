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
    /// Lógica de interacción para Page_EditProfile.xaml
    /// </summary>
    public partial class Page_EditProfile : Page
    {
        public Page_EditProfile()
        {
            InitializeComponent();
            SetUserData();
        }

        private void Button_SaveProfile_Clic(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Cancel_Clic(object sender, RoutedEventArgs e)
        {
            var clickedButton = MessageBox.Show("¿Desea cancelar? Los cambios realizados no se guardaran", "¿Cancelar?", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if(clickedButton == MessageBoxResult.OK)
            {
                MainWindow.Instance.Frame_Page.Navigate(new Uri("/Pages/Page_Profile.xaml", UriKind.Relative));
            }
        }


        private void SetUserData()
        {
            if(MainWindow.UserLogged != null)
            {
                User loggedUser = MainWindow.UserLogged;

                TextBox_Name.Text = loggedUser.name;
                TextBox_LastName.Text = loggedUser.lastname;
                TextBox_Email.Text = loggedUser.email;
                TextBox_PhoneNumber.Text = loggedUser.phone_number;
                TextBox_Country.Text = loggedUser.country;
                TextBox_City.Text = loggedUser.city;
                TextBox_Description.Text = loggedUser.user_description;
            }
        }
    }
}
