using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if (ValidateFields())
            {

            }
        }

        private void Button_Cancel_Clic(object sender, RoutedEventArgs e)
        {
            var clickedButton = MessageBox.Show("¿Desea cancelar? Los cambios realizados no se guardaran", "¿Cancelar?", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if(clickedButton == MessageBoxResult.OK)
            {
                MainWindow.Instance.Frame_Page.Navigate(new Uri("/Pages/Page_Profile.xaml", UriKind.Relative));
            }
        }


        private bool ValidateFields()
        {
            bool isValid = true;
            string emailFormat = "^\\S+@\\S+\\.\\S+$";
            string lettersFormat = "^[A-Za-z]+$";

            HideErrorLabels();
            if(!Regex.IsMatch(TextBox_Name.Text, lettersFormat) || TextBox_Name.Text.Equals(""))
            {
                LabelFieldError.Visibility = Visibility.Visible;
                LabelNameError.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (!Regex.IsMatch(TextBox_LastName.Text, lettersFormat) || TextBox_LastName.Text.Equals(""))
            {
                LabelFieldError.Visibility = Visibility.Visible;
                LabelLastNameError.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (!Regex.IsMatch(TextBox_Email.Text, emailFormat) || TextBox_Email.Text.Equals(""))
            {
                LabelFieldError.Visibility = Visibility.Visible;
                LabelEmailError.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (TextBox_PhoneNumber.Text.Equals(""))
            {
                LabelFieldError.Visibility = Visibility.Visible;
                LabelPhoneNumberError.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (!Regex.IsMatch(TextBox_Country.Text, lettersFormat) || TextBox_Country.Text.Equals(""))
            {
                LabelFieldError.Visibility = Visibility.Visible;
                LabelCountryError.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (!Regex.IsMatch(TextBox_City.Text, lettersFormat) || TextBox_City.Text.Equals(""))
            {
                LabelFieldError.Visibility = Visibility.Visible;
                LabelCityError.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (TextBox_Description.Text.Equals(""))
            {
                LabelFieldError.Visibility = Visibility.Visible;
                LabelDescriptionError.Visibility = Visibility.Visible;
                isValid = false;
            }


            return isValid;
        }


        private void HideErrorLabels()
        {
            LabelFieldError.Visibility = Visibility.Hidden;
            LabelNameError.Visibility = Visibility.Hidden;
            LabelLastNameError.Visibility = Visibility.Hidden;
            LabelEmailError.Visibility = Visibility.Hidden;
            LabelPhoneNumberError.Visibility = Visibility.Hidden;
            LabelCountryError.Visibility = Visibility.Hidden;
            LabelCityError.Visibility = Visibility.Hidden;
            LabelDescriptionError.Visibility = Visibility.Hidden;
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

        private void ValidatePhoneNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
