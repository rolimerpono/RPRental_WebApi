using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Common.RepositoryInterface
{
    public interface IRepository<T> where T : class
    {

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? IncludeProperties = null,
           int pageSize = 0, int pageNumber = 1, bool isTracking = false);

        Task<T> GetAsync(Expression<Func<T, bool>> filter, string? IncludeProperties = null, bool isTracking = false);

        Task AddAsync(T objEntity);

        Task<bool> AnyAsync(Expression<Func<T, Boolean>>? Filter);

        Task RemoveAsync(T objEntity);

        Task SaveAsync();

    }
}
