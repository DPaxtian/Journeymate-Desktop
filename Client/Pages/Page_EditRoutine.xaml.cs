using Logic;
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
    /// Lógica de interacción para EditRoutine.xaml
    /// </summary>
    public partial class Page_EditRoutine : Page
    {

        public static Routine routine;

        public Page_EditRoutine()
        {
            InitializeComponent();
            FillVisibilityComboBox();
            SetRoutineInfo();
            FillCategoryComboBox();
        }

        private async void Button_UpdateRoutine_Clic(object sender, RoutedEventArgs e)
        {
            int statusCode = 500;

            if (ValidateFields())
            {
                Routine routineUpdated = new Routine
                {
                    Name = TextBox_RoutineTitle.Text,
                    City = TextBox_City.Text,
                    Country = TextBox_Country.Text,
                    State_Country = TextBox_StateCountry.Text,
                    Town = TextBox_Town.Text,
                    Label_Category = ComboBox_LabelCategory.SelectedItem.ToString(),
                    Visibility = SetVisibilityField(),
                    Routine_Description = TextBox_RoutineDescription.Text,
                    Followers = routine.Followers,
                    Routine_Creator = MainWindow.UserLogged.Username
                };

                statusCode = await RoutineLogic.UpdateRoutine(routine.Id, routineUpdated);

                if(statusCode == 200)
                {
                    MessageBox.Show("Rutina actualizada con exito!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow.Instance.Frame_Page.Navigate(new Uri("/Pages/Page_MyLists.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Hubo un error actualizando la rutina", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void Button_Cancel_Clic(object sender, RoutedEventArgs e)
        {
            var messageResult = MessageBox.Show("¿Cancelar la edicion de la rutina? Los cambios realizados se perderan", "Cancelar", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (messageResult == MessageBoxResult.OK)
            {
                MainWindow.Instance.Frame_Page.Navigate(new Uri("/Pages/Page_MyLists.xaml", UriKind.Relative));
            }
        }


        private void SetRoutineInfo()
        {
            if (routine != null)
            {
                TextBox_RoutineTitle.Text = routine.Name;
                TextBox_City.Text = routine.City;
                TextBox_Country.Text = routine.Country;
                TextBox_StateCountry.Text = routine.State_Country;
                TextBox_Town.Text = routine.Town;
                ComboBox_LabelCategory.SelectedItem = routine.Label_Category;
                SetVisibilityValue();
                TextBox_RoutineDescription.Text = routine.Routine_Description;
                
            }
        }


        private void SetVisibilityValue()
        {
            if (routine.Visibility.Equals("public"))
            {
                ComboBox_Visibilty.SelectedIndex = 0;
            }
            if (routine.Visibility.Equals("private"))
            {
                ComboBox_Visibilty.SelectedIndex = 1;
            }
        }


        private void FillVisibilityComboBox()
        {
            List<string> states = new List<string>
            {
                "Publica",
                "Privada"
            };

            ComboBox_Visibilty.ItemsSource = states;
        }


        private string SetVisibilityField()
        {
            string visibility = "";

            if (ComboBox_Visibilty.SelectedItem.ToString().Equals("Publica"))
            {
                visibility = "public";
            }
            if (ComboBox_Visibilty.SelectedItem.ToString().Equals("Privada"))
            {
                visibility = "private";
            }

            return visibility;
        }


        private void FillCategoryComboBox()
        {
            List<string> categories = new List<string>
            {
                "Turismo",
                "Gastronomía",
                "Compras",
                "Entretenimiento",
                "Actividades al aire libre",
                "Vida nocturna",
                "Arte y cultura",
                "Naturaleza y espacios verdes",
                "Actividades educativas",
                "Deportes y recreación",
                "Relajación y bienestar",
                "Eventos culturales",
                "Aventura",
                "Actividades acuaticas",
                "Educación y aprendizaje"
            };

            ComboBox_LabelCategory.ItemsSource = categories;
        }


        private bool ValidateFields()
        {
            bool isValid = true;

            HideErrorLabels();
            if (TextBox_RoutineTitle.Text.Equals(""))
            {
                LabelFieldError.Visibility = Visibility.Visible;
                LabelTitleRoutineError.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (TextBox_City.Text.Equals(""))
            {
                LabelFieldError.Visibility = Visibility.Visible;
                LabelCityError.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (TextBox_Country.Text.Equals(""))
            {
                LabelFieldError.Visibility = Visibility.Visible;
                LabelCountryError.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (TextBox_StateCountry.Text.Equals(""))
            {
                LabelFieldError.Visibility = Visibility.Visible;
                LabelStateError.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (TextBox_Town.Text.Equals(""))
            {
                LabelFieldError.Visibility = Visibility.Visible;
                LabelTownError.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (ComboBox_LabelCategory.SelectedItem.ToString().Equals(""))
            {
                LabelFieldError.Visibility = Visibility.Visible;
                LabelCategoryError.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (ComboBox_Visibilty.SelectedItem == null)
            {
                LabelFieldError.Visibility = Visibility.Visible;
                LabelVisibilityError.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (TextBox_RoutineDescription.Text.Equals(""))
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
            LabelTitleRoutineError.Visibility = Visibility.Hidden;
            LabelCityError.Visibility = Visibility.Hidden;
            LabelCountryError.Visibility = Visibility.Hidden;
            LabelStateError.Visibility = Visibility.Hidden;
            LabelTownError.Visibility = Visibility.Hidden;
            LabelCountryError.Visibility = Visibility.Hidden;
            LabelTownError.Visibility = Visibility.Hidden;
            LabelCategoryError.Visibility = Visibility.Hidden;
            LabelVisibilityError.Visibility = Visibility.Hidden;
            LabelDescriptionError.Visibility = Visibility.Hidden;
        }

        
    }
}
