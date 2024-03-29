﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ProfPlan.ViewModels.Base;

namespace ProfPlan.Models
{
    public class ExcelModel : ExcelData
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
        public int? GroupCount { get; set; }
        public string SubGroup { get; set; }
        public string FormOfStudy { get; set; }
        public int? StudentsCount { get; set; }
        public int? CommercicalStudentsCount { get; set; }
        public int? Weeks { get; set; }
        public string ReportingForm { get; set; }
        public double? Lectures { get; set; }
        public double? Practices { get; set; }
        public double? Laboratory { get; set; }
        public double? Consultations { get; set; }
        public double? Tests { get; set; }
        public double? Exams { get; set; }
        public double? CourseWorks { get; set; }
        public double? CourseProjects { get; set; }
        public double? GEKAndGAK { get; set; }
        public double? Diploma { get; set; }
        public double? RGZ { get; set; }
        public double? ReviewDiploma { get; set; }
        public double? Other { get; set; }
        public double? Total { get; set; }
        public double? Budget { get; set; }
        public double? Commercial { get; set; }
        public ExcelModel(ObservableCollection<string> teachlist,
            int number, string teacher, string discipline, string term,
            string group, string institute, int? groupCount, string subGroup,
            string formOfStudy, int? studentsCount, int? commercicalStudentsCount,
            int? weeks, string reportingForm, double? lectures, double? practices,
            double? laboratory, double? consultations, double? tests, double? exams,
            double? courseWorks, double? courseProjects, double? gEKAndGAK, double? diploma,
            double? rGZ, double? reviewDiploma, double? other, double? total, double? budget,
            double? commercial)
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

        public double SumProperties()
        {
            return (Lectures ?? 0) +
                       (Consultations ?? 0) +
                       (Laboratory ?? 0) +
                       (Practices ?? 0) +
                       (Tests ?? 0) +
                       (Exams ?? 0) +
                       (CourseProjects ?? 0) +
                       (CourseWorks ?? 0) +
                       (Diploma ?? 0) +
                       (RGZ ?? 0) +
                       (GEKAndGAK ?? 0) +
                       (ReviewDiploma ?? 0) +
                       (Other ?? 0);
        }

    }
}
