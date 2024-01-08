using ProfPlan.Commads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ProfPlan.Models;

namespace ProfPlan.ViewModels
{
    public class AddTeacherViewModel
    {
        public ICommand AddTeacherCommand { get; set; }

        public string Institute { get; set; }
        public string Department { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Position { get; set; }
        public string AcademicDegree { get; set; }
        public string Workload { get; set; }
        private bool CanAdd = true;
        private Teacher existingTeacher { get; set; }


        public AddTeacherViewModel()
        {
            AddTeacherCommand = new RelayCommand(AddTeacher, CanAddTeacher);
        }

        private bool CanAddTeacher(object obj)
        {
            return true;
        }

        private void AddTeacher(object obj)
        {
            Teacher checkUser = TeacherManager.GetTeacherByName(Institute, Department, LastName, FirstName, MiddleName, Position);
            if (existingTeacher == null && CanAdd == true && checkUser == null)
            {
                TeacherManager.AddTeacher(new Teacher() { Institute = Institute, Department = Department, LastName = LastName, FirstName = FirstName, MiddleName = MiddleName, Position = Position, AcademicDegree = AcademicDegree, Workload = Workload });
            }
            else
            {
                if (existingTeacher == null)
                {
                    existingTeacher = TeacherManager.GetTeacherByName(Institute, Department, LastName, FirstName, MiddleName, Position);

                }
                existingTeacher.Institute = Institute;
                existingTeacher.Department = Department;
                existingTeacher.LastName = LastName;
                existingTeacher.FirstName = FirstName;
                existingTeacher.MiddleName = MiddleName;
                existingTeacher.Position = Position;
                existingTeacher.AcademicDegree = AcademicDegree;
                existingTeacher.Workload = Workload;

                MessageBox.Show("Данные пользователя обновлены.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
        public void SetUser(Teacher teacher)
        {
            Institute = teacher?.Institute;
            Department = teacher?.Department;
            LastName = teacher?.LastName;
            FirstName = teacher?.FirstName;
            MiddleName = teacher?.MiddleName;
            Position = teacher?.Position;
            AcademicDegree = teacher?.AcademicDegree;
            Workload = teacher?.Workload;
            existingTeacher = TeacherManager.GetTeacherByName(Institute, Department, LastName, FirstName, MiddleName, Position);
            if (existingTeacher == null)
            {
                CanAdd = true;
            }
            else
            {
                CanAdd = false;
            }
        }
    }
}
