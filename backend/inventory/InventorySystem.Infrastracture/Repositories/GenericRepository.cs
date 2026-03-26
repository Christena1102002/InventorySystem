using InventorySystem.Domain.Interfaces;
using InventorySystem.Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Infrastracture.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly InventoryDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(InventoryDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public IQueryable<T> GetAllQueryable()
        {
            return _dbSet.AsNoTracking(); // lazy query
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        //public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        //{
        //    return await _dbSet.Where(predicate).ToListAsync();
        //}

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
        //public void RemoveRange(IEnumerable<T> entities)
        //{
        //    _dbSet.RemoveRange(entities);
        //}


        //public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        //{
        //    return await _context.Set<T>().CountAsync(predicate);
        //}

    }
}
