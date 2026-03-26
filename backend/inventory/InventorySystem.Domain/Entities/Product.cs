using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Domain.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;
    }
}
