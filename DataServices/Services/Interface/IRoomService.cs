using DataServices.Common.DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Services.Interface
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllAsync(Expression<Func<Room, bool>>? filter = null, string? IncludeProperties = null, bool isTracking = false, int pageSize = 0, int pageNumber = 1);

        Task<Room> GetAsync(int Id);

        Task<APIResponse> CreateAsync(Room objRoom);

        Task<APIResponse> UpdateAsync(Room objRoom);

        Task<APIResponse> RemoveAsync(int Id);

        Task<APIResponse> IsUniqueRoom(string RoomName);

    }

}
