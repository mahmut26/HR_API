using BAL.Database.DatabaseIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Database.DatabaseModels
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Total { get; set; }
        public string reportTo { get; set; }
        public bool Approval { get; set; }
        public string userId { get; set; }
        public virtual User user { get; set; }
    }
}
