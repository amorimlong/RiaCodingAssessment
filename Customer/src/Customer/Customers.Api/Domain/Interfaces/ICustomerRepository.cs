using Customers.Api.Domain.Entities;

namespace Customers.Api.Domain.Interfaces
{
	public interface ICustomerRepository
	{
		Task AddAsync(Customer customer);
		Task<List<Customer>> GetAllAsync();
		Task<Customer?> GetByIdAsync(int id);
	}
}
