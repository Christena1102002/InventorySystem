using InventorySystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Infrastracture.Data
{
    public class InventoryDbContext : IdentityDbContext<ApplicationUser>
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }
        public DbSet<Product> products { get; set; }
        public DbSet<Sale> sales { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "1",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            );
        }

    }
}
