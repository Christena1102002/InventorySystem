using InventorySystem.Domain.Entities;
using InventorySystem.Domain.Interfaces;
using InventorySystem.Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Infrastracture.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDbContext _context;
        public ProductRepository(InventoryDbContext context)
        {
            _context = context;
        }
        public async Task<(List<Product>, int)> GetPagedAsync(int pageNumber, int pageSize, string? search = null)
        {
            var query=_context.products.AsQueryable();

            if(!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Name.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var data=await query.Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();
            return (data, totalCount);  
        }
    }
}
