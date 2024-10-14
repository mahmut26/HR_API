using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.UpdateDTO
{
    public class UpdateLeaveDTO
    {
        public string mail { get;set; } 
        public string? LeaveType { get; set; } 
        public DateTime? StartDate { get; set; } 
        public DateTime? EndingDate { get; set; }
        public int idLeave { get; set; }
    }
}
