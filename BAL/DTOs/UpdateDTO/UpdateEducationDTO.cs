using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.UpdateDTO
{
    public class UpdateEducationDTO
    {
        public string mail { get; set; }
        public string? nameofEdu { get; set; } 
        public DateTime? dateofEdu { get; set; }

        public int id { get; set; }
    }
}
