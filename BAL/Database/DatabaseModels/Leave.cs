using BAL.Database.DatabaseIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Database.DatabaseModels
{
    public class Leave
    {
        public int Id { get; set; }
        public string LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string reportTo { get; set; }
        public bool Approval { get; set; }
        public string userId { get; set; }
        public virtual User user { get; set; }
    }
}
