using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.UpdateDTO
{
    public class UpdateExpenseDTO
    {
       public string mail { get; set; }
       public string? description { get; set; }
       public decimal? total { get; set; }
       public int idExp { get; set; }
    }
}
