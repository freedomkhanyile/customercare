using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerCare.Data.Contracts;
using CustomerCare.Data.Models;
using CustomerCare.Services;
using CustomerCare.Services.Contracts;
using FluentAssertions;
using Moq;
using Xunit;
using Type = CustomerCare.Data.Models.Type;

namespace CustomerCare.Tests
{
    public class CustomerTypeServiceTests
    {
        private Mock<IUnitOfWork> _uow;
        private List<Type> _types;
        private List<CustomerType> _customerTypes;
        private Random _random;
        private Customer _defaultCustomer;
        private ICustomerTypeService _customerTypeService;

        public CustomerTypeServiceTests()
        {
            _uow = new Mock<IUnitOfWork>();
            _random = new Random();
            _types = new List<Type>();
            _uow.Setup(x => x.Query<Type>())
                .Returns(() => _types.AsQueryable());

            _customerTypes = new List<CustomerType>();
            _uow.Setup(x => x.Query<CustomerType>())
                .Returns(() => _customerTypes.AsQueryable());
            _defaultCustomer = new Customer {Id = _random.Next()};
            _customerTypeService = new CustomerTypeService(_uow.Object);
        }

        [Fact]
        public void GetShouldReturnAllTypesForCustomer()
        {
            // Arrange
            _types.Add(new Type{ Id = 1, Name = "Person"});
            _types.Add(new Type { Id =2, Name = "Organization" });
            _types.Add(new Type { Id = 3, Name = "Government" });

            _customerTypes.Add(new CustomerType { CustomerId = _defaultCustomer.Id, TypeId = 1});

            // Act
            var result = _customerTypeService.GetCustomerTypes(_defaultCustomer.Id);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(1);
        }

        [Fact]
        public void GetByIdShouldReturnType()
        {
            // Arrange
            _types.Add(new Type { Id = 1, Name = "Person" });
            _types.Add(new Type { Id = 2, Name = "Organization" });
            _types.Add(new Type { Id = 3, Name = "Government" });

            // Act
            var result = _customerTypeService.Get(1);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(_types[0].Id);
            result.Name.Should().Be(_types[0].Name);
        }

        [Fact]
        public void GetByNameShouldReturnType()
        {
            // Arrange
            _types.Add(new Type { Id = 1, Name = "Person" });
            _types.Add(new Type { Id = 2, Name = "Organization" });
            _types.Add(new Type { Id = 3, Name = "Government" });

            // Act
            var result = _customerTypeService.Get("Person");

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(_types[0].Id);
            result.Name.Should().Be(_types[0].Name);
        }

        [Fact]
        public async Task CreateCustomerTypeShouldSave()
        {
            // Arrange
            _types.Add(new Type { Id = 1, Name = "Person" });
            _types.Add(new Type { Id = 2, Name = "Organization" });
            _types.Add(new Type { Id = 3, Name = "Government" });

            // Act
            var result = await _customerTypeService.CreateCustomerType(_defaultCustomer.Id, "Person");

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(_types[0].Id);
            result.Name.Should().Be(_types[0].Name);
        }


    }
}
