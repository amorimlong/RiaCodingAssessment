namespace Customers.Api.Domain.Entities
{
	public class Customer
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public int Age { get; set; }

		public string FullName()
		{
			return $"{LastName}{FirstName}";
		}
	}
}
