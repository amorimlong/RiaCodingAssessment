using Customers.Api.Contracts.Requests;
using Customers.Api.Contracts.Responses;
using Customers.Api.CustomExceptions;
using Customers.Api.Domain.Interfaces;

namespace Customers.Api.Domain.Validators
{
	public class CustomerRequestValidator
	{
		private ICustomerRepository _customerRepository;
		public CustomerRequestValidator(ICustomerRepository customerRepository)
		{
			_customerRepository = customerRepository;
			;
		}
		public async Task ValidateAsync(CreateCustomerRequest request)
		{
			ErrorResponse errorResponse = new ErrorResponse(request.Id);
			
			var clientIdMessage = $"There is an error with Customer Id = {request.Id}";

			if (request == null)
			{
				errorResponse.Errors.Add(new Error { Message = "Request is required" });
			}

			if (request.Age < 18)
			{
				errorResponse.Errors.Add(new Error { Message = $"Customer age must be above 18" });
			}

			ValidateName(request, errorResponse);

			if (await _customerRepository.GetByIdAsync(request.Id) != null)
			{
				errorResponse.Errors.Add(new Error { Message = "There is a Customer with this Id number!"});
			}

			if (errorResponse.Errors.Any())
			{
				throw new InvalidRequestException(errorResponse);
			}
		}

		private void ValidateName(CreateCustomerRequest request, ErrorResponse errorResponse)
		{
			if (string.IsNullOrEmpty(request.FirstName))
			{
				errorResponse.Errors.Add(new Error { Message = "Customer First Name is required" });
			}

			if (string.IsNullOrEmpty(request.LastName))
			{
				errorResponse.Errors.Add(new Error { Message = "Customer Last Name is required" });
			}
		}
	}
}
