using CustomerCare.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerCare.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedTypes(this ModelBuilder builder)
        {
            builder.Entity<Models.Type>().HasData(
                new Models.Type { Id = 1, Name = "Person" },
                new Models.Type { Id = 2, Name = "Organisation" },
                new Models.Type { Id = 3, Name = "Government" }
                );
        }

        public static void SeedCustomers(this ModelBuilder builder)
        {
            builder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@mail.com",
                    Cellphone = "074587545",
                    AmountTotal = 0.0,
                    Date = DateTime.Now
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "Fedi",
                    LastName = "Cash",
                    Email = "fedicashadmin@mail.com",
                    Cellphone = "07451215",
                    AmountTotal = 0.0,
                    Date = DateTime.Now
                },
               new Customer
               {
                   Id = 3,
                   FirstName = "Dpt Health",
                   LastName = "Saftey",
                   Email = "health@department.gov.za",
                   Cellphone = "011100444",
                   AmountTotal = 0.0,
                   Date = DateTime.Now
                });
        }

        public static void SeedCustomerTypes(this ModelBuilder builder)
        {
            builder.Entity<CustomerType>().HasData(
                new CustomerType
                {
                    Id = 1,
                    CustomerId = 1,
                    TypeId = 1
                },
                new CustomerType
                {
                    Id = 2,
                    CustomerId = 2,
                    TypeId = 2
                },
                new CustomerType
                {
                    Id = 3,
                    CustomerId = 3,
                    TypeId = 3
                });
        }


    }
}
