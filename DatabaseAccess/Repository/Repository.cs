using DataServices.Common.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DatabaseAccess.Repository
{
    public class Repository<T> : IRepository<T> where T: class
    {
        private readonly ApplicationDBContext _db;
        private DbSet<T> _dbSet;

        public Repository(ApplicationDBContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();  
        }

        public async Task AddAsync(T objEntity)
        {
           await _dbSet.AddAsync(objEntity);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>>? Filter)
        {
            return await _dbSet.AnyAsync(Filter);
        }

    
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? IncludeProperties = null, int pageSize = 0, int pageNumber = 1, bool isTracking = false)
        {
            IQueryable<T> objQuery = isTracking ? _dbSet.AsQueryable() : _dbSet.AsNoTracking();

            if (filter != null)
            {
                objQuery = objQuery.Where(filter);
            }
            if (pageSize > 0)
            {
                if (pageSize > 100)
                {
                    pageSize = 100;
                }

                objQuery = objQuery.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            }
            if (IncludeProperties != null)
            {
                foreach (var includeProp in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    objQuery = objQuery.Include(includeProp);
                }
            }

            return await objQuery.ToListAsync();
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> filter, string? IncludeProperties = null, bool isTracking = false)
        {
            IQueryable<T> objQuery = isTracking ? _dbSet.AsQueryable() : _dbSet.AsNoTracking();

            if (filter != null)
            {
                objQuery = objQuery.Where(filter);
            }

            if (!string.IsNullOrEmpty(IncludeProperties))
            {
                foreach (var prop in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    objQuery = objQuery.Include(prop);
                }
            }

            return objQuery.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(T objEntity)
        {
            _dbSet.Remove(objEntity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
