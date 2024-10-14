using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.UpdateDTO
{
    public class ApproveLeaveDTO
    {
        public int leaveId { get; set; }
        public string idForRtoId { get; set; }
        public bool isApp { get; set; }
    }
}
