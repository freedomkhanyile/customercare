using CustomerCare.Api.Models.Customers;
using CustomerCare.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Services.Contracts
{
    public interface ICustomerService
    {
        IQueryable<Customer> Get();
        Customer Get(int id);
        Task<Customer> CreateAsync(CreateCustomerViewModel model);
        Task<Customer> UpdateAsync(int id, UpdateCustomerViewModel model);
        Task<bool> Delete(int id);
    }
}
