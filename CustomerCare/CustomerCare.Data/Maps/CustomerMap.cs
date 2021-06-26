using CustomerCare.Data.Maps.Common;
using CustomerCare.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerCare.Data.Maps
{
    public class CustomerMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Customer>()
                .ToTable("Customers")
                .HasKey(x => x.Id);
        }
    }
}
