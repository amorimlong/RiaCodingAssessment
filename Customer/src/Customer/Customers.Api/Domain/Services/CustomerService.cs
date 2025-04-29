using Customers.Api.Contracts.Requests;
using Customers.Api.Domain.Entities;
using Customers.Api.Domain.Interfaces;
using Customers.Api.Domain.Validators;
using Customers.Api.Mappers;
using Microsoft.Extensions.Caching.Memory;

namespace Customers.Api.Domain.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly IMemoryCache _memoryCache;
		private const string CacheKey = "customers";
		public CustomerService(ICustomerRepository customerRepository, IMemoryCache memoryCache)
		{
			_customerRepository = customerRepository;
			_memoryCache = memoryCache;
		}

		public async Task Add(List<CreateCustomerRequest> createCustomersRequest)
		{
			foreach (var request in createCustomersRequest) 
			{
				var validator = new CustomerRequestValidator(_customerRepository);
				await validator.ValidateAsync(request);

				var customer = request.MapToEntity();

				await _customerRepository.AddAsync(customer);
			}

			_memoryCache.Remove(CacheKey);
		}

		public async Task<List<CustomerResponse>> GetAll()
		{
			var customers = new List<Customer>();

			if(!_memoryCache.TryGetValue(CacheKey, out customers))
			{
				customers = await _customerRepository.GetAllAsync();
				_memoryCache.Set(CacheKey, customers, 
					new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(1)));
			}

			return customers.MapToResponseList();
		}
	}
}
