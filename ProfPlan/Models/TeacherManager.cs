using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfPlan.Models
{
    public class TeacherManager
    {
        public static ObservableCollection<Teacher> _DatabaseUsers = new ObservableCollection<Teacher>() {
           new Teacher
        {
            Institute = "Institute1",
            Department = "Department1",
            LastName = "LastName1",
            FirstName = "FirstName1",
            MiddleName = "MiddleName1",
            Position = "Position1",
            AcademicDegree = "AcademicDegree1",
            Workload = "Workload1"
        },

         new Teacher
        {
            Institute = "Institute2",
            Department = "Department2",
            LastName = "LastName2",
            FirstName = "FirstName2",
            MiddleName = "MiddleName2",
            Position = "Position2",
            AcademicDegree = "AcademicDegree2",
            Workload = "Workload2"
        }
    
        };

        public static ObservableCollection<Teacher> GetTeachers()
        {
            return _DatabaseUsers;

        }


        public static void AddTeacher(Teacher teacher)
        {
            _DatabaseUsers.Add(teacher);

        }
        public static Teacher GetTeacherByName(string institute, string department, string lastname, string firstname, string middlename, string position)
        {
            return _DatabaseUsers.FirstOrDefault(teacher => (teacher.LastName == lastname || teacher.FirstName == firstname) && teacher.MiddleName == middlename && teacher.Institute == institute && teacher.Department == department && teacher.Position == position);
        }
    }
}
