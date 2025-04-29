namespace Customers.Api.Contracts.Requests
{
	public class CustomerResponse
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public int Age { get; set; }
		public int Id { get; set; }
	}
}
