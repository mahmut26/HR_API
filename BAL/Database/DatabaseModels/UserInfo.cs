using BAL.Database.DatabaseIdentity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Database.DatabaseModels
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }
        public string userId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime bornDate { get; set; }
        public virtual User user { get; set; }
    }
}
