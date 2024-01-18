using ProfPlan.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfPlan.Models
{
    public class TableCollection: ViewModel, INotifyPropertyChanged
    {
        private string _tablename = null;
        private ObservableCollection<ExcelModel> _excelData = new ObservableCollection<ExcelModel>();

        public ObservableCollection<ExcelModel> ExcelData
        {
            get { return _excelData; }
            set
            {
                if (_excelData != value)
                {
                    _excelData = value;
                    OnPropertyChanged(nameof(ExcelData));
                    SubscribeToExcelDataChanges();
                }
            }
        }
        public string Tablename
        {
            get { return _tablename; }
            set
            {
                if (_tablename != value)
                {
                    _tablename = value;
                    OnPropertyChanged(nameof(Tablename));
                }
            }
        }
        private void SubscribeToExcelDataChanges()
        {
            // Отписываемся от предыдущих событий, если они были
            foreach (var excelModel in _excelData)
            {
                excelModel.PropertyChanged -= ExcelModel_PropertyChanged;
            }

            // Подписываемся на события изменения каждого элемента коллекции
            foreach (var excelModel in _excelData)
            {
                excelModel.PropertyChanged += ExcelModel_PropertyChanged;
            }

            // Вызываем обновление TotalHours при изменении коллекции
            UpdateTotalHours();
        }
        private void ExcelModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Обработка изменения свойства в ExcelModel
            UpdateTotalHours();
        }
        private void UpdateTotalHours()
        {
            // Обновление TotalHours на основе значений свойства Total каждого элемента коллекции
            // Пример: суммирование Total каждого элемента
            TotalHours = _excelData.Where(x => x.Total != null).Sum(x => Convert.ToDouble(x.Total));
        }
        private double _totalHours;
        public double TotalHours
        {
            get { return _totalHours; }
            set
            {
                if (_totalHours != value)
                {
                    _totalHours = value;
                    OnPropertyChanged(nameof(TotalHours));
                }
            }
        }
        private string _autumnHours;
        public string AutumnHours
        {
            get { return _autumnHours; }
            set
            {
                if (_autumnHours != value)
                {
                    _autumnHours = value;
                    OnPropertyChanged(nameof(AutumnHours));
                }
            }
        }
        private string _springHours;

        public string SpringHours
        {
            get { return _springHours; }
            set
            {
                if (_springHours != value)
                {
                    _springHours = value;
                    OnPropertyChanged(nameof(SpringHours));
                }
            }
        }

        public TableCollection(string tablename, ObservableCollection<ExcelModel> col)
        {
            Tablename = tablename;
            ExcelData = col;
        }
        public TableCollection(string tablename)
        {
            Tablename = tablename;
        }
        public TableCollection()
        {
        }
    }
}
