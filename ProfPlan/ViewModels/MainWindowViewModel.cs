﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows;
using ProfPlan.ViewModels.Base;
using ProfPlan.Models;
using ProfPlan.Commads;
using ExcelDataReader;
using ProfPlan.Views;

namespace ProfPlan.ViewModels
{

    internal class MainWindowViewModel : ViewModel, INotifyPropertyChanged
    {
        private int CountOfLists;
        private int Number;
        private DataTableCollection tableCollection;
        private ObservableCollection<TableCollection> _tablesCollection = new ObservableCollection<TableCollection>();

        public ObservableCollection<TableCollection> TablesCollection
        {
            get { return _tablesCollection; }
            set
            {
                if (_tablesCollection != value)
                {
                    _tablesCollection = value;
                    OnPropertyChanged(nameof(TablesCollection));
                }
            }
        }
        private TableCollection _selectedTable;

        public TableCollection SelectedTable
        {
            get { return _selectedTable; }
            set
            {
                if (_selectedTable != value)
                {
                    _selectedTable = value;
                    OnPropertyChanged(nameof(SelectedTable));
                }
            }
        }
        private RelayCommand _loadDataCommand;
        public ICommand LoadDataCommand
        {
            get { return _loadDataCommand ?? (_loadDataCommand = new RelayCommand(LoadData)); }
        }

        private void LoadData(object parameter)
        {
            try
            {
                string tabname = "";
                var openFileDialog = new OpenFileDialog() { Filter = "Excel Files|*.xls;*.xlsx" };
                if (openFileDialog.ShowDialog() == true)
                {
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = false }
                            });
                            tableCollection = result.Tables;
                            CountOfLists = tableCollection.Count;
                            TablesCollection.Clear();
                            Number = 0;
                            foreach (DataTable table in tableCollection)
                            {
                                tabname = table.TableName;
                                ObservableCollection<ExcelModel> list = new ObservableCollection<ExcelModel>();
                                int rowIndex = -1;
                                bool haveTeacher = false;

                                // Определение индекса строки с заголовком "Дисциплина"
                                for (int i = 0; i < table.Rows.Count; i++)
                                {
                                    for (int j = 0; j < table.Columns.Count - 1; j++)
                                    {
                                        if (table.Rows[i][j].ToString().Trim() == "Дисциплина")
                                        {
                                            rowIndex = i;
                                            break;
                                        }
                                    }
                                }

                                // Проверка наличия столбца "Преподаватель"
                                for (int j = 0; j < table.Columns.Count - 1; j++)
                                {
                                    if (rowIndex != -1 && table.Rows[rowIndex][j].ToString().Trim() == "Преподаватель")
                                    {
                                        haveTeacher = true;
                                        break;
                                    }
                                }
                                var teachers = new ObservableCollection<string>();

                                // Заполнение коллекции данных
                                if (table.TableName.IndexOf("Итого", StringComparison.OrdinalIgnoreCase) == -1 &&
                                    table.TableName.IndexOf("доп", StringComparison.OrdinalIgnoreCase) == -1)
                                {
                                    teachers = new ObservableCollection<string>(TeacherManager.GetTeachers().Select(t => $"{t.FirstName} {t.LastName[0]}.{t.MiddleName[0]}."));
                                    for (int i = rowIndex + 1; i < table.Rows.Count; i++)
                                    {
                                        try
                                        {
                                            if (haveTeacher && !string.IsNullOrWhiteSpace(table.Rows[i][0].ToString()))
                                            {
                                                list.Add(new ExcelModel(teachers, Convert.ToInt32(table.Rows[i][0]),
                                                                       table.Rows[i][1].ToString(),
                                                                       table.Rows[i][2].ToString(),
                                                                       table.Rows[i][3].ToString(),
                                                                       table.Rows[i][4].ToString(),
                                                                       table.Rows[i][5].ToString(),
                                                                       table.Rows[i][6].ToString(),
                                                                       table.Rows[i][7].ToString(),
                                                                       table.Rows[i][8].ToString(),
                                                                       table.Rows[i][9].ToString(),
                                                                       table.Rows[i][10].ToString(),
                                                                       table.Rows[i][11].ToString(),
                                                                       table.Rows[i][12].ToString(),
                                                                       table.Rows[i][13].ToString(),
                                                                       table.Rows[i][14].ToString(),
                                                                       table.Rows[i][15].ToString(),
                                                                       table.Rows[i][16].ToString(),
                                                                       table.Rows[i][17].ToString(),
                                                                       table.Rows[i][18].ToString(),
                                                                       table.Rows[i][19].ToString(),
                                                                       table.Rows[i][20].ToString(),
                                                                       table.Rows[i][21].ToString(),
                                                                       table.Rows[i][22].ToString(),
                                                                       table.Rows[i][23].ToString(),
                                                                       table.Rows[i][24].ToString(),
                                                                       table.Rows[i][25].ToString(),
                                                                       table.Rows[i][26].ToString(),
                                                                       table.Rows[i][27].ToString(),
                                                                       table.Rows[i][28].ToString()));
                                                Number++;
                                            }
                                            else if (!haveTeacher)
                                            {
                                                list.Add(new ExcelModel(teachers,Convert.ToInt32(table.Rows[i][0]),
                                                                       "",
                                                                       table.Rows[i][1].ToString(),
                                                                       table.Rows[i][2].ToString(),
                                                                       table.Rows[i][3].ToString(),
                                                                       table.Rows[i][4].ToString(),
                                                                       table.Rows[i][5].ToString(),
                                                                       table.Rows[i][6].ToString(),
                                                                       table.Rows[i][7].ToString(),
                                                                       table.Rows[i][8].ToString(),
                                                                       table.Rows[i][9].ToString(),
                                                                       table.Rows[i][10].ToString(),
                                                                       table.Rows[i][11].ToString(),
                                                                       table.Rows[i][12].ToString(),
                                                                       table.Rows[i][13].ToString(),
                                                                       table.Rows[i][14].ToString(),
                                                                       table.Rows[i][15].ToString(),
                                                                       table.Rows[i][16].ToString(),
                                                                       table.Rows[i][17].ToString(),
                                                                       table.Rows[i][18].ToString(),
                                                                       table.Rows[i][19].ToString(),
                                                                       table.Rows[i][20].ToString(),
                                                                       table.Rows[i][21].ToString(),
                                                                       table.Rows[i][22].ToString(),
                                                                       table.Rows[i][23].ToString(),
                                                                       table.Rows[i][24].ToString(),
                                                                       table.Rows[i][25].ToString(),
                                                                       table.Rows[i][26].ToString(),
                                                                       table.Rows[i][27].ToString()));
                                                Number++;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show($"Error adding data: {ex.Message}");
                                        }
                                    }
                                }

                                // Добавление коллекции в TablesCollection
                                TablesCollection.Add(new TableCollection(tabname, list));
                            }
                        }
                    }

