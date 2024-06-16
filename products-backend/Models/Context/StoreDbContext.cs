using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace products_backend.Models.Context
{
    public class StoreDbContext: DbContext
    {
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder mB)
        {
            
        }

    }
}