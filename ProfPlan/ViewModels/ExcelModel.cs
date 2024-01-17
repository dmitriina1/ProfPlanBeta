using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfPlan.ViewModels.Base;

namespace ProfPlan.Models
{
    public class ExcelModel: ViewModel
    {
        private ObservableCollection<string> teachers = new ObservableCollection<string>();

        public ObservableCollection<string> Teachers
        {
            get { return teachers; }
            set
            {
                if (teachers != value)
                {
                    teachers = value;
                    OnPropertyChanged(nameof(Teachers));
                }
            }
        }
        public int Number { get; set; }
        public string Teacher { get; set; }
        public string Discipline { get; set; }
        public string Term { get; set; }
        public string Group { get; set; }
        public string Institute { get; set; }
        public string GroupCount { get; set; }
        public string SubGroup { get; set; }
        public string FormOfStudy { get; set; }
        public string StudentsCount { get; set; }
        public string CommercicalStudentsCount { get; set; }
        public string Weeks { get; set; }
        public string ReportingForm { get; set; }
        public string Lectures { get; set; }
        public string Practices { get; set; }
        public string Laboratory { get; set; }
        public string Consultations { get; set; }
        public string Tests { get; set; }
        public string Exams { get; set; }
        public string CourseWorks { get; set; }
        public string CourseProjects { get; set; }
        public string GEKAndGAK { get; set; }
        public string Diploma { get; set; }
        public string RGZ { get; set; }
        public string ReviewDiploma { get; set; }
        public string Other { get; set; }
        public string Total { get; set; }
        public string Budget { get; set; }
        public string Commercial { get; set; }
        public ExcelModel(ObservableCollection<string> teachlist, int number, string teacher, string discipline, string term, string group, string institute, string groupCount, string subGroup, string formOfStudy, string studentsCount, string commercicalStudentsCount, string weeks, string reportingForm, string lectures, string practices, string laboratory, string consultations, string tests, string exams, string courseWorks, string courseProjects, string gEKAndGAK, string diploma, string rGZ, string reviewDiploma, string other, string total, string budget, string commercial)
        {
            Teachers = teachlist;
            Number = number;
            Teacher = teacher;
            Discipline = discipline;
            Term = term;
            Group = group;
            Institute = institute;
            GroupCount = groupCount;
            SubGroup = subGroup;
            FormOfStudy = formOfStudy;
            StudentsCount = studentsCount;
            CommercicalStudentsCount = commercicalStudentsCount;
            Weeks = weeks;
            ReportingForm = reportingForm;
            Lectures = lectures;
            Practices = practices;
            Laboratory = laboratory;
            Consultations = consultations;
            Tests = tests;
            Exams = exams;
            CourseWorks = courseWorks;
            CourseProjects = courseProjects;
            GEKAndGAK = gEKAndGAK;
            Diploma = diploma;
            RGZ = rGZ;
            ReviewDiploma = reviewDiploma;
            Other = other;
            Total = total;
            Budget = budget;
            Commercial = commercial;
        }
        
    }
}
