using Logic;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
using System.Windows.Threading;

namespace Client.Pages
{
    /// <summary>
    /// Lógica de interacción para Page_SignUp.xaml
    /// </summary>
    public partial class Page_SignUp : Page
    {
        public Page_SignUp()
        {
            InitializeComponent();
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

        private void Validate_Only_Numbers(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                //Cancel entered key if isn't a digit
                e.Handled = true;
            }
            else
            {
                // Parse the entered text as a number
                if (int.TryParse(((TextBox)sender).Text + e.Text, out int age))
                {
                    // Check if the age is greater than or equal to 90
                    if (age >= 90)
                    {
                        e.Handled = true;
                    }
                }
            }
        }


        private int ValidateData(User userToSignUp, string passwordRepeated)
        {
            int resultValidation =  (int)StatusCode.Ok;

            if (userToSignUp.Age  <= 13 || userToSignUp.Age >= 90)
            {
                resultValidation = (int)StatusCode.EntryError;
                Label_Error_Age.Visibility = Visibility.Visible;

            }

            if (!Regex.IsMatch(userToSignUp.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                resultValidation = (int)StatusCode.EntryError;
                Label_Error_Email.Content = "El correo es invalido";
                Label_Error_Email.Visibility = Visibility.Visible;

            }

            if (!Regex.IsMatch(userToSignUp.Password, @"^(?=.*[A-Z])(?=.*\d).+$"))
            {
                resultValidation = (int)StatusCode.EntryError;
                Label_Error_Password.Visibility = Visibility.Visible;

            }

            if (userToSignUp.Password != passwordRepeated)
            {
                resultValidation = (int)StatusCode.EntryError;
                Label_Error_RepeatPassword.Visibility = Visibility.Visible;

            }

            return resultValidation;
        }


        private int ValidateNotEmptyData()
        {

            int resultValidation = (int)StatusCode.Ok;

            if (string.IsNullOrWhiteSpace(TextBox_Name.Text))
            {
                resultValidation = (int)StatusCode.EntryError;
                Label_Error_Name.Visibility = Visibility.Visible;
            }

            if (string.IsNullOrWhiteSpace(TextBox_Lastname.Text))
            {
                resultValidation = (int)StatusCode.EntryError;
                Label_Error_Lastname.Visibility = Visibility.Visible;

            }

            if (string.IsNullOrWhiteSpace(TextBox_Age.Text))
            {
                resultValidation = (int)StatusCode.EntryError;
                Label_Error_Age.Visibility = Visibility.Visible;

            }

            if (string.IsNullOrWhiteSpace(TextBox_Email.Text))
            {
                resultValidation = (int)StatusCode.EntryError;
                Label_Error_Email.Content = "El correo es invalido";
                Label_Error_Email.Visibility = Visibility.Visible;

            }

            if (string.IsNullOrWhiteSpace(PasswordBox_Password.Password))
            {
                resultValidation = (int)StatusCode.EntryError;
                Label_Error_Password.Visibility = Visibility.Visible;

            }

            if (string.IsNullOrWhiteSpace(PasswordBox_RepeatPassword.Password))
            {
                resultValidation = (int)StatusCode.EntryError;
                Label_Error_RepeatPassword.Visibility = Visibility.Visible;

            }

            return (int)resultValidation;
        }


        private string GenerateUsername()
        {
            Random random = new Random();
            string[] names = TextBox_Name.Text.Split(' ');
            string[] lastnames = TextBox_Lastname.Text.Split(' ');
            string username = names[0] + lastnames[0] + random.Next(1, 1001).ToString();

            return username;
        }


        private async void Button_SignUp_Clic(object sender, RoutedEventArgs e)
        {
            Label_Error_Email.Visibility = Visibility.Hidden;
            Label_Error_EmailWasRegistered.Visibility = Visibility.Collapsed;
            if (ValidateNotEmptyData() == (int)StatusCode.Ok)
            {
                User userToSignUp = new User
                {
                    Name = TextBox_Name.Text,
                    Lastname = TextBox_Lastname.Text,
                    Age = Int32.Parse(TextBox_Age.Text),
                    Email = TextBox_Email.Text,
                    Password = PasswordBox_Password.Password,
                    Username = GenerateUsername()
                };

                if (ValidateData(userToSignUp, PasswordBox_RepeatPassword.Password) == (int)StatusCode.Ok)
                {
                    int codeStatus = await UserLogic.SignUp(userToSignUp);
                    if (codeStatus == (int)StatusCode.Ok)
                    {
                        MainWindow.Instance.Frame_Page.Navigate(new Uri("/Pages/Page_Login.xaml", UriKind.Relative));
                    }if(codeStatus == (int)StatusCode.Conflict)
                    {
                        Label_Error_Email.Content = "El correo fue previamente registrado";
                        Label_Error_Email.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Message_SignUpError.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        

        private void Button_Login_Click(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Instance.Frame_Page.Navigate(new Uri("/Pages/Page_Login.xaml", UriKind.Relative));
        }

        private void TextBox_OnlyCharacters(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[a-zA-Z]+$");
            TextBox textBox = (TextBox)sender;
            string newText = textBox.Text + e.Text;

            if (newText.Length > 50)
            {
                e.Handled = true;
            }
        }


        private void TextChanged_TextBox_Name(object sender, TextChangedEventArgs e)
        {
            Label_Error_Name.Visibility = Visibility.Hidden;
        }

        private void TextChanged_TextBox_Lastname(object sender, TextChangedEventArgs e)
        {
            Label_Error_Lastname.Visibility = Visibility.Hidden;

        }

        private void TextChanged_TextBox_Age(object sender, TextChangedEventArgs e)
        {
            Label_Error_Age.Visibility = Visibility.Hidden;
        }

        private void TextChanged_TextBox_Email(object sender, TextChangedEventArgs e)
        {
            Label_Error_Email.Visibility = Visibility.Hidden;
        }

        private void TextChanged_TextBox_Password(object sender, TextChangedEventArgs e)
        {
            Label_Error_Password.Visibility = Visibility.Hidden;
        }

        private void PasswordChanged_TextBox_RepeatPassword(object sender, RoutedEventArgs e)
        {
            Label_Error_RepeatPassword.Visibility = Visibility.Hidden;

        }
    }
}
