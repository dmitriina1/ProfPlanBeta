using ProfPlan.Models;
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
        private void UserListViewItem_DoubleClick(object sender, RoutedEventArgs e)
        {
            if (TeacherList.SelectedItem is Teacher selectedUser)
            {
                TeachersListViewModel mainViewModel = DataContext as TeachersListViewModel;
                mainViewModel.SelectedTeacher = selectedUser;

                AddTeacherViewModel addUserViewModel = new AddTeacherViewModel();
                addUserViewModel.SetTeacher(mainViewModel.SelectedTeacher);

                AddTeacherWindow addUserWin = new AddTeacherWindow();
                addUserWin.Owner = this;
                addUserWin.DataContext = addUserViewModel;
                addUserWin.WindowStartupLocation = WindowStartupLocation.CenterOwner;
				addUserWin.ShowDialog();
            }
        }
        private void UserList_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                TeachersListViewModel mainViewModel = DataContext as TeachersListViewModel;

                // Определяем, находится ли курсор над элементом ListView
                HitTestResult hitTestResult = VisualTreeHelper.HitTest(TeacherList, e.GetPosition(TeacherList));
                if (hitTestResult.VisualHit is FrameworkElement element && element.DataContext is Teacher selectedUser)
                {
                    // Вызываем метод удаления элемента из MainViewModel
                    mainViewModel.RemoveSelectedTeacher(selectedUser);
                }
            }
        }

    }
}
