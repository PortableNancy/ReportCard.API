using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportCard.Core.Models
{
    public class Result
    {
        public int studentId { get; set; }
        public string studentName { get; set; }
        public int Mathematics { get; set; }
        public int English { get; set; }
        public int Chemistry { get; set; }
        public int Biology { get; set; }
        public int Economics { get; set; }
        public int History { get; set; }
        public int studentTotal { get; set; }
        public decimal studentAverage =>  (decimal) studentTotal / 6;
        public char Grade
        {
            get
            {
                switch (studentAverage)
                {
                    case >= 75:
                        return 'A';
                    case >= 65:
                        return 'B';
                    case >= 50:
                        return 'C';
                    case >= 45:
                        return 'D';
                    default:
                        return 'F';
                }
            }
        }
        public string Remark
        {
            get
            {
                switch (studentAverage)
                {
                    case >= 75:
                        return "Excellent";
                    case >= 65:
                        return "Very Good";
                    case >= 50:
                        return "Good";
                    case >= 45:
                        return "Pass";
                    default:
                        return "Fail";
                }
            }
        }


    }
}
