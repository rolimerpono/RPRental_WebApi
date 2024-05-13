using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Common.DTO
{
    public class loginRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsRemember { get; set; }
    }
}
