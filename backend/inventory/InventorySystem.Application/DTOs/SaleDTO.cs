using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Application.DTOs
{
    public class SaleDTO
    {
        public int id {  get; set; }
        public int ProductId {  get; set; }
        public int Quantity { get; set; }

    }
}
