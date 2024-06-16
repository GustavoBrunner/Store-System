using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace user_api.Models.Context
{
    public class UserDbContext : DbContext
    {
        
        public DbSet<UserModel> Users { get; set; }

        public DbSet<AddressModel> Addresses { get; set; }

        

        public UserDbContext(DbContextOptions options) : base(options){ }



        protected override void OnModelCreating(ModelBuilder mB)
        {
            
        }
    }
}