using CustomerCare.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerCare.Data.DAL
{
    public class CustomerCareDbContext: DbContext
    {
        public CustomerCareDbContext(DbContextOptions<CustomerCareDbContext> options)
            : base(options)
        {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var mappings = MappingsHelper.GetMappings();

            foreach (var item in mappings)
            {
                item.Visit(modelBuilder);
            }

            modelBuilder.SeedTypes();
            modelBuilder.SeedCustomers();
            modelBuilder.SeedCustomerTypes();
        }
    }
}
