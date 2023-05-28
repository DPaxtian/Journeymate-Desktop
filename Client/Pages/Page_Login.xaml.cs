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
    /// Lógica de interacción para Page_Login.xaml
    /// </summary>
    public partial class Page_Login : Page
    {
        public Page_Login()
        {
            InitializeComponent();
        }


        private void Button_Login_Clic(object sender, RoutedEventArgs e)
        {

        }

        private void Button_SignUp_Clic(object sender, MouseButtonEventArgs e)
        {

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
