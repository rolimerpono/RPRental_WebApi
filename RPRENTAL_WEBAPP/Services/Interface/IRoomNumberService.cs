using RPRENTAL_WEBAPP.Models.DTO.Room;
using RPRENTAL_WEBAPP.Models.DTO.RoomNumber;

namespace RPRENTAL_WEBAPP.Services.Interface
{
    public interface IRoomNumberService
    {

        Task<T> GetAsync<T>(int RoomNo, string Token);
        Task<T> GetAllAsync<T>(string Token);  
        Task<T> CreateAsync<T>(RoomNumberCreateDTO objRoom , string Token);

        Task<T> UpdateAsync<T>(RoomNumberUpdateDTO objRoom, string Token);

        Task<T> DeleteAsync<T>(int RoomNo, string Token);

    }
}
