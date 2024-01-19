using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfPlan.Models
{
    public class ExcelTotal: ExcelData
    {
        public string Teacher { get; set; }
        public int? Bet { get; set; }
        public double? BetPercent { get; set; }  
        public double? TotalHours { get; set; }
        public double? AutumnHours { get; set; }
        public double? SpringHours { get; set; }
        public double? Difference { get; set; }
        public ExcelTotal() { }
        public ExcelTotal(string techer, int? bet, double? betpercent, double? total, double? autumnhours, double? springHours, double? difference)
        {
            Teacher = techer;
            Bet = bet;
            BetPercent = betpercent;
            TotalHours = total;
            AutumnHours = autumnhours;
            SpringHours = springHours;
            Difference = difference;
        }
    }
}
