using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Domain.Entities
{
    public class Sale :BaseEntity
    {
        public int Quantity { get; set; }

        public int ProductId {  get; set; }
        public Product Product { get; set; }    

    }
}
