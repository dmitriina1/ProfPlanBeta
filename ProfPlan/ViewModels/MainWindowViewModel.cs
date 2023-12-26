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

namespace ProfPlan.ViewModels
{

    internal class MainWindowViewModel : ViewModel, INotifyPropertyChanged
    {
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
                TablesCollection.Clear();
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

                                // Заполнение коллекции данных
                                if (table.TableName.IndexOf("Итого", StringComparison.OrdinalIgnoreCase) == -1 &&
                                    table.TableName.IndexOf("доп", StringComparison.OrdinalIgnoreCase) == -1)
                                {
                                    for (int i = rowIndex + 1; i < table.Rows.Count; i++)
                                    {
                                        try
                                        {
                                            if (haveTeacher && !string.IsNullOrWhiteSpace(table.Rows[i][0].ToString()))
                                                list.Add(new ExcelModel(Convert.ToInt32(table.Rows[i][0]),
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
                                            else if (!haveTeacher)
                                                list.Add(new ExcelModel(Convert.ToInt32(table.Rows[i][0]),
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
    }
    }