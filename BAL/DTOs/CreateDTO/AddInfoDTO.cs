using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.CreateDTO
{
    public class AddInfoDTO
    {
        public string id { get; set; } 
        public string name { get; set; }
        public string des { get; set; }
        public DateTime dateTime { get; set; }
    }
}
