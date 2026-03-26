using InventorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<(List<Product>,int)> GetPagedAsync(int pageNumber , int pageSize,string? search=null);
    }
}
