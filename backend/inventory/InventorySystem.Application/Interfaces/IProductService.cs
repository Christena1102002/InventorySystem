using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Application.Interfaces
{
    public interface IProductService
    {
       Task<Result<ProductDTO>> CreateProductAsync(CreateProductDTO dTO);

        Task<Result<List<ProductDTO>>> GetAllProductsAsync();

        Task<Result<PagedResult<ProductDTO>>> GetProductsPagedAsync(int pageNumber, int pageSize, string? search = null);
    }
}
