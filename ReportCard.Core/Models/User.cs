using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportCard.Core.Models
{
    public class User
    {
        public int ID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int Isactive { get; set; }

    }
}
