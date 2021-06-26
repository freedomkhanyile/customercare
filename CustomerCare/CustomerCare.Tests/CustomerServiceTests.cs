using CustomerCare.Api.Models.Customers;
using CustomerCare.Data.Contracts;
using CustomerCare.Data.Models;
using CustomerCare.Services;
using CustomerCare.Services.Contracts;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomerCare.Tests
{
    public class CustomerServiceTests
    {
        private Mock<IUnitOfWork> _uow;
        private List<Customer> _customers;
        private Random _random;

        private ICustomerService _customerService;
        public CustomerServiceTests()
        {
            _uow = new Mock<IUnitOfWork>();
            _random = new Random();
            _customers = new List<Customer>();
            _uow.Setup(x => x.Query<Customer>()).Returns(() => _customers.AsQueryable());

            _customerService = new CustomerService(_uow.Object);
        }

        [Fact]
        public void GetShouldReturnAllCustomers()
        {
            // Arrange
            _customers.Add(new Customer { Id = _random.Next(), FirstName = _random.Next().ToString(), LastName = _random.Next().ToString() });
            _customers.Add(new Customer { Id = _random.Next(), FirstName = _random.Next().ToString(), LastName = _random.Next().ToString() });
            _customers.Add(new Customer { Id = _random.Next(), FirstName = _random.Next().ToString(), LastName = _random.Next().ToString() });
            // Act
            var result = _customerService.Get().ToList();
            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(3);
            result[0].Id.Should().Be(_customers[0].Id);
            result[1].Id.Should().Be(_customers[1].Id);
            result[2].Id.Should().Be(_customers[2].Id);
        }

        [Fact]
        public void GetShouldReturnAllCustomersExceptDeleted()
        {
            // Arrange
            _customers.Add(new Customer { Id = _random.Next(), FirstName = _random.Next().ToString(), LastName = _random.Next().ToString() });
            _customers.Add(new Customer { Id = _random.Next(), FirstName = _random.Next().ToString(), LastName = _random.Next().ToString() });
            _customers.Add(new Customer { Id = _random.Next(), FirstName = _random.Next().ToString(), LastName = _random.Next().ToString(), IsDeleted = true });
            // Act
            var result = _customerService.Get().ToList();
            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
            result[0].Id.Should().Be(_customers[0].Id);
            result[1].Id.Should().Be(_customers[1].Id);
        }

        [Fact]
        public void GetShouldReturnCustomerById()
        {
            // Arrange
            var id = _random.Next();
            _customers.Add(new Customer { Id = id, FirstName = _random.Next().ToString(), LastName = _random.Next().ToString() });
            _customers.Add(new Customer { Id = _random.Next(), FirstName = _random.Next().ToString(), LastName = _random.Next().ToString() });
            // Act
            var result = _customerService.Get(id);
            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        [Fact]
        public void GetShouldThrowExceptionIfNotFoundById()
        {
            // Arrange
            var id = _random.Next();
            _customers.Add(new Customer { Id = _random.Next(), FirstName = _random.Next().ToString(), LastName = _random.Next().ToString() });
            _customers.Add(new Customer { Id = _random.Next(), FirstName = _random.Next().ToString(), LastName = _random.Next().ToString() });

            // Act
            Action get = () =>
            {
                _customerService.Get(id);
            };

            // Assert
            get.Should().Throw<Exception>();
        }

        [Fact]
        public void GetShouldThrowExeptionIfCustomerIsDeleted()
        {
            // Arrange
            var id = _random.Next();
            _customers.Add(new Customer { Id = id, FirstName = _random.Next().ToString(), LastName = _random.Next().ToString(), IsDeleted = true });
            _customers.Add(new Customer { Id = _random.Next(), FirstName = _random.Next().ToString(), LastName = _random.Next().ToString() });

            // Act
            Action get = () =>
            {
                _customerService.Get(id);
            };
            // Assert
            get.Should().Throw<Exception>();
        }

        [Fact]
        public async Task CreateShouldSaveACustomer()
        {
            // Arrange
            var model = new CreateCustomerViewModel
            {
                FirstName = _random.Next().ToString(),
                LastName = _random.Next().ToString(),
                Email = _random.Next().ToString(),
                Cellphone = _random.Next().ToString(),
                Date = DateTime.Now
            };

            // Act
            var result = await _customerService.CreateAsync(model);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be(model.FirstName);

            _uow.Verify(x => x.Add(result));
            _uow.Verify(x => x.CommitAsync());
        }

        [Fact]
        public async Task UpdateShould_UpdateFields()
        {
            // Arrange
            var customer = new Customer 
            { 
                Id = _random.Next(), 
                FirstName = _random.Next().ToString(), 
                LastName = _random.Next().ToString(),
                Email = _random.Next().ToString(),
                Cellphone = _random.Next().ToString()                
            };
            _customers.Add(customer);
            var model = new UpdateCustomerViewModel
            {
                FirstName = _random.Next().ToString(),
                LastName = _random.Next().ToString(),
                Email = _random.Next().ToString(),
                Cellphone = _random.Next().ToString(),
                Date = DateTime.Now
            };

            // Act
            var result = await _customerService.UpdateAsync(customer.Id, model);

            // Assert
            result.Should().Be(customer);
            result.Id.Should().Be(customer.Id);
            result.FirstName.Should().Be(model.FirstName);
        }

        [Fact]
        public void UpdateShouldThrowException_IfItemToUpdate_NotFound()
        {
            Action update = () =>
            {
                var result = _customerService.UpdateAsync(_random.Next(), new UpdateCustomerViewModel()).Result;
            };
            update.Should().Throw<Exception>();
        }

        [Fact]
        public async Task DeleteShouldDeleteACustomerAndReturnTrue()
        {
            // Arrange
            _customers.Add(new Customer { Id = _random.Next(), FirstName = _random.Next().ToString(), LastName = _random.Next().ToString() });
            _customers.Add(new Customer { Id = _random.Next(), FirstName = _random.Next().ToString(), LastName = _random.Next().ToString() });

            // Act
            var result = await _customerService.Delete(_customers[0].Id);

            // Assert
            result.Should().BeTrue();
        } 
        [Fact]
        public async Task DeleteShouldThrowExceptionIfCustomerIsDeleted()
        {
            // Arrange
            _customers.Add(new Customer { Id = _random.Next(), FirstName = _random.Next().ToString(), LastName = _random.Next().ToString() });
            _customers.Add(new Customer { Id = _random.Next(), FirstName = _random.Next().ToString(), LastName = _random.Next().ToString() });
            _customers.Add(new Customer { Id = _random.Next(), FirstName = _random.Next().ToString(), LastName = _random.Next().ToString(), IsDeleted = true});
            // Act
            Action delete = () =>
            {
                var result = _customerService.Delete(_customers[2].Id).Result;
            };

            // Assert
            delete.Should().Throw<Exception>();
        }
    }
}
