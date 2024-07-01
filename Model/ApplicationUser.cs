using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Model
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Role = string.Empty;
        }
        public string? Fullname { get; set; }

        
        [NotMapped]
        public string Password { get; set;}
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [NotMapped]
        public string Role { get;set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = Convert.ToDateTime("1900-01-01");

        [NotMapped]
        public string? Token { get; set; }
    }
}
