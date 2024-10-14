using BAL.Database.DatabaseIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Database.DatabaseModels
{
    public class Education
    {
        public int Id { get; set; }
        public string NameofEdu { get; set; }
        public DateTime DateofEdu { get; set; }
        public string userId { get; set; }
        public virtual User user { get; set; }
    }
}
