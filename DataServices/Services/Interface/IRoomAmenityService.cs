using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Services.Interface
{
    public interface IRoomAmenityService
    {
        Task<IEnumerable<RoomAmenity>> GetAllAsync(Expression<Func<RoomAmenity, bool>>? filter = null, string? IncludeProperties = null, bool isTracking = false, int pageSize = 0, int pageNumber = 1);

        Task<RoomAmenity> GetAsync(int Id);

        Task<Boolean> CreateAsync(RoomAmenity objRoomAmenity);

        Task<Boolean> UpdateAsync(RoomAmenity objRoomAmenity);

        Task<Boolean> RemoveAsync(int Id);


    }
}
