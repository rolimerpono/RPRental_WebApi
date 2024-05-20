using DataServices.Common.DTO;
using DataServices.Common.DTO.RoomNumber;
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
        Task<APIResponse> IsUniqueRoomNumber(int RoomNo);

        Task<APIResponse> GetAsync(int RoomNo);
        Task<APIResponse> GetAllAsync(bool isTracking = false, int pageSize = 0, int pageNumber = 1);
         
        Task<APIResponse> CreateAsync(RoomNumberCreateDTO objRoomNo);

        Task<APIResponse> UpdateAsync(RoomNumberUpdateDTO objRoomNo);

        Task<APIResponse> RemoveAsync(int RoomNo);

      
    }
}
