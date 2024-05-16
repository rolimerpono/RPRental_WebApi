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
        Task<IEnumerable<Amenity>> GetAllAsync(Expression<Func<Amenity, bool>>? filter = null, string? IncludeProperties = null, bool isTracking = false, int pageSize = 0, int pageNumber = 1);

        Task<Amenity> GetAsync(int Id);

        Task<Boolean> CreateAsync(Amenity objAmenity);

        Task<Boolean> UpdateAsync(Amenity objAmenity);

        Task<Boolean> RemoveAsync(int Id);
    }
}
