using DataServices.Common.DTO;
using DataServices.Common.DTO.Amenity;
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

        Task<APIResponse> GetAllAsync(bool isTracking = false, int pageSize = 0, int pageNumber = 1);

        Task<APIResponse> GetAsync(int AmenityId);

        Task<APIResponse> CreateAsync(AmenityCreateDTO objAmenity);

        Task<APIResponse> UpdateAsync(AmenityUpdateDTO objAmenity);

        Task<APIResponse> RemoveAsync(int AmenityId);

    }
}
