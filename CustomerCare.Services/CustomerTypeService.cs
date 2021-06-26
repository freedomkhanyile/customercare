using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerCare.Data.Contracts;
using CustomerCare.Data.Models;
using CustomerCare.Services.Contracts;
using Microsoft.EntityFrameworkCore.Internal;
using Type = CustomerCare.Data.Models.Type;

namespace CustomerCare.Services
{
    public class CustomerTypeService: ICustomerTypeService
    {
        private readonly IUnitOfWork _uow;

        public CustomerTypeService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IQueryable<Type> Get()
        {
            var query = GetQuery();
            return query;
        }

        public Type Get(int id)
        {
            var type = GetQuery().FirstOrDefault(x => x.Id == id);
            if (type == null)
            {
                throw new Exception($"Type not found for id: {id}");
            }
            return type;
        }
        public Type Get(string name)
        {
            var type = GetQuery().FirstOrDefault(x => x.Name == name);
            if (type == null)
            {
                throw new Exception($"Type not found for name: {name}");
            }
            return type;
        }

        public IQueryable<Type> GetCustomerTypes(int customerId)
        {
            var customerTypes = GetCustomerTypesQuery(customerId).ToList();
            if (customerTypes == null) return null;
            var types = new List<Type>();
            foreach (var customerType in customerTypes)
            {
                types.Add(Get(customerType.TypeId));
            }

            return types.AsQueryable();
        }

        public async Task<Type> CreateCustomerType(int customerId, string name)
        {
            // get types and existing customer types for customer id.
            var type = Get(name);
            var customerTypes = GetCustomerTypes(customerId);
            if (customerTypes.Contains(type))
            {
                throw new Exception($"Customer type already exists.");
            }

            var customerType = new CustomerType
            {
                CustomerId = customerId,
                TypeId = type.Id
            };

            _uow.Add(customerType);
            await _uow.CommitAsync();

            return type;
        }

        #region Private methods 
        private IQueryable<Type> GetQuery()
        {
            var q = _uow.Query<Type>()
                .Where(x => x.Name != null);
            return q;
        }

        private IQueryable<CustomerType> GetCustomerTypesQuery(int customerId)
        {
            var q = _uow.Query<CustomerType>()
                .Where(x => x.CustomerId == customerId);
            return q;
        }
        #endregion
    }
}
