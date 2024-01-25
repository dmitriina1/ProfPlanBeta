using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ProfPlan.Models;
using ProfPlan.ViewModels;
using ProfPlan.ViewModels.Base;

namespace ProfPlan.ViewModels
{
    internal class ReportViewModel : ViewModel
    {
        public ReportViewModel(ObservableCollection<TableCollection> TablesCollection)
        {
            TablesCollectionForReport = new ObservableCollection<TableCollection>();

            foreach (var table in TablesCollection)
            {
                if (table.Tablename.IndexOf("ПИиИС", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    TablesCollectionForReport.Add(table);
                }
            }
        }
            
        public ObservableCollection<TableCollection> TablesCollectionForReport { get; set; }
    }
}
