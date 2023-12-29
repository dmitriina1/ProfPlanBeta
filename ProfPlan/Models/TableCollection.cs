﻿using ProfPlan.ViewModels.Base;
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
        public TableCollection(string tablename, ObservableCollection<ExcelModel> col)
        {
            Tablename = tablename;
            ExcelData = col;
        }
    }
}