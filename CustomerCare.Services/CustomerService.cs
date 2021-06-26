using CustomerCare.Api.Models.Customers;
using CustomerCare.Data.Contracts;
using CustomerCare.Data.Models;
using CustomerCare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _uow;

        public CustomerService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Customer> CreateAsync(CreateCustomerViewModel model)
        {
            var customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Cellphone = model.Cellphone,
                AmountTotal = model.AmountTotal,
                Date = DateTime.Now
            };

            _uow.Add(customer);
            await _uow.CommitAsync();

            return customer;
        }

        public async Task<bool> Delete(int id)
        {
            var customer = GetQuery().FirstOrDefault(x => x.Id == id);

            if (customer == null)
            {
                throw new Exception($"Customer not found for id: {id}");
            }

            if (customer.IsDeleted) return false;

            customer.IsDeleted = true;
            await _uow.CommitAsync();
            return true;
        }

        public IQueryable<Customer> Get()
        {
            var query = GetQuery();
            return query;
        }

        public Customer Get(int id)
        {
            var customer = GetQuery().FirstOrDefault(x => x.Id == id);
            if(customer == null)
            {
                throw new Exception($"Customer not found for id: {id}");
            }
            return customer;
        }

        public async Task<Customer> UpdateAsync(int id, UpdateCustomerViewModel model)
        {
            var customer = GetQuery().FirstOrDefault(x => x.Id == id);

            if(customer == null)
            {
                throw new Exception($"Customer not found for id: {id}");
            }

            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Email = model.Email;
            customer.Cellphone = model.Cellphone;
            customer.AmountTotal = model.AmountTotal;
            customer.IsDeleted = model.IsDeleted;

            await _uow.CommitAsync();
            return customer;
        }

        #region Private methods 
        private IQueryable<Customer> GetQuery()
        {
            var q = _uow.Query<Customer>()
                .Where(x => !x.IsDeleted);
            return q;
        }
        #endregion
    }
}
