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
using ProfPlan.Models;
using ProfPlan.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;

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
            
        }
        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            UserList.Items.Filter = FilterMethod;


        }

        private bool FilterMethod(object obj)
        {
            //var user = (Teacher)obj;

            //return user.FirstName.Contains(FilterTextBox.Text);
            return false;
            //return user.FirstName.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);

        }
        private void UserListViewItem_DoubleClick(object sender, RoutedEventArgs e)
        {
            if (UserList.SelectedItem is Teacher selectedUser)
            {
                TeachersViewModel mainViewModel = DataContext as TeachersViewModel;
                mainViewModel.SelectedUser = selectedUser;

                AddTeacherViewModel addUserViewModel = new AddTeacherViewModel();
                addUserViewModel.SetUser(mainViewModel.SelectedUser);

                TeacherAddWindow addUserWin = new TeacherAddWindow();
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
                TeachersViewModel mainViewModel = DataContext as TeachersViewModel;

                // Определяем, находится ли курсор над элементом ListView
                HitTestResult hitTestResult = VisualTreeHelper.HitTest(UserList, e.GetPosition(UserList));
                if (hitTestResult.VisualHit is FrameworkElement element && element.DataContext is Teacher selectedUser)
                {
                    // Вызываем метод удаления элемента из MainViewModel
                    mainViewModel.RemoveSelectedUser(selectedUser);
                }
            }
        }

        
    }
}
