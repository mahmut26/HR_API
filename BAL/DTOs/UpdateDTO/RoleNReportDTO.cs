using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.UpdateDTO
{
    public class RoleNReportDTO
    {
        public string toBeUpdate { get; set; }
        public string reportTo { get; set; }
        public int cId { get; set; }
        public string roleName { get; set; }
    }
}
