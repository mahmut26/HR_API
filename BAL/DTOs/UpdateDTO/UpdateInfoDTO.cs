using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.UpdateDTO
{
    public class UpdateInfoDTO
    {
        public string mail { get; set; }
        public string? name { get; set; }
        public string? des { get; set; }
        public DateTime? dateTime { get;set; }
    }
}
