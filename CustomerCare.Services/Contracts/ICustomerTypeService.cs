using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Services.Contracts
{
    public interface ICustomerTypeService
    {
        // Type 

        IQueryable<Data.Models.Type> Get();
        Data.Models.Type Get(int id);
        Data.Models.Type Get(string name);
        // Customer type
        IQueryable<Data.Models.Type> GetCustomerTypes(int customerId);
        Task<Data.Models.Type> CreateCustomerType(int customerId, string typeName);


    }
}
