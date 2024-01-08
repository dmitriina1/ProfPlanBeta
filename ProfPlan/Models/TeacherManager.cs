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
        public static ObservableCollection<Teacher> _DatabaseUsers = new ObservableCollection<Teacher>() { new Teacher() { LastName = "Дмитриевич", FirstName = "Диман"} };

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
