﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RPRENTAL_WEBAPP.Models.DTO
{
    public class APIResponse
    {
        public APIResponse()
        {
            StatusCode = new HttpStatusCode();
            IsSuccess = false;
            Message = String.Empty;
            ErrorMessages = new List<string>();
            Result = new object();

        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }

    }
}
