using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs
{
    public class UserCreateDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public int CompanyID { get; set; }

        public string? ReportToMail { get; set; }

        public int? RLeav { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
