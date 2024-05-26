
using RPRENTAL_WEBAPP.Models.DTO.Amenity;

namespace RPRENTAL_WEBAPP.Services.Interface
{
    public interface IAmenityService
    {
        Task<T> GetAsync<T>(int AmenityId, string Token);
        Task<T> GetAllAsync<T>(string Token);
        Task<T> CreateAsync<T>(AmenityCreateDTO objAmenity, string Token);
        Task<T> UpdateAsync<T>(AmenityUpdateDTO objAmenity, string Token);
        Task<T> DeleteAsync<T>(int AmenityId, string Token);

    }
}
