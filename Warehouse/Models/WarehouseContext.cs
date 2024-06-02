using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    internal class WarehouseContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ShopCart> ShopCarts { get; set; }
        public DbSet<Order> Orders { get; set; }

        public WarehouseContext() 
        { 
           // Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Warehouse.db");
        }
    }
}
