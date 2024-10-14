using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.CreateDTO
{
    public class AddExperienceDTO
    {
        public string id { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndingDate { get; set; }
    }
}
