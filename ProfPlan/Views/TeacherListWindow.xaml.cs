using ProfPlan.ViewModels;
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
using System.Windows.Shapes;

namespace ProfPlan.Views
{
    /// <summary>
    /// Логика взаимодействия для TeacherListWindow.xaml
    /// </summary>
    public partial class TeacherListWindow : Window
    {
        public TeacherListWindow()
        {
            InitializeComponent();
            TeachersListViewModel TeacherListWindow = new TeachersListViewModel();
            this.DataContext = TeacherListWindow;
        }

    }
}
