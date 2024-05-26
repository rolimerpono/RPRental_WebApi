using RPRENTAL_WEBAPP.Models.DTO.Room;

namespace RPRENTAL_WEBAPP.Services.Interface
{
    public interface IRoomService
    {

        Task<T> GetAsync<T>(int RoomId, string Token);
        Task<T> GetAllAsync<T>(string Token);  
        Task<T> CreateAsync<T>(RoomCreateDTO objRoomAmenity , string Token);
        Task<T> UpdateAsync<T>(RoomUpdateDTO objRoomAmenity, string Token);
        Task<T> DeleteAsync<T>(int RoomId, string Token);

    }
}
