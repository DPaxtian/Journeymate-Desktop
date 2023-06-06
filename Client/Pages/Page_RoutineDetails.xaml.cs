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
    /// Lógica de interacción para Page_RoutineDetails.xaml
    /// </summary>
    public partial class Page_RoutineDetails : Page
    {
        public static Routine routineDetails;
        List<Models.Task> tasks = new List<Models.Task>();

        public Page_RoutineDetails()
        {
            InitializeComponent();
            SetRoutineInfo();
            SetTasksInfo();
        }

        private void Button_DeleteTaks_Clic(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_EditTaks_Clic(object sender, MouseButtonEventArgs e)
        {

        }


        private void SetRoutineInfo()
        {
            if (routineDetails != null)
            {
                Label_RoutineTitle.Content = routineDetails.Name;
                Label_RoutineCreator.Content = routineDetails.Routine_Creator;
                Label_RoutineUbication.Content = routineDetails.City + ", " + routineDetails.Country;
                Label_RoutineCategory.Content = routineDetails.Label_Category;
                Label_RoutineBudget.Content = 0;
                Label_RoutineFollowers.Content = routineDetails.Followers;
                Label_RoutineDescription.Text = routineDetails.Routine_Description;
            }
        }


        private async void SetTasksInfo()
        {
            tasks = await TaskLogic.GetTasksByRoutineId(routineDetails.Id);
            List_Tasks.ItemsSource = tasks;
        }
    }
}
