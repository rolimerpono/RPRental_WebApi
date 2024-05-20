using RPRENTAL_WEBAPP.Models.DTO.Room;

namespace RPRENTAL_WEBAPP.Services.Interface
{
    public interface IRoomService
    {

        Task<T> GetAsync<T>(int RoomId, string Token);
        Task<T> GetAllAsync<T>(string Token);  
        Task<T> CreateAsync<T>(RoomCreateDTO objRoom , string Token);

        Task<T> UpdateAsync<T>(RoomUpdateDTO objRoom, string Token);

        Task<T> DeleteAsync<T>(int RoomId, string Token);

    }
}
