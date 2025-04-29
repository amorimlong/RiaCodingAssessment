using Customers.Api.Contracts.Responses;

namespace Customers.Api.CustomExceptions
{
	public class InvalidRequestException : Exception
	{
		public ErrorResponse ErrorResponse { get; init; }

		public InvalidRequestException(ErrorResponse errorResponse) : base("BAD Request")
		{
			ErrorResponse = errorResponse;
		}
	}
}
