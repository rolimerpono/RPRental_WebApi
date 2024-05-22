using DataServices.Common.DTO;
using DataServices.Common.DTO.RoomAmenity;
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
    
        Task<APIResponse> GetAsync(int RoomId);

        Task<APIResponse> GetAllAsync(bool isTracking = false, int pageSize = 0, int pageNumber = 1);
          
        Task<APIResponse> CreateAsync(RoomAmenityCreateDTO objRoomAmenity);

        Task<APIResponse> UpdateAsync(RoomAmenityUpdateDTO objRoomAmenity);

        Task<APIResponse> RemoveAsync(int RoomId);


    }
}
