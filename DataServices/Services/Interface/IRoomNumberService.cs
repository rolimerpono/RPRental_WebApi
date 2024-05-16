using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Services.Interface
{
    public interface IRoomNumberService
    {
        Task<IEnumerable<RoomNumber>> GetAllAsync(Expression<Func<RoomNumber, bool>>? filter = null, string? IncludeProperties = null, bool isTracking = false, int pageSize = 0, int pageNumber = 1);

        Task<RoomNumber> GetAsync(int RoomNo);

        Task<Boolean> CreateAsync(RoomNumber objRoomNo);

        Task<Boolean> UpdateAsync(RoomNumber objRoomNo);

        Task<Boolean> RemoveAsync(int RoomNo);
    }
}
