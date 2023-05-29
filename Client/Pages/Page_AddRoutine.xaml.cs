using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para Page_AddRoutine.xaml
    /// </summary>
    public partial class Page_AddRoutine : Page
    {
        ObservableCollection<String> tasks = new ObservableCollection<String>();
        public Page_AddRoutine()
        {
            InitializeComponent();
        }

        private void Button_AddTask_Clic(object sender, RoutedEventArgs e)
        {
            
            tasks.Add("1");
            ListBox_TasksCards.ItemsSource = null;
            ListBox_TasksCards.ItemsSource = tasks;
        }
    }
}
