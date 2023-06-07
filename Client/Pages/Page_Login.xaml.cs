using System;
using Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Net.Http;
using Logic;
using System.Threading.Tasks;

namespace Client.Pages
{
    /// <summary>
    /// Lógica de interacción para Page_Login.xaml
    /// </summary>
    public partial class Page_Login : Page
    {
        public Page_Login()
        {
            InitializeComponent();
        }


        private async void Button_Login_Clic(object sender, RoutedEventArgs e)
        {
            if (ValidateFormatEntryParams(TextBox_Email.Text) == (int)StatusCode.Ok || !string.IsNullOrEmpty(TextBox_Email.Text) || !string.IsNullOrEmpty(PasswordBox_Password.Password))
            {
                try
                {
                    User userToLogin = await Autentication.Login(TextBox_Email.Text, PasswordBox_Password.Password);
                    if (userToLogin != null)
                    {
                        MainWindow.UserLogged = userToLogin;
                        MainWindow.Instance.NavigateToExplorer();
                    }
                    else
                    {
                        Label_Login_Error.Visibility = Visibility.Visible;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There is an error to execute login method: "+ex);
                }
            }
            else
            {
                Label_Login_Error.Visibility = Visibility.Visible;
            }
        }


        private void Button_SignUp_Clic(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Instance.Frame_Page.Navigate(new Uri("/Pages/Page_SignUp.xaml", UriKind.Relative));
        }


        private int ValidateFormatEntryParams(String email)
        {
            int codeStatus = (int)StatusCode.ProccessError;
            Regex emailFormat = new Regex("^\\S+@\\S+\\.\\S+$");

            if (emailFormat.IsMatch(email))
            {
                codeStatus = (int)StatusCode.Ok;
            }

            return codeStatus;
        }


        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox TextBox = (TextBox)sender;
            TextBox.Padding = new Thickness(20, 0, 10, 0); // Ajusta el valor del Padding para el espacio deseado
            TextBox.VerticalContentAlignment = VerticalAlignment.Center;
        }

        private void PasswordBox_Loaded(object sender, RoutedEventArgs e)
        {
              PasswordBox passwordBox = (PasswordBox)sender;
              passwordBox.Padding = new Thickness(20, 0, 10, 0); // Ajusta el valor del Padding para el espacio deseado
              passwordBox.VerticalContentAlignment = VerticalAlignment.Center;
        }
    }
}
