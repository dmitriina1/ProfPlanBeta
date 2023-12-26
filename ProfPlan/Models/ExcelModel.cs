using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfPlan.Models
{
    public class ExcelModel
    {
        public int Number { get; set; }
        public string Teacher { get; set; }
        public string Discipline { get; set; }

        public ExcelModel(int number, string teacher, string discipline)
        {
            Number = number;
            Teacher = teacher;
            Discipline = discipline;
        }
    }
}
