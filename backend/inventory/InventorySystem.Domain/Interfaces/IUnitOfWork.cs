using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        IProductRepository Products { get; }
        Task<int> SaveChangesAsync();

    }
}
