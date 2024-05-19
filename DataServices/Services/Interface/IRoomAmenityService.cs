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
    public interface IRoomAmenityService
    {
    
        Task<RoomAmenity> GetAsync(int RoomId);

        Task<IEnumerable<RoomAmenity>> GetAllAsync(bool isTracking = false, int pageSize = 0, int pageNumber = 1);
          
        Task<APIResponse> CreateAsync(RoomAmenity objRoomAmenity);

        Task<APIResponse> UpdateAsync(RoomAmenity objRoomAmenity);

        Task<APIResponse> RemoveAsync(int RoomId);


    }
}
