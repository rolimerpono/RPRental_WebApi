using DataServices.Common.DTO;
using DataServices.Common.DTO.Room;
using Microsoft.AspNetCore.Http;
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
        Task<APIResponse> IsUniqueRoom(string RoomName);

        Task<APIResponse> GetAsync(int RoomId);

        Task<APIResponse> GetAllAsync(bool isTracking = false, int pageSize = 0, int pageNumber = 1);

        Task<APIResponse> GetRoomAvailable(DateOnly CheckInDate, DateOnly CheckOutDate, bool isTracking = false, int pageSize = 0, int pageNumber = 1);
     
        Task<APIResponse> CreateAsync(RoomCreateDTO objRoom);

        Task<APIResponse> UpdateAsync(RoomUpdateDTO objRoom);

        Task<APIResponse> RemoveAsync(int RoomId);

    

    }

}
