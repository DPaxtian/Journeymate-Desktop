using Logic;
using MaterialDesignThemes.Wpf;
using Models;
using System;
using System.Collections.Generic;
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

namespace Client.Pages
{
    /// <summary>
    /// Lógica de interacción para Page_RoutineDetails.xaml
    /// </summary>
    public partial class Page_RoutineDetails : Page
    {
        public static Routine routineDetails;
        List<Models.Task> tasks = new List<Models.Task>();
        private Models.Task taskToEdit = null;
        public Page_RoutineDetails()
        {
            InitializeComponent();
            SetRoutineInfo();
            SetTasksInfo();
        }

        private async void Button_DeleteTaks_Clic(object sender, MouseButtonEventArgs e)
        {
            Models.Task taskRecovered = FindTaskOnListView(sender);
            
            try
            {
                if (taskRecovered != null)
                {
                    int codeStatus = await TaskLogic.DeleteTask(taskRecovered._Id);
                    if(codeStatus == (int)StatusCode.Ok)
                    {
                        SetTasksInfo();
                    }
                    else
                    {
                        MessageBox.Show("Estamos presentando problemas, intentelo mas tarde", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void Button_EditTaks_Clic(object sender, MouseButtonEventArgs e)
        {
            Models.Task taskRecovered = FindTaskOnListView(sender);
            if (taskRecovered != null)
            {
                NewTaskScreen.Visibility = Visibility.Visible;
                LabelTittleScreenAddEdit.Content = "Edita tu tarea";
                ButtonSaveNewTask.Visibility = Visibility.Collapsed;
                ButtonEditTask.Visibility = Visibility.Visible;
                TextBox_TaskTitle.Text = taskRecovered.Name;
                TextBox_Address.Text = taskRecovered.Address;
                TextBox_Budget.Text = taskRecovered.Budget.ToString();
                TextBox_TaskDescription.Text = taskRecovered.Task_Description;
                taskToEdit = taskRecovered;
            }
        }


        private void SetRoutineInfo()
        {
            if (routineDetails != null)
            {
                Label_RoutineTitle.Content = routineDetails.Name;
                Label_RoutineCreator.Content = routineDetails.Routine_Creator;
                Label_RoutineUbication.Content = routineDetails.City + ", " + routineDetails.Country;
                Label_RoutineCategory.Content = routineDetails.Label_Category;
                Label_RoutineFollowers.Content = routineDetails.Followers;
                Label_RoutineDescription.Text = routineDetails.Routine_Description;
            }
        }


        private int CalculateRoutineBudget()
        {
            int budget = 0;

            foreach(var task in tasks)
            {
                budget += task.Budget;
            }

            return budget;
        }


        private async void SetTasksInfo()
        {
            tasks = await TaskLogic.GetTasksByRoutineId(routineDetails.Id);
            List_Tasks.ItemsSource = tasks;
            Label_RoutineBudget.Content = CalculateRoutineBudget().ToString();
        }


        private void Button_AddNewTask_Click(object sender, RoutedEventArgs e)
        {
            if (NewTaskScreen.Visibility == Visibility.Collapsed)
            {
                NewTaskScreen.Visibility = Visibility.Visible;
                ButtonAddTask.Content = "Ocultar registro";
            }
            else
            {
                ButtonAddTask.Content = "Agregar tarea";
                NewTaskScreen.Visibility = Visibility.Collapsed;
            }
        }


        private void Button_CancelAddTask_Click(object sender, MouseButtonEventArgs e)
        {
            ButtonAddTask.Content = "Agregar tarea";
            NewTaskScreen.Visibility = Visibility.Collapsed;
        }

        private async void Button_AddSaveTask_Click(object sender, MouseButtonEventArgs e)
        {
            if(validateData() == (int)StatusCode.Ok)
            {
                Models.Task taskCreated = new Models.Task
                {
                    Name = TextBox_TaskTitle.Text,
                    Address = TextBox_Address.Text,
                    Budget = Int32.Parse(TextBox_Budget.Text),
                    Task_Description = TextBox_TaskDescription.Text,
                    IsCompleted = false
                };

                try
                {
                    int codeStatus = await TaskLogic.AddNewTaskToRoutine(routineDetails.Id, taskCreated);
                    if (codeStatus == (int)StatusCode.Ok)
                    {
                        ButtonAddTask.Content = "Agregar tarea";
                        NewTaskScreen.Visibility = Visibility.Collapsed;
                        SetTasksInfo();
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }


            }
        }


        private int validateData()
        {
            int isValid = (int)StatusCode.Ok;

            LabelTaskTitleError.Visibility = Visibility.Hidden;
            LabelTaskAddressError.Visibility = Visibility.Hidden;
            LabelTaskBudgetError.Visibility = Visibility.Hidden;
            LabelTaskDescriptionError.Visibility = Visibility.Hidden;

            if (TextBox_TaskTitle.Text.Equals(""))
            {
                LabelTaskTitleError.Visibility = Visibility.Visible;
                isValid = (int)StatusCode.EntryError;
            }
            if (TextBox_Address.Text.Equals(""))
            {
                LabelTaskAddressError.Visibility = Visibility.Visible;
                isValid = (int)StatusCode.EntryError;
            }
            if (TextBox_Budget.Text.Equals(""))
            {
                LabelTaskBudgetError.Visibility = Visibility.Visible;
                isValid = (int)StatusCode.EntryError;
            }
            if (TextBox_TaskDescription.Text.Equals(""))
            {
                LabelTaskDescriptionError.Visibility = Visibility.Visible;
                isValid = (int)StatusCode.EntryError;

            }

            return isValid;
        }


        private void TextBox_ValidateOnlyNumberst(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private async void Button_CompleteTask_Click(object sender, MouseButtonEventArgs e)
        {
            Models.Task taskRecovered = FindTaskOnListView(sender);
            taskRecovered.IsCompleted = taskRecovered.IsCompleted ? false : true; 
            try
            {
                if (taskRecovered != null)
                {
                    int codeResult = await TaskLogic.EditTask(taskRecovered);
                    if (codeResult == (int)StatusCode.Ok)
                    {
                        SetTasksInfo();
                    }
                }
               
            }catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }


        private Models.Task FindTaskOnListView(object sender)
        {
            Image imageSelected = (Image)sender;
            Models.Task taskSelected = null;
            try
            {
                taskSelected = (Models.Task)imageSelected.DataContext;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return taskSelected;
        }

        private async void Button_SaveTaskChanges_Click(object sender, MouseButtonEventArgs e)
        {
            if (validateData() == (int)StatusCode.Ok)
            {
                Models.Task taskEdited= new Models.Task
                {
                    _Id = taskToEdit._Id,
                    Name = TextBox_TaskTitle.Text,
                    Address = TextBox_Address.Text,
                    Budget = Int32.Parse(TextBox_Budget.Text),
                    Task_Description = TextBox_TaskDescription.Text,
                    IsCompleted = taskToEdit.IsCompleted
                };

                try
                {
                    int codeStatus = await TaskLogic.EditTask(taskEdited);
                    if (codeStatus == (int)StatusCode.Ok)
                    {
                        SetTasksInfo();
                    }
                    else
                    {
                        MessageBox.Show("Estamos presentando problemas, intentelo mas tarde", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }

            NewTaskScreen.Visibility = Visibility.Collapsed;
            ButtonSaveNewTask.Visibility = Visibility.Visible;
            ButtonEditTask.Visibility = Visibility.Collapsed;
            LabelTittleScreenAddEdit.Content = "Edita tu tarea";

        }


        private void Button_NavigateBack_Clic(object sender, RoutedEventArgs e)
        {
            if(MainWindow.Instance.TabPages.SelectedItem == MainWindow.Instance.TabItem_Explorer)
            {
                MainWindow.Instance.Frame_Page.Navigate(new Uri("/Pages/Page_Explorer.xaml", UriKind.Relative));
            }
            if (MainWindow.Instance.TabPages.SelectedItem == MainWindow.Instance.TabItem_MyLists)
            {
                MainWindow.Instance.Frame_Page.Navigate(new Uri("/Pages/Page_MyLists.xaml", UriKind.Relative));
            }
            if (MainWindow.Instance.TabPages.SelectedItem == MainWindow.Instance.TabItem_Favorites)
            {
                MainWindow.Instance.Frame_Page.Navigate(new Uri("/Pages/Page_Favorites.xaml", UriKind.Relative));
            }
        }
    }
}
