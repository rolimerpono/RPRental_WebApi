using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? IncludeProperties = null, bool isTracking = false);

        Task<T> GetAsync(Expression<Func<T, bool>> filter, string? IncludeProperties = null, bool isTracking = false);

        Task CreateAync(T objEntity);

        Task RemoveAsync(T objEntity);

        Task SaveAsync();

        Task AnyAsync(Expression<Func<T, Boolean>>? Filter);

 
    }
}
