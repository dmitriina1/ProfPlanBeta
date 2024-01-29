using System;
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
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProfPlan.ViewModels
{

    internal class MainWindowViewModel : ViewModel
    {
        private int CountOfLists;
        private int Number = 1;
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
            //try
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

                            foreach (DataTable table in tableCollection)
                            {
                                Number = 1;
                                tabname = table.TableName;
                                ObservableCollection<ExcelData> list = new ObservableCollection<ExcelData>();
                                int rowIndex = -1;
                                bool haveTeacher = false;

                                //Определение индекса строки с заголовком "Дисциплина"
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
                                bool exitOuterLoop = false;
                                int endstring = -1;
                                for (int i = 0; i < table.Rows.Count; i++)
                                {
                                    for (int j = 0; j < table.Columns.Count - 1; j++)
                                    {
                                        if (table.Rows[i][j].ToString().Trim() == "Дисциплина")
                                        {
                                            rowIndex = i;

                                            exitOuterLoop = true;
                                            break;
                                        }
                                    }

                                    if (exitOuterLoop)
                                    {
                                        // Выход из внешнего цикла
                                        break;
                                    }
                                }
                                if (rowIndex != -1)
                                    for (int i = rowIndex; i < table.Rows.Count; i++)
                                    {
                                        if (table.Rows[i][0].ToString() == "")
                                        {
                                            endstring = i;
                                            break;
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
                                    if (endstring == -1) { endstring = table.Rows.Count; }
                                    for (int i = rowIndex + 1; i < endstring; i++)
                                    {
                                        try
                                        {
                                            if (haveTeacher && !string.IsNullOrWhiteSpace(table.Rows[i][0].ToString()))
                                            {
                                                list.Add(new ExcelModel(teachers,
                                                                       Number,
                                                                       table.Rows[i][1].ToString(),
                                                                       table.Rows[i][2].ToString(),
                                                                       table.Rows[i][3].ToString(),
                                                                       table.Rows[i][4].ToString(),
                                                                       table.Rows[i][5].ToString(),
                                                                       table.Rows[i][6].ToNullable<int>(),
                                                                       table.Rows[i][7].ToString(),
                                                                       table.Rows[i][8].ToString(),
                                                                       table.Rows[i][9].ToNullable<int>(),
                                                                       table.Rows[i][10].ToNullable<int>(),
                                                                       table.Rows[i][11].ToNullable<int>(),
                                                                       table.Rows[i][12].ToString(),
                                                                       table.Rows[i][13].ToNullable<int>(),
                                                                       table.Rows[i][14].ToNullable<double>(),
                                                                       table.Rows[i][15].ToNullable<double>(),
                                                                       table.Rows[i][16].ToNullable<double>(),
                                                                       table.Rows[i][17].ToNullable<double>(),
                                                                       table.Rows[i][18].ToNullable<double>(),
                                                                       table.Rows[i][19].ToNullable<double>(),
                                                                       table.Rows[i][20].ToNullable<double>(),
                                                                       table.Rows[i][21].ToNullable<double>(),
                                                                       table.Rows[i][22].ToNullable<double>(),
                                                                       table.Rows[i][23].ToNullable<double>(),
                                                                       table.Rows[i][24].ToNullable<double>(),
                                                                       table.Rows[i][25].ToNullable<double>(),
                                                                       table.Rows[i][26].ToNullable<double>(),
                                                                       table.Rows[i][27].ToNullable<double>(),
                                                                       table.Rows[i][28].ToNullable<double>()));
                                                Number++;
                                            }
                                            else if (!haveTeacher)
                                            {
                                                list.Add(new ExcelModel(teachers,
                                                                       Number,
                                                                       "",
                                                                       table.Rows[i][1].ToString(),
                                                                       table.Rows[i][2].ToString(),
                                                                       table.Rows[i][3].ToString(),
                                                                       table.Rows[i][4].ToString(),
                                                                       table.Rows[i][5].ToNullable<int>(),
                                                                       table.Rows[i][6].ToString(),
                                                                       table.Rows[i][7].ToString(),
                                                                       table.Rows[i][8].ToNullable<int>(),
                                                                       table.Rows[i][9].ToNullable<int>(),
                                                                       table.Rows[i][10].ToNullable<int>(),
                                                                       table.Rows[i][11].ToString(),
                                                                       table.Rows[i][12].ToNullable<double>(),
                                                                       table.Rows[i][13].ToNullable<double>(),
                                                                       table.Rows[i][14].ToNullable<double>(),
                                                                       table.Rows[i][15].ToNullable<double>(),
                                                                       table.Rows[i][16].ToNullable<double>(),
                                                                       table.Rows[i][17].ToNullable<double>(),
                                                                       table.Rows[i][18].ToNullable<double>(),
                                                                       table.Rows[i][19].ToNullable<double>(),
                                                                       table.Rows[i][20].ToNullable<double>(),
                                                                       table.Rows[i][21].ToNullable<double>(),
                                                                       table.Rows[i][22].ToNullable<double>(),
                                                                       table.Rows[i][23].ToNullable<double>(),
                                                                       table.Rows[i][24].ToNullable<double>(),
                                                                       table.Rows[i][25].ToNullable<double>(),
                                                                       table.Rows[i][26].ToNullable<double>(),
                                                                       table.Rows[i][27].ToNullable<double>()));
                                                Number++;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show($"Error adding data: {ex.Message}");
                                        }
                                    }
                                }
                                else if (table.TableName.IndexOf("Итого", StringComparison.OrdinalIgnoreCase) != -1)
                                {
                                    
                                    for (int i = 1; i < table.Rows.Count; i++)
                                        {
                                        if (!string.IsNullOrEmpty(table.Rows[i][0].ToString()))
                                            list.Add(new ExcelTotal(
                                                table.Rows[i][0].ToString(),
                                                table.Rows[i][1].ToNullable<int>(),
                                                null,
                                                table.Rows[i][2].ToNullable<double>(),
                                                table.Rows[i][3].ToNullable<double>(),
                                                table.Rows[i][4].ToNullable<double>(),
                                                Math.Round(Convert.ToDouble(table.Rows[i][5].ToNullable<double>()),2)
                                                )); 
                                        
                                            
                                        
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
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Error adding data: {ex.Message}");
            //}
        }
        public ICommand ShowTeachersListCommand { get; set; }
        public MainWindowViewModel()
        {
            ShowTeachersListCommand = new RelayCommand(ShowWindow, CanShowWindow);
            ShowReportWindowCommand = new RelayCommand(ShowReportWindow, CanShowWindow);
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
        private async void TeacherListWindow_Closed(object sender, EventArgs e)
        {
            foreach (TableCollection tab in TablesCollection)
            {
                foreach (ExcelData excelData in tab.ExcelData)
                {
                    if (excelData is ExcelModel excelModel)
                    {
                        var teachers = TeacherManager.GetTeachers();
                        ObservableCollection<string> newTeachList = new ObservableCollection<string>();

                        foreach (Teacher teacher in teachers)
                        {
                            newTeachList.Add($"{teacher.LastName} {teacher.FirstName[0]}.{teacher.MiddleName[0]}.");
                        }

                        await Task.Run(() =>
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                excelModel.Teachers = newTeachList;
                            });
                        });
                    }
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
            if (SelectedTable.Tablename.ToString().Contains("ПИиИС"))
            {
                if (CountOfLists != TablesCollection.Count)
                    for (int i = TablesCollection.Count - 1; i >= CountOfLists; i--)
                    {
                        TablesCollection.RemoveAt(i);
                    }

                TableCollection foundTableCollection = TablesCollection.FirstOrDefault(tc => tc.Tablename == SelectedTable.Tablename);
                selectedtab = TablesCollection.IndexOf(foundTableCollection);
                // Метод для создания TableCollection по преподавателям

                var uniqueTeachers = TablesCollection[selectedtab].ExcelData
                .Where(data => data is ExcelModel) // Фильтрация по типу ExcelModel
                .Select(data => ((ExcelModel)data).Teacher) // Приведение к ExcelModel и выбор Teacher
                .Distinct()
                .ToList();
                ObservableCollection<ExcelData> totallist = new ObservableCollection<ExcelData>();
                foreach (var teacher in uniqueTeachers)
                {
                    var teacherTableCollection = new TableCollection() { };
                    
                    if (teacher.ToString() != "")
                        teacherTableCollection = new TableCollection(teacher.ToString().Split(' ')[0]);
                    else
                        teacherTableCollection = new TableCollection("Незаполненные");
                    var teacherRows = TablesCollection[selectedtab].ExcelData
                    .Where(data => data is ExcelModel && ((ExcelModel)data).Teacher == teacher)
                    .ToList();
                    foreach (ExcelModel techrow in teacherRows)
                    {
                        techrow.PropertyChanged += teacherTableCollection.ExcelModel_PropertyChanged;
                        teacherTableCollection.ExcelData.Add(techrow);

                    }
                    teacherTableCollection.SubscribeToExcelDataChanges();
                    TablesCollection.Add(teacherTableCollection);
                    //Реализация листа Итого:

                    if (teacherTableCollection.Tablename != "Незаполненные")
                    {
                        totallist.Add(new ExcelTotal(
                        teacher,
                            null,
                        null,
                            teacherTableCollection.TotalHours,
                           teacherTableCollection.AutumnHours,
                           teacherTableCollection.SpringHours,
                            null)
                            );

                    }
                }
                string tabname = "Итого";
                TablesCollection.Add(new TableCollection(tabname, totallist));



            }
        }
        private RelayCommand _moveTeachersCommand;
        public ICommand MoveTeachersCommand
        {
            get { return _moveTeachersCommand ?? (_moveTeachersCommand = new RelayCommand(MoveTeachers)); }
        }
        private void MoveTeachers(object parameter)
        {
            int ftableindex = FindTableIndex("П_ПИиИс");
            int stableindex = FindTableIndex("Ф_ПИиИс");

            if (ftableindex != -1 && stableindex != -1)
            {
                for (int i = 0; i < TablesCollection[stableindex].ExcelData.Count; i++)
                {
                    if (TablesCollection[stableindex].ExcelData[i] is ExcelModel excelModel && excelModel.Teacher == "")
                    {
                        ExcelModel stableData = TablesCollection[stableindex].ExcelData[i] as ExcelModel;
                        ExcelModel ftableData = TablesCollection[ftableindex].ExcelData[i] as ExcelModel;

                        if (stableData != null && ftableData != null &&
                            stableData.Term == ftableData.Term &&
                            stableData.Group == ftableData.Group &&
                            stableData.Institute == ftableData.Institute &&
                            stableData.FormOfStudy == ftableData.FormOfStudy &&
                            ftableData.Teacher != "")
                        {
                            stableData.Teacher = ftableData.Teacher;
                        }
                    }
                }
            }
        }
        private int FindTableIndex(string tableName)
        {
            string cleanedTableName = Regex.Replace(tableName, @"[^\w\s]|_", "");

            string cleanedTableNameLower = cleanedTableName.ToLower();

            for (int i = 0; i < TablesCollection.Count; i++)
            {
                string currentTableName = Regex.Replace(TablesCollection[i].Tablename, @"[^\w\s]|_", "").ToLower();
                if (currentTableName == cleanedTableNameLower)
                {
                    return i;
                }
            }

            return -1;
        }

        //Окно ReportWindow

        public ICommand ShowReportWindowCommand { get; set; }

        private void ShowReportWindow(object obj)
        {
                var reportwindow = obj as Window;

                ReportWindow report = new ReportWindow(TablesCollection);
                report.Owner = reportwindow;
                report.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                report.ShowDialog();
            
        }
    }
}