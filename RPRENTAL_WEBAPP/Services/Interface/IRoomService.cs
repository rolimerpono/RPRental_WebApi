﻿using RPRENTAL_WEBAPP.Models.DTO.Room;
using RPRENTAL_WEBAPP.Models.DTO.RoomAmenity;

namespace RPRENTAL_WEBAPP.Services.Interface
{
    public interface IRoomAmenityService
    {

        Task<T> GetAsync<T>(int RoomId, string Token);
        Task<T> GetAllAsync<T>(string Token);  
        Task<T> CreateAsync<T>(RoomAmenityCreateDTO objRoom , string Token);
        Task<T> UpdateAsync<T>(RoomAmenityUpdateDTO objRoom, string Token);
        Task<T> DeleteAsync<T>(int RoomId, string Token);

    }
}
