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
    public interface IAmenityService
    {
        Task<APIResponse> IsUniqueAmenity(string AmenityName);

        Task<IEnumerable<Amenity>> GetAllAsync(bool isTracking = false, int pageSize = 0, int pageNumber = 1);

        Task<Amenity> GetAsync(int AmenityId);

        Task<APIResponse> CreateAsync(Amenity objAmenity);

        Task<APIResponse> UpdateAsync(Amenity objAmenity);

        Task<APIResponse> RemoveAsync(int AmenityId);

    }
}
