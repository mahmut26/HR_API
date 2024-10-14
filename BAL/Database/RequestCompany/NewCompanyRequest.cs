using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Database.RequestCompany
{
    public class NewCompanyRequest
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string CompanyName { get; set; }
        public bool Created { get; set; }
    }
}
