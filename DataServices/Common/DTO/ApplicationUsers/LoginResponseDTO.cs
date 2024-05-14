using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Common.DTO.ApplicationUsers
{
    public class LoginResponseDTO
    {
        public ApplicationUser User { get; set; }
        public string Token { get; set; }
    }
}
