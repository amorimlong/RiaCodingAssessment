using Customers.Api.Contracts.Requests;

namespace Customers.Api.Domain.Interfaces
{
	public interface ICustomerService
	{
		Task Add(List<CreateCustomerRequest> customersRequest);
		Task<List<CustomerResponse>> GetAll();
	}
}
