using ProfPlan.Commads;
using ProfPlan.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProfPlan.ViewModels
{
    public class TeachersListViewModel: INotifyPropertyChanged
    {

        public ObservableCollection<Teacher> Teachers { get; set; }

        public ICommand ShowWindowCommand { get; set; }



        public TeachersListViewModel()
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
            MessageBox.Show("Класс");


        }


        private Teacher _selectedTeacher;
        public Teacher SelectedTeacher
        {
            get { return _selectedTeacher; }
            set
            {
                _selectedTeacher = value;
                OnPropertyChanged(nameof(SelectedTeacher));
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
