using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Database.Company
{
    public class CompanyData
    {
        [Key]
        public int Id { get; set; }
        public string CompanyName { get; set; }
    }
}
