using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ProfPlan.Commads;
using System.Windows.Input;
using ProfPlan.Models;
using ProfPlan.ViewModels;
using ProfPlan.ViewModels.Base;

namespace ProfPlan.ViewModels
{
    internal class ReportViewModel : ViewModel
    {
        //Получение таблиц из MainWindowViewModel;
        public ObservableCollection<TableCollection> TablesCollectionForReport { get; set; }
        public ObservableCollection<TableCollection> TablesCollectionForComboBox { get; set; }
        public ReportViewModel(ObservableCollection<TableCollection> TablesCollection)
        {
            TablesCollectionForReport = new ObservableCollection<TableCollection>();
            TablesCollectionForComboBox = new ObservableCollection<TableCollection>();

            foreach (var table in TablesCollection)
            {
                TablesCollectionForReport.Add(table);
                if (table.Tablename.IndexOf("ПИиИС", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    TablesCollectionForComboBox.Add(table);
                }
            }
        }
        /// <summary>
        /// ////////////
        /// </summary>
        
            private RelayCommand _ISumCommand;
        public ICommand ISumCommand
        {
            get { return _ISumCommand ?? (_ISumCommand = new RelayCommand(SumAllTeachersTables)); }
        }
        public ObservableCollection<TableCollection> TablesCollectionTeacherSum{ get; set; }
        private void SumAllTeachersTables(object parameter)
        {
            TablesCollectionTeacherSum = new ObservableCollection<TableCollection>();
            foreach (var tableCollection in TablesCollectionForReport)
            {
                if(tableCollection.Tablename.IndexOf("ПИиИС", StringComparison.OrdinalIgnoreCase) == -1 && tableCollection.Tablename.IndexOf("Итого", StringComparison.OrdinalIgnoreCase) == -1 && tableCollection.Tablename.IndexOf("Незаполненные", StringComparison.OrdinalIgnoreCase) == -1 && tableCollection.Tablename.IndexOf("Доп", StringComparison.OrdinalIgnoreCase) == -1)
                {
                    
                    ExcelModel sumEven = CalculateSum(tableCollection, "чет");
                    ExcelModel sumOdd = CalculateSum(tableCollection, "нечет");

                    // Create a new TableCollection for the sums and add it to TablesCollectionTeacherSum
                    TableCollection sumTableCollection = new TableCollection($"{tableCollection.Tablename}");
                    sumTableCollection.ExcelData.Add(sumEven);
                    sumTableCollection.ExcelData.Add(sumOdd);
                    TablesCollectionTeacherSum.Add(sumTableCollection);
                }
            }
        }
        private ExcelModel CalculateSum(TableCollection tableCollection, string term)
        {
            var sumModel = new ExcelModel(new ObservableCollection<string>(), 0, "", "", term, "", "", null, "", "", null, null, null,
                null, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            foreach (var excelModel in tableCollection.ExcelData.OfType<ExcelModel>().Where(x => x.Term != null && x.Term.Equals(term, StringComparison.OrdinalIgnoreCase)))
            {
                // Add the corresponding properties
                if(excelModel.Lectures!=null)
                sumModel.Lectures += excelModel.Lectures;
                if (excelModel.Consultations != null)
                    sumModel.Consultations += excelModel.Consultations;
                if (excelModel.Laboratory != null)
                    sumModel.Laboratory += excelModel.Laboratory;
                if (excelModel.Practices != null)
                    sumModel.Practices += excelModel.Practices;
                if (excelModel.Tests != null)
                    sumModel.Tests += excelModel.Tests;
                if (excelModel.Exams != null)
                    sumModel.Exams += excelModel.Exams;
                if (excelModel.CourseProjects != null)
                    sumModel.CourseProjects += excelModel.CourseProjects;
                if (excelModel.CourseWorks != null)
                    sumModel.CourseWorks += excelModel.CourseWorks;
                if (excelModel.Diploma != null)
                    sumModel.Diploma += excelModel.Diploma;
                if (excelModel.RGZ != null)
                    sumModel.RGZ += excelModel.RGZ;
                if (excelModel.GEKAndGAK != null)
                    sumModel.GEKAndGAK += excelModel.GEKAndGAK;
                if (excelModel.ReviewDiploma != null)
                    sumModel.ReviewDiploma += excelModel.ReviewDiploma;
                if (excelModel.Other != null)
                    sumModel.Other += excelModel.Other;
            }
            if (sumModel.Lectures == 0)
                sumModel.Lectures = null;
            if (sumModel.Consultations == 0)
                sumModel.Consultations = null;
            if (sumModel.Laboratory == 0)
                sumModel.Laboratory = null;
            if (sumModel.Practices == 0)
                sumModel.Practices = null;
            if (sumModel.Tests == 0)
                sumModel.Tests = null;
            if (sumModel.Exams == 0)
                sumModel.Exams = null;
            if (sumModel.CourseProjects == 0)
                sumModel.CourseProjects = null;
            if (sumModel.CourseWorks == 0)
                sumModel.CourseWorks = null;
            if (sumModel.Diploma == 0)
                sumModel.Diploma = null;
            if (sumModel.RGZ == 0)
                sumModel.RGZ = null;
            if (sumModel.GEKAndGAK == 0)
                sumModel.GEKAndGAK = null;
            if (sumModel.ReviewDiploma == 0)
                sumModel.ReviewDiploma = null;
            if (sumModel.Other == 0)
                sumModel.Other = null;

            return sumModel;
        }


    }
}
