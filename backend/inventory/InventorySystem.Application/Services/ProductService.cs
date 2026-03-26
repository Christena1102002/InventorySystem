using AutoMapper;
using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using InventorySystem.Domain.Interfaces;
using InventorySystem.Infrastracture.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ProductService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public async Task<Result<ProductDTO>> CreateProductAsync(CreateProductDTO dTO)
        {
            if (dTO == null)
                return Result<ProductDTO>.Failure("Product data is required!");
            var product = mapper.Map<Product>(dTO);
            await unitOfWork.Repository<Product>().AddAsync(product);

           await unitOfWork.SaveChangesAsync();
            var productDto = mapper.Map<ProductDTO>(product);

            return Result<ProductDTO>.Success(productDto,"Product created successfuly");
        }
        public async Task<Result<List<ProductDTO>>> GetAllProductsAsync()
        {
           var products=await unitOfWork.Repository<Product>().GetAllAsync();
            var productDto=mapper.Map<List<ProductDTO>>(products);
            return Result<List<ProductDTO>>.Success(productDto, "Get All Successfully");
        }

      public async  Task<Result<PagedResult<ProductDTO>>> GetProductsPagedAsync(int pageNumber, int pageSize, string? search = null)
        {
            var (products, totalCount) = await unitOfWork.Products.GetPagedAsync(pageNumber, pageSize ,search);
           
            var data = mapper.Map<List<ProductDTO>>(products);

            var result = new PagedResult<ProductDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = data
            };

            return Result<PagedResult<ProductDTO>>.Success(result, "Get All Successfully");
        }


    }
}
