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

        private void Button_SignUp_Clic(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Login_Click(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Instance.Frame_Page.Navigate(new Uri("/Pages/Page_Login.xaml", UriKind.Relative));
        }
    }
}
