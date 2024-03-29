﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
using Logic;
using MaterialDesignThemes.Wpf;
using Models;

namespace Client.Pages
{
    /// <summary>
    /// Lógica de interacción para Page_AddRoutine.xaml
    /// </summary>
    public partial class Page_AddRoutine : Page
    {
        static Routine routineToUpdate = new Routine();
        List<Models.Task> createdTasks = new List<Models.Task>();
        ObservableCollection<Models.Task> observableTasks = new ObservableCollection<Models.Task>();

        public Page_AddRoutine()
        {
            InitializeComponent();
            FillVisibilityComboBox();
            FillCategoryComboBox();
        }


        private void Button_AddTask_Clic(object sender, RoutedEventArgs e)
        {
            observableTasks.Add(new Models.Task());
            ListBox_TasksCards.ItemsSource = null;
            ListBox_TasksCards.ItemsSource = observableTasks;
        }


        private void Button_Cancel_Clic(object sender, RoutedEventArgs e)
        {
            var messageResult = MessageBox.Show("¿Cancelar el registro de rutina? Los cambios realizados se perderan", "Cancelar", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if(messageResult == MessageBoxResult.OK)
            {
                MainWindow.Instance.NavigateToExplorer();
            }
        }


        private async void Button_SaveRoutine_Clic(object sender, RoutedEventArgs e)
        {
            int statusCode = 500;

            if (ValidateFields())
            {
                Routine routineCreated = new Routine
                {
                    Name = TextBox_RoutineTitle.Text,
                    City = TextBox_City.Text,
                    Country = TextBox_Country.Text,
                    State_Country = TextBox_StateCountry.Text,
                    Town = TextBox_Town.Text,
                    Label_Category = ComboBox_LabelCategory.SelectedItem.ToString(),
                    Visibility = SetVisibilityField(),
                    Routine_Description = TextBox_RoutineDescription.Text
                };

                string idRoutineCreated = null;
                if (ListBox_TasksCards.Items.Count > 0)
                {
                    idRoutineCreated = await RoutineLogic.SaveRoutine(MainWindow.UserLogged.Username, routineCreated);

                    if (idRoutineCreated != null)
                    {
                        if (SaveTasksCreated())
                        {
                            foreach(Models.Task task in createdTasks)
                            {
                                statusCode = await TaskLogic.AddNewTaskToRoutine(idRoutineCreated, task);
                            }
                        }
                    }
                    
                }
                else
                {
                    idRoutineCreated = await RoutineLogic.SaveRoutine(MainWindow.UserLogged.Username, routineCreated);
                    if (idRoutineCreated != null)
                    {
                        statusCode = (int)StatusCode.Ok;
                    }
                }


                if(statusCode == (int)StatusCode.Ok && idRoutineCreated != null)
                {
                    MessageBox.Show("Rutina guardada con exito!", "Exito" ,MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigateToMyListsPages();
                }
                else if(idRoutineCreated != null && statusCode != (int)StatusCode.Ok)
                {
                    MessageBox.Show("Ups hubo un error al registrar tus tareas, sin embargo hemos registrado tu rutina y estamos trabajando para solucionar el problma, por favor registra tus tareas mas tarde", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (idRoutineCreated == null && statusCode == (int)StatusCode.ProccessError)
                {
                    MessageBox.Show("Ups estmaos presentando problemas, por favor intentalo mas tarde", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }


        private void Button_DeleteTask_Clic(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            DependencyObject container = VisualTreeHelper.GetParent(button);

            while (!(container is ListBoxItem) && container != null)
            {
                container = VisualTreeHelper.GetParent(container);
            }

            if (container is ListBoxItem listBoxItem)
            {
                int index = ListBox_TasksCards.ItemContainerGenerator.IndexFromContainer(listBoxItem);
                observableTasks.RemoveAt(index);
                ListBox_TasksCards.ItemsSource = null;
                ListBox_TasksCards.ItemsSource = observableTasks;
            }
        }


        private void NavigateToMyListsPages()
        {
            MainWindow.Instance.TabPages.SelectedItem = MainWindow.Instance.TabItem_MyLists;
        }

        private bool SaveTasksCreated()
        {
            bool isValid = true;

            if (ListBox_TasksCards.Items.Count > 0)
            {
                foreach (var item in ListBox_TasksCards.Items)
                {
                    ListBoxItem listBoxItem = (ListBoxItem)(ListBox_TasksCards.ItemContainerGenerator.ContainerFromItem(item));
                    ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(listBoxItem);
                    DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;

                    TextBox taskTitle = (TextBox)myDataTemplate.FindName("TextBox_TaskTitle", myContentPresenter);
                    TextBox taskAddress = (TextBox)myDataTemplate.FindName("TextBox_Address", myContentPresenter);
                    TextBox taskBudget = (TextBox)myDataTemplate.FindName("TextBox_Budget", myContentPresenter);
                    TextBox taskDescription = (TextBox)myDataTemplate.FindName("TextBox_TaskDescription", myContentPresenter);

                    Label title = (Label)myDataTemplate.FindName("LabelTaskTitleError", myContentPresenter);
                    Label address = (Label)myDataTemplate.FindName("LabelTaskAddressError", myContentPresenter);
                    Label budget = (Label)myDataTemplate.FindName("LabelTaskBudgetError", myContentPresenter);
                    Label description = (Label)myDataTemplate.FindName("LabelTaskDescriptionError", myContentPresenter);

                    title.Visibility = Visibility.Hidden;
                    address.Visibility = Visibility.Hidden;
                    budget.Visibility = Visibility.Hidden;
                    description.Visibility = Visibility.Hidden;

                    if (taskTitle.Text.Equals(""))
                    {
                        title.Visibility = Visibility.Visible;
                        isValid = false;
                    }
                    if (taskAddress.Text.Equals(""))
                    {   
                        address.Visibility = Visibility.Visible;
                        isValid = false;
                    }
                    if (taskBudget.Text.Equals(""))
                    {   
                        budget.Visibility = Visibility.Visible;
                        isValid = false;
                    }
                    if (taskDescription.Text.Equals(""))
                    {   
                        description.Visibility = Visibility.Visible;
                        isValid = false;
                    }
                }


                if (isValid)
                {
                    foreach (var item in ListBox_TasksCards.Items)
                    {
                        ListBoxItem listBoxItem = (ListBoxItem)(ListBox_TasksCards.ItemContainerGenerator.ContainerFromItem(item));

                        ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(listBoxItem);
                        DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                        TextBox taskTitle = (TextBox)myDataTemplate.FindName("TextBox_TaskTitle", myContentPresenter);
                        TextBox taskAddress = (TextBox)myDataTemplate.FindName("TextBox_Address", myContentPresenter);
                        TextBox taskBudget = (TextBox)myDataTemplate.FindName("TextBox_Budget", myContentPresenter);
                        TextBox taskDescription = (TextBox)myDataTemplate.FindName("TextBox_TaskDescription", myContentPresenter);

                        Models.Task task = new Models.Task
                        {
                            Name = taskTitle.Text,
                            Address = taskAddress.Text,
                            Budget = Int32.Parse(taskBudget.Text),
                            IsCompleted = false,
                            Task_Description = taskDescription.Text,
                        };

                        createdTasks.Add(task);
                    }
                }
            }
            return isValid;
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
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
            if(ComboBox_Visibilty.SelectedItem == null)
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
