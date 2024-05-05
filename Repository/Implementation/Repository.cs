using DatabaseAccess;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDBContext _db;
        private DbSet<T> _dbSet;
        public Repository(ApplicationDBContext db)
        {
            _db = db;
            _dbSet =  _db.Set<T>();

        }

        public async Task AnyAsync(Expression<Func<T, bool>>? Filter)
        {          
            _dbSet.Any(Filter);
        }

        public async Task CreateAync(T objEntity)
        {
            await _dbSet.AddAsync(objEntity);
            await SaveAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? IncludeProperties = null, bool isTracking = false)
        {

            IQueryable<T> objQuery = isTracking ? _dbSet.AsQueryable() : _dbSet.AsNoTracking();

            objQuery = filter != null ? objQuery.Where(filter) : objQuery;

            if (!string.IsNullOrEmpty(IncludeProperties))
            {
                foreach (var prop in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    objQuery = objQuery.Include(prop);
                }
            }

            return await objQuery.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, string? IncludeProperties = null, bool isTracking = false)
        {
            IQueryable<T> objQuery = isTracking ? _dbSet.AsQueryable() : _dbSet.AsNoTracking();

            objQuery = filter != null ? objQuery.Where(filter) : objQuery;


            if (!string.IsNullOrEmpty(IncludeProperties))
            {
                foreach (var prop in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    objQuery = objQuery.Include(prop);
                }
            }

            return await objQuery.FirstOrDefaultAsync();
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
