using RPRENTAL_WEBAPP.Models.DTO.Room;

namespace RPRENTAL_WEBAPP.Services.Interface
{
    public interface IRoomService
    {

        Task<T> GetAsync<T>(int id, string Token);
        Task<T> GetAllAsync<T>(string Token);  
        Task<T> CreateAsync<T>(RoomDTO objRoom , string Token);

        Task<T> UpdateAsync<T>(RoomDTO objRoom, string Token);

        Task<T> DeleteAsync<T>(int id, string Token);

    }
}
