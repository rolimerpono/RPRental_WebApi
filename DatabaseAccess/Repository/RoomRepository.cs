using DataServices.Common.RepositoryInterface;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.Repository
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {

        private readonly ApplicationDBContext _db;

        public RoomRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
       
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Room objEntity)
        {
            var previous_data = await _db.tbl_Rooms.FindAsync(objEntity.RoomId);

         
            foreach (var property in typeof(Room).GetProperties())
            {
        
                if (property.GetValue(objEntity) != null)
                {
                    //Update only the fields if source field not null;
                    property.SetValue(previous_data, property.GetValue(objEntity));
                }
            }
            _db.tbl_Rooms.Update(previous_data);

        }
    }
}
