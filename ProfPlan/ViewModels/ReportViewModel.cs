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
using ExcelDataReader;
using System.Data;
using System.IO;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

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
                    sumTableCollection.ExcelData.Add(sumOdd);
                    sumTableCollection.ExcelData.Add(sumEven);
                    TablesCollectionTeacherSum.Add(sumTableCollection);
                }
            }
            SaveToExcel(TablesCollectionTeacherSum, "C://Users//DimasikAnanasik//OneDrive//Рабочий стол//ProfPlan Beta");
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


        //Сохранение

        public static void SaveToExcel(ObservableCollection<TableCollection> tablesCollection, string directoryPath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("CombinedTeachers");
                int frow = 1, srow = 2;
                // Добавление заголовков
                
                int columnNumber = 1;

                worksheet.Range(frow, 1, frow, 3).Merge();
                worksheet.Cell(frow, 1).Value = "Первое полугодие";
                frow++;

                worksheet.Cell(2, columnNumber++).Value = "Teacher";

                List<string> propertyNames = new List<string>();

                foreach (var propertyInfo in typeof(ExcelModel).GetProperties())
                {
                    if (propertyInfo.Name == "Lectures" || propertyInfo.Name == "Consultations" || propertyInfo.Name == "Laboratory" || propertyInfo.Name == "Practices" || propertyInfo.Name == "Tests" || propertyInfo.Name == "Exams" || propertyInfo.Name == "CourseProjects" || propertyInfo.Name == "CourseWorks" || propertyInfo.Name == "Diploma" || propertyInfo.Name == "RGZ" || propertyInfo.Name == "GEKAndGAK" || propertyInfo.Name == "ReviewDiploma" || propertyInfo.Name == "Other")
                    {
                        worksheet.Cell(2, columnNumber).Value = propertyInfo.Name;
                        propertyNames.Add(propertyInfo.Name);
                        columnNumber++;
                    }
                }

                // Add "TotalSemester" column header
                worksheet.Cell(2, columnNumber).Value = "TotalSemester";

                // Заполнение данных - первые элементы
                int rowNumber = 3;
                foreach (var tableCollection in tablesCollection)
                {
                    string teacherName = tableCollection.Tablename; // Используем имя преподавателя из TableCollection

                    if (tableCollection.ExcelData.Count >= 1)
                    {
                        var excelModel = tableCollection.ExcelData[0];
                        columnNumber = 1;
                        worksheet.Cell(rowNumber, columnNumber++).Value = teacherName;

                        // Sum of all columns starting from column 2 (excluding "Teacher" column)
                        double totalSemester = propertyNames.Sum(propertyName => Convert.ToDouble(typeof(ExcelModel).GetProperty(propertyName)?.GetValue(excelModel, null) ?? 0));
                        foreach (var propertyName in propertyNames)
                        {
                            var value = typeof(ExcelModel).GetProperty(propertyName)?.GetValue(excelModel, null);
                            worksheet.Cell(rowNumber, columnNumber++).Value = value != null ? value.ToString() : "";
                        }

                        // Add the TotalSemester value
                        worksheet.Cell(rowNumber, columnNumber).Value = totalSemester;

                        rowNumber++;
                    }
                }

                // Add a gap of 10 rows
                rowNumber += 6;

                // Duplicate column headers
                int headerRow = rowNumber;
                srow = rowNumber - 1;
                worksheet.Range(srow, 1, srow, 3).Merge();
                worksheet.Cell(srow, 1).Value = "Второе полугодие";
                srow++;
                rowNumber++;
                
                columnNumber = 1;
                worksheet.Cell(headerRow, columnNumber++).Value = "Teacher";
                foreach (var propertyName in propertyNames)
                {
                    worksheet.Cell(headerRow, columnNumber++).Value = propertyName;
                }
                worksheet.Cell(headerRow, columnNumber).Value = "TotalSemester";

                // Заполнение данных - вторые элементы
                foreach (var tableCollection in tablesCollection)
                {
                    string teacherName = tableCollection.Tablename; // Используем имя преподавателя из TableCollection

                    if (tableCollection.ExcelData.Count == 2)
                    {
                        var excelModel = tableCollection.ExcelData[1];

                        columnNumber = 1;
                        worksheet.Cell(rowNumber, columnNumber++).Value = teacherName;

                        // Sum of all columns starting from column 2 (excluding "Teacher" column)
                        double totalSemester = propertyNames.Sum(propertyName => Convert.ToDouble(typeof(ExcelModel).GetProperty(propertyName)?.GetValue(excelModel, null) ?? 0));
                        foreach (var propertyName in propertyNames)
                        {
                            var value = typeof(ExcelModel).GetProperty(propertyName)?.GetValue(excelModel, null);
                            worksheet.Cell(rowNumber, columnNumber++).Value = value != null ? value.ToString() : "";
                        }

                        // Add the TotalSemester value
                        worksheet.Cell(rowNumber, columnNumber).Value = totalSemester;

                        rowNumber++;
                    }
                }
                SwapColumns(worksheet, 3, 5);
                SwapColumns(worksheet, 8, 9);
                SwapColumns(worksheet, 10, 11);
                SwapColumns(worksheet, 11, 12);
                List<string> newPropertyNames = new List<string>
                {
                    "Преподаватель", "Чтение лекций", "Консультации", "Лабораторные работы",
                    "Практические занятия", "Зачеты", "Экзамены", "Курсовыми проектами",
                    "Курсовыми работами", "Дипломными работами", "РГР", "ГЭК",
                    "Проверка контрольных работ", "Другие виды работ", "Итог за семестр"
                };
                for (int i = 0; i < newPropertyNames.Count; i++)
                {
                    worksheet.Cell(srow, i + 1).Value = newPropertyNames[i];
                    worksheet.Cell(frow, i + 1).Value = newPropertyNames[i];
                }

                // Сохранение в файл
                string fileName = $"Бланк_Нагрузки.xlsx";
                string filePath = Path.Combine(directoryPath, fileName);

                // Сохранение в файл
                workbook.SaveAs(filePath);
            }
        }

        public static void SwapColumns(IXLWorksheet worksheet, int column1Index, int column2Index)
        {
            int startRow = worksheet.FirstRowUsed().RowNumber();
            int endRow = worksheet.LastRowUsed().RowNumber();

            for (int row = startRow; row <= endRow; row++)
            {
                var tempValue = worksheet.Cell(row, column1Index).Value;
                worksheet.Cell(row, column1Index).Value = worksheet.Cell(row, column2Index).Value;
                worksheet.Cell(row, column2Index).Value = tempValue;
            }
        }


    }
}
