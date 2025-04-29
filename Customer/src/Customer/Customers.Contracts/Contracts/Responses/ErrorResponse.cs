namespace Customers.Api.Contracts.Responses
{
	public class ErrorResponse
	{
		public int CustomerId { get; private set; }

		public ErrorResponse(int customerId)
		{
			CustomerId = customerId;
		}

		public List<Error> Errors { get; private set; } = new List<Error>();
	}

	public class Error
	{
		public string Message { get; set; }
	}
}
