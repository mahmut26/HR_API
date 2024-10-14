using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.CreateDTO
{
    public class AddExpenseDTO
    {
        public string id { get; set; }
        public string Description { get; set; }
        public decimal Total { get; set; }
    }
}
