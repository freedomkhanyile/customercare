using CustomerCare.Api.Models.Customers;
using CustomerCare.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCare.Api.Extensions
{
    public static class CustomerExtensions
    {
        public static CustomerViewModel ToViewModel(this Customer customer, string typeName = null)
        {
            return new CustomerViewModel
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Cellphone = customer.Cellphone,
                AmountTotal = customer.AmountTotal,
                Date = customer.Date,
                IsDeleted = customer.IsDeleted,
                CustomerType = typeName
            };
        }
    }
}
