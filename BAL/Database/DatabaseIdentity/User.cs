using BAL.Database.DatabaseModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Database.DatabaseIdentity
{
    public class User : IdentityUser
    {
        public int CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string? reportToMail { get; set; }

        public int? rLeav { get; set; }

        public decimal? totalExp { get; set; }

        public virtual UserInfo? userInfo { get; set; }

        public ICollection<Expense> expenses { get; set; } = new List<Expense>();

        public ICollection<Leave> leaves { get; set; } = new List<Leave>();

        public ICollection<Education> educations { get; set; } = new List<Education>();

        public ICollection<Experience> experiences { get; set; } = new List<Experience>();

    }
}
