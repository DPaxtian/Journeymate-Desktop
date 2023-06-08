using Logic;
using Microsoft.Win32;
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
using System.IO;
using ImageService;

namespace Client.Pages
{
    /// <summary>
    /// Lógica de interacción para Page_EditProfile.xaml
    /// </summary>
    public partial class Page_EditProfile : Page
    {
        string pathImage;
        string nameImage = MainWindow.UserLogged.Username + "ProfilePic";


        public Page_EditProfile()
        {
            InitializeComponent();
            SetUserData();
        }

        private async void Button_SaveProfile_Clic(object sender, RoutedEventArgs e)
        {
            int statusCode = 500;
            if (ValidateFields())
            {
                User user = new User{
                    Username = MainWindow.UserLogged.Username,
                    Name = TextBox_Name.Text,
                    Lastname = TextBox_LastName.Text,
                    Email = TextBox_Email.Text,
                    PhoneNumber = TextBox_PhoneNumber.Text,
                    City = TextBox_City.Text,
                    Country= TextBox_Country.Text,
                    UserDescription = TextBox_Description.Text,
                    Age = MainWindow.UserLogged.Age
                };

                try
                {
                    if(pathImage != null)
                    {
                        ImageClient.UploadProfileImage(nameImage, pathImage);
                        statusCode = await UserLogic.EditProfile(user);
                    }
                    else
                    {
                        statusCode = await UserLogic.EditProfile(user);
                    }

                 
                    if (statusCode == 200)
                    {
                        UpdateUserLogged(MainWindow.UserLogged.Username);
                        EditProfileSuccessfull();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar el perfil", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("An error has been ocurred while updating the profile" + ex.Message);
                }
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
            Regex emailFormat = new Regex("^\\S+@\\S+\\.\\S+$");
            Regex lettersFormat = new Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$");
            

            HideErrorLabels();
            if(!lettersFormat.IsMatch(TextBox_Name.Text) || TextBox_Name.Text.Equals(""))
            {
                LabelFieldError.Visibility = Visibility.Visible;
                LabelNameError.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (!lettersFormat.IsMatch(TextBox_LastName.Text) || TextBox_LastName.Text.Equals(""))
            {
                LabelFieldError.Visibility = Visibility.Visible;
                LabelLastNameError.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (!emailFormat.IsMatch(TextBox_Email.Text) || TextBox_Email.Text.Equals(""))
            {
                LabelFieldError.Visibility = Visibility.Visible;
                LabelEmailError.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (TextBox_PhoneNumber.Text.Equals("") || TextBox_PhoneNumber.Text.Length < 10)
            {
                LabelFieldError.Visibility = Visibility.Visible;
                LabelPhoneNumberError.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (!lettersFormat.IsMatch(TextBox_Country.Text) || TextBox_Country.Text.Equals(""))
            {
                LabelFieldError.Visibility = Visibility.Visible;
                LabelCountryError.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (!lettersFormat.IsMatch(TextBox_City.Text) || TextBox_City.Text.Equals(""))
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

                TextBox_Name.Text = loggedUser.Name;
                TextBox_LastName.Text = loggedUser.Lastname;
                TextBox_Email.Text = loggedUser.Email;
                TextBox_PhoneNumber.Text = loggedUser.PhoneNumber;
                TextBox_Country.Text = loggedUser.Country;
                TextBox_City.Text = loggedUser.City;
                TextBox_Description.Text = loggedUser.UserDescription;
            }
        }


        private void ValidatePhoneNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void EditProfileSuccessfull()
        {
            MessageBox.Show("Usuario actualizado con exito", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            MainWindow.Instance.Frame_Page.Navigate(new Uri("/Pages/Page_Profile.xaml", UriKind.Relative));
        }


        private async void UpdateUserLogged(string username)
        {
            User userUpdated = await UserLogic.RecoverUserByUsername(username);

            if (userUpdated != null)
            {
                MainWindow.UserLogged = userUpdated;
            }
        }

        private void Button_SelectProfileImage_Clic(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Filtrar solo archivos de imagen
            openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                // Obtener la ruta y el nombre de la imagen seleccionada
                pathImage = openFileDialog.FileName;
                
            }
        }


    }
}