                    // Обновление свойства привязок данных в XAML
                    SelectedTable = TablesCollection.FirstOrDefault();
                    OnPropertyChanged(nameof(TablesCollection));
                    OnPropertyChanged(nameof(SelectedTable));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding data: {ex.Message}");
            }
        }
        public ICommand ShowTeachersListCommand { get; set; }
        public MainWindowViewModel()
        {
            ShowTeachersListCommand = new RelayCommand(ShowWindow, CanShowWindow);
        }
        private bool CanShowWindow(object obj)
        {
            return true;
        }

        private void ShowWindow(object obj)
        {
            var techerswindow = obj as Window;

            TeacherListWindow teacherlist = new TeacherListWindow();
            teacherlist.Owner = techerswindow;
            teacherlist.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            teacherlist.Closed += TeacherListWindow_Closed;
            teacherlist.ShowDialog();
        }
        private void TeacherListWindow_Closed(object sender, EventArgs e)
        {
            foreach (TableCollection tab in TablesCollection)
            {
                for (int i = 0; i < tab.ExcelData.Count; i++)
                {
                    var teahers = TeacherManager.GetTeachers();
                    ObservableCollection<string> NewTeachList = new ObservableCollection<string>();
                    foreach (Teacher teacher in teahers)
                    {
                        NewTeachList.Add($"{teacher.LastName} {teacher.FirstName[0]}.{teacher.MiddleName[0]}.");
                    }
                    tab.ExcelData[i].Teachers = NewTeachList;
                }
            }

        }

        private RelayCommand _generateTeachersLists;
        public ICommand GenerateTeachersLists
        {
            get { return _generateTeachersLists ?? (_generateTeachersLists = new RelayCommand(GenerateTeacher)); }
        }
        private void GenerateTeacher(object parameter)
        {
            int selectedtab = -1;
            //if (SelectedTable.Tablename.ToString().Contains("ПИиИС"))
            if (SelectedTable.Tablename.ToString().Contains("Лист"))
            {
                //for (int i=0;i<TablesCollection.Count;i++)
                //{
                //    if (TablesCollection[i].Tablename.ToString() == SelectedTable.Tablename.ToString())
                //    {
                //        selectedtab = i;
                //        break;
                //    }
                //}
                if (CountOfLists != TablesCollection.Count)
                    for (int i = TablesCollection.Count - 1; i >= CountOfLists; i--)
                    {
                        TablesCollection.RemoveAt(i);
                    }

                TableCollection foundTableCollection = TablesCollection.FirstOrDefault(tc => tc.Tablename == SelectedTable.Tablename);
                selectedtab = TablesCollection.IndexOf(foundTableCollection);
                #region Метод для создания TableCollection по преподавателям

                var uniqueTeachers = TablesCollection[selectedtab].ExcelData
                                    .Select(data => data.Teacher)
                                    .Distinct()
                                    .ToList();

                foreach (var teacher in uniqueTeachers)
                {
                    var teacherTableCollection = new TableCollection() { };
                    if (teacher.ToString() != "")
                        teacherTableCollection = new TableCollection(teacher.ToString().Split(' ')[0]);
                    else
                        teacherTableCollection = new TableCollection("Незаполненные");
                    // Фильтруем строки для текущего преподавателя
                    var teacherRows = TablesCollection[selectedtab].ExcelData
                        .Where(data => data.Teacher == teacher)
                        .ToList();

                    // Добавляем отфильтрованные строки в новую TableCollection
                    foreach (ExcelModel techrow in teacherRows)
                        teacherTableCollection.ExcelData.Add(techrow);

                    // Добавляем новую TableCollection в общую коллекцию
                    TablesCollection.Add(teacherTableCollection);
                }


                #endregion
            }
        }

    }
}