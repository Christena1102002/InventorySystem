using InventorySystem.Domain.Interfaces;
using InventorySystem.Infrastracture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Infrastracture.Repositories
{
    public class UnitOfWork : IUnitOfWork ,IDisposable
    {
        private readonly InventoryDbContext _context;
        private Dictionary<string, Object> _repositories;
        public IProductRepository Products { get; }
        public UnitOfWork(InventoryDbContext context)
        {
            _context = context;
            Products=new ProductRepository(context);
        }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            _repositories ??= new Dictionary<string, object>();
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
