using DatabaseAccess;
using Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class RoomAmenityRepository : Repository<RoomAmenity>, IRoomAmenityRepository
    {
        private readonly ApplicationDBContext _db;
        public RoomAmenityRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public async Task UpdateAsync(RoomAmenity objEntity)
        {
            _db.tbl_RoomAmenity.Update(objEntity);
        }
    }
}
