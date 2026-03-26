using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Application.DTOs
{
    public class CreateProductDTO
    {
        public string Name { get; set; }

        public int Quantity {  get; set; }
    }
}
