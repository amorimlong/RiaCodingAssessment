using Customers.Api.Contracts.Requests;
using Customers.Api.Domain.Entities;

namespace Customers.Api.Mappers
{
	public static class CustomerMapper
	{
		public static Customer MapToEntity(this CreateCustomerRequest request)
		{
			return new Customer
			{
				Age = request.Age,
				FirstName = request.FirstName,
				Id = request.Id,
				LastName = request.LastName
			};
		}

		public static CustomerResponse MapToResponse(this Customer customer)
		{
			return new CustomerResponse
			{
				Age = customer.Age,
				FirstName = customer.FirstName,
				Id = customer.Id,
				LastName = customer.LastName
			};
		}

		public static List<CustomerResponse> MapToResponseList(this List<Customer> customers)
		{
			return customers.Select(MapToResponse).ToList();
		}
	}
}
