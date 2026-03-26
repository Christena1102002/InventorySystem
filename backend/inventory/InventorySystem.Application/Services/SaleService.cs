using AutoMapper;
using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using InventorySystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public SaleService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public async Task<Result<SaleDTO>> CreateSaleAsync(CreateSaleDTO dTO)
        {
            var product = await unitOfWork.Repository<Product>().GetByIdAsync(dTO.ProductId);
            if (product == null)
 
                return Result<SaleDTO>.Failure("Product Not Found");


           if (dTO.Quantity > product.Quantity)
                    return Result<SaleDTO>.Failure("Not Enough Stock Available");

          var sale = mapper.Map<Sale>(dTO);

          await unitOfWork.Repository<Sale>().AddAsync(sale);

          product.Quantity -= dTO.Quantity;
            try
            {
                await unitOfWork.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                return Result<SaleDTO>.Failure("This product was updated by another user..please try again");
            }


          var result = mapper.Map<SaleDTO>(sale);

         return Result<SaleDTO>.Success(result, "Sale created successfuly");
        }
    }
}
