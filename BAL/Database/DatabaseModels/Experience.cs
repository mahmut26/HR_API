using BAL.Database.DatabaseIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Database.DatabaseModels
{
    public class Experience
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string userId { get; set; }
        public virtual User user { get; set; }
    }
}
