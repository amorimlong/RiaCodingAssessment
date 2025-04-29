using Customers.Api.Contracts.Requests;
using Customers.Api.CustomExceptions;
using Customers.Api.Domain.Entities;
using Customers.Api.Domain.Interfaces;
using Customers.Api.Domain.Services;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;

namespace Customers.Api.Tests.Domain.Services
{
	public class CustomerServiceTests
	{
		private CustomerService _sut;
		private readonly ICustomerRepository _customerRepository;
		private readonly IMemoryCache _memoryCache;

		public CustomerServiceTests()
		{
			_customerRepository = Substitute.For<ICustomerRepository>();
			_memoryCache = Substitute.For<IMemoryCache>();
			_sut = new CustomerService(_customerRepository, _memoryCache);
		}

		[Fact]
		public async Task Add_UserAgeUnder18_ThrowInvalidRequestException()
		{
			var request = new List<CreateCustomerRequest>
			{
				{new CreateCustomerRequest { Age = 17, FirstName = "Test", LastName = "Api", Id = 1 } },
			};

			var invalidRequestException = await Assert.ThrowsAsync<InvalidRequestException>(() => _sut.Add(request));

			Assert.Equal("Customer age must be above 18", invalidRequestException.ErrorResponse.Errors.First().Message);
		}

		[Fact]
		public async Task Add_UserWithoutFirstName_ThrowInvalidRequestException()
		{
			var request = new List<CreateCustomerRequest>
			{
				{new CreateCustomerRequest { Age = 18, FirstName = string.Empty, LastName = "Api", Id = 1 } },
			};

			var invalidRequestException = await Assert.ThrowsAsync<InvalidRequestException>(() => _sut.Add(request));

			Assert.Equal("Customer First Name is required", invalidRequestException.ErrorResponse.Errors.First().Message);
		}

		[Fact]
		public async Task Add_UserWithoutLastName_ThrowInvalidRequestException()
		{
			var request = new List<CreateCustomerRequest>
			{
				{new CreateCustomerRequest { Age = 18, FirstName = "Test", LastName = string.Empty, Id = 1 } },
			};

			var invalidRequestException = await Assert.ThrowsAsync<InvalidRequestException>(() => _sut.Add(request));

			Assert.Equal("Customer Last Name is required", invalidRequestException.ErrorResponse.Errors.First().Message);
		}

		[Fact]
		public async Task Add_UserIdAlreadyExists_ThrowInvalidRequestException()
		{
			var request = new List<CreateCustomerRequest>
			{
				{new CreateCustomerRequest { Age = 18, FirstName = "Test", LastName = "Api", Id = 1 } },
			};

			_customerRepository.GetByIdAsync(Arg.Any<int>()).Returns(new Customer());
			var invalidRequestException = await Assert.ThrowsAsync<InvalidRequestException>(() => _sut.Add(request));

			Assert.Equal("There is a Customer with this Id number!", invalidRequestException.ErrorResponse.Errors.First().Message);
		}

		[Fact]
		public async Task Add_ShouldAdd()
		{
			var request = new List<CreateCustomerRequest>
			{
				{new CreateCustomerRequest { Age = 18, FirstName = "Test", LastName = "Api", Id = 1 } },
			};

			await _sut.Add(request);

			_memoryCache.Received(1).Remove("customers");
			await _customerRepository.Received(1).AddAsync(Arg.Any<Customer>());
		}

		[Fact]
		public async Task GetAll_KeyExistsCache_ShouldGetFromCache()
		{
			var memCache = new MemoryCache(new MemoryCacheOptions());
			var customers = new List<Customer>();
			memCache.Set("customers", new List<Customer>());

			var sut = new CustomerService(_customerRepository, memCache);
			await sut.GetAll();

			await _customerRepository.DidNotReceive().GetAllAsync();
		}

		[Fact]
		public async Task GetAll_CacheEmpty_ShouldGetFromRepository()
		{
			var customers = new List<Customer>();
			_memoryCache.TryGetValue("customers", out Arg.Any<List<Customer>>()).Returns(false);

			_customerRepository.GetAllAsync().Returns(c => Task.FromResult(customers));

			await _sut.GetAll();

			await _customerRepository.Received(1).GetAllAsync();
		}
	}
}
