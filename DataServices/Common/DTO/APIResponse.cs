using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Common.DTO
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessages = new List<string>();
            Message = string.Empty;
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }

        public string Message { get; set; } 
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }

    }
}
