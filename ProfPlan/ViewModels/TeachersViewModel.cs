using ProfPlan.Commads;
using ProfPlan.Models;
using ProfPlan.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ProfPlan.Models;
using ProfPlan.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProfPlan.ViewModels
{
    public class TeachersViewModel
    {
        public ObservableCollection<Teacher> Teachers { get; set; }


        public ICommand ShowWindowCommand { get; set; }
        public TeachersViewModel()
        {
            Teachers = TeacherManager.GetTeachers();

            ShowWindowCommand = new RelayCommand(ShowWindow, CanShowWindow);

        }

        private bool CanShowWindow(object obj)
        {
            return true;
        }

        private void ShowWindow(object obj)
        {
            var mainWindow = obj as Window;

            TeacherAddWindow addUserWin = new TeacherAddWindow();
            addUserWin.Owner = mainWindow;
            addUserWin.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addUserWin.ShowDialog();


        }
        private Teacher _selectedUser;
        public Teacher SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void RemoveSelectedUser(Teacher teacher)
        {
            if (MessageBox.Show($"Вы уверены, что хотите удалить пользователя {teacher.LastName} {teacher.FirstName} {teacher.MiddleName}?", "Удаление пользователя", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Удаление пользователя из коллекции и обновление представления
                Teachers.Remove(teacher);
            }
        }
    }
}
