﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Services.Interface
{
    public interface IDBInitializer
    {
        Task DBInitializer();
    }
}