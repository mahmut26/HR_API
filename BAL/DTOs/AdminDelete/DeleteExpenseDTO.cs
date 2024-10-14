using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.AdminDelete
{
    public class DeleteExpenseDTO
    {
        public string mail {  get; set; }
        public int expenseId { get; set; }
    }
}
