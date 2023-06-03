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
    /// Lógica de interacción para Page_Profile.xaml
    /// </summary>
    public partial class Page_Profile : Page
    {
        public Page_Profile()
        {
            InitializeComponent();
            Console.WriteLine(MainWindow.UserLogged.Name);
            SetUserInfo();
        }

        private void Button_EditProfile_Clic(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Instance.Frame_Page.Navigate(new Uri("/Pages/Page_EditProfile.xaml", UriKind.Relative));
        }

        private void SetUserInfo()
        {

            if(MainWindow.UserLogged != null)
            {
                User loggedUser = MainWindow.UserLogged;
                
                TextBlock_Name.Text = loggedUser.Name + " " + loggedUser.Lastname;
                Label_Username.Content = loggedUser.Username;
                Label_Email.Content = loggedUser.Email;
                Label_PhoneNumber.Content = loggedUser.PhoneNumber;
                Label_Age.Content = loggedUser.Age;
                Label_Location.Content = loggedUser.City + ", " + loggedUser.Country;
                Label_Description.Text = loggedUser.UserDescription;
            }
            
        }
    }
}
