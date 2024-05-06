﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IRoomAmenityRepository : IRepository<RoomAmenity>
    {
        Task UpdateAsync(RoomAmenity objEntity);

    }
}
