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
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Position { get; set; }
        public string AcademicDegree { get; set; }
        public string Workload { get; set; }
        private bool CanAdd = true;
        private Teacher existingUser { get; set; }


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
            Teacher checkUser = TeacherManager.GetTeacherByName(Lastname, Firstname, Middlename);
            if (existingUser == null && CanAdd == true && checkUser == null)
            {
                TeacherManager.AddTeacher(new Teacher() { Institute = Institute, Department = Department, LastName = Lastname, FirstName = Firstname, MiddleName = Middlename, Position = Position, AcademicDegree = AcademicDegree, Workload = Workload });
            }
            else
            {
                if (existingUser == null)
                {
                    existingUser = TeacherManager.GetTeacherByName(Lastname, Firstname, Middlename);

                }
                existingUser.Institute = Institute;
                existingUser.Department = Department;
                existingUser.LastName = Lastname;
                existingUser.FirstName = Firstname;
                existingUser.MiddleName = Middlename;
                existingUser.Position = Position;
                existingUser.AcademicDegree = AcademicDegree;
                existingUser.Workload = Workload;

                MessageBox.Show("Данные пользователя обновлены.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
        public void SetTeacher(Teacher teacher)
        {
            Institute = teacher?.Institute;
            Department = teacher?.Department;
            Lastname = teacher?.LastName;
            Firstname = teacher?.FirstName;
            Middlename = teacher?.MiddleName;
            Position = teacher?.Position;
            AcademicDegree = teacher?.AcademicDegree;
            Workload = teacher?.Workload;
            existingUser = TeacherManager.GetTeacherByName(Lastname, Firstname, Middlename);
            if (existingUser == null)
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
