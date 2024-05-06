using DatabaseAccess;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class RoomRepository :  Repository<Room>, IRoomRepository
    {
        private readonly ApplicationDBContext _db;

        public RoomRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;   
        }

        public async Task UpdateAsync(Room objEntity)
        {
            _db.tbl_Rooms.Update(objEntity);
          
        }
       

    }
}
