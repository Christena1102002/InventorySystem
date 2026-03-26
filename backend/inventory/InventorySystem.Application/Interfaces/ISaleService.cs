using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Application.Interfaces
{
    public interface ISaleService
    {
        Task<Result<SaleDTO>> CreateSaleAsync(CreateSaleDTO dTO);
    }
}
