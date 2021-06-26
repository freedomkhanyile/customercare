using CustomerCare.Api.Extensions;
using CustomerCare.Api.Helpers;
using CustomerCare.Api.Models.Customers;
using CustomerCare.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Type = CustomerCare.Data.Models.Type;

namespace CustomerCare.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {

        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerService _customerService;
        private readonly ICustomerTypeService _customerTypeService;
        public CustomersController(ILogger<CustomersController> logger, ICustomerService customerService, ICustomerTypeService customerTypeService)
        {
            _logger = logger;
            _customerService = customerService;
            _customerTypeService = customerTypeService;
        }

        [HttpGet]
        public IEnumerable<CustomerViewModel> GetAll()
        {
            var customers = _customerService.Get();

            return customers.Select(x => x.ToViewModel(null)).ToList();
        }

        [HttpGet("{id}")]
        public CustomerViewModel Get(int id)
        {
            var customer = _customerService.Get(id);
            var types = _customerTypeService.GetCustomerTypes(customer.Id).ToList();
            return types.Count > 0 ? customer.ToViewModel(types[0]?.Name) : customer.ToViewModel();
        }

        [HttpPost]
        [ValidateModel]
        public async Task<CustomerViewModel> PostAsyc([FromBody] CreateCustomerViewModel model)
        {
            var item = await _customerService.CreateAsync(model);
            var type = await CreateCustomerType(item.Id, model.CustomerType);
            return item.ToViewModel(type.Name);
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<CustomerViewModel> PutAsync(int id, [FromBody] UpdateCustomerViewModel model)
        {
            var item = await _customerService.UpdateAsync(id, model);
            return item.ToViewModel();
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(int id)
        {
           return await _customerService.Delete(id);
        }

        private async Task<Type> CreateCustomerType(int customerId, string typeName)
        {
            var result = await _customerTypeService.CreateCustomerType(customerId, typeName);
            return result;
        }

    }
}
