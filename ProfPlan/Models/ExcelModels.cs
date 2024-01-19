using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfPlan.Models
{
    public class ExcelModels
    {
        public ObservableCollection<ExcelModel> ExcelData { get; set; }

        public ExcelModels()
        {
            ExcelData = new ObservableCollection<ExcelModel>();
        }
    }
}
