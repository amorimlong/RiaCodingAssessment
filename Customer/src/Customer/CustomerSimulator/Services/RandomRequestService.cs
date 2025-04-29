using Customers.Api.Contracts.Requests;
using CustomerSimulator.ApiClients;

namespace CustomerSimulator.Services
{
	public class RandomRequestService
	{
		const int MinAge = 10;
		const int MaxAge = 90;

		private int nextId = 0;
		private Random random = new();
		SemaphoreSlim semaphore = new(1);


		public async Task<List<CreateCustomerRequest>> CreateCustomerRequestsAsync()
		{
			var createCustomersRequest = new List<CreateCustomerRequest>();
			var qtdCustomerPerRequest = random.Next(2, 5);

			for (int i = 0; i < qtdCustomerPerRequest; i++)
			{
				createCustomersRequest.Add(await CreateRequest());
			}

			return createCustomersRequest;
		}

		private async Task<CreateCustomerRequest> CreateRequest()
		{
			await semaphore.WaitAsync();
			int id = await GetNexId();
			semaphore.Release();

			return new CreateCustomerRequest
			{
				Id = id,
				Age = GenerateRamdonAge(),
				FirstName = GenerateFirstName(),
				LastName = GenerateLastName(),
			};
		}

		private async Task<int> GetNexId()
		{
			if (nextId == 0)
			{
				var apiClient = new CustomerApiClient();
				var customers = await apiClient.GetResponseAsync();
				if (customers is not null && customers.Count > 0)
				{
					nextId = customers.Max(c => c.Id);
				}
			}

			nextId = nextId + 1;
			return nextId;
		}

		private int GenerateRamdonAge()
		{
			return random.Next(MinAge, MaxAge);
		}

		private string GenerateFirstName()
		{
			var firstNames = new Dictionary<int, string>
			{
				{ 1, "Leia" },
				{ 2, "Sadie" },
				{ 3, "Jose" },
				{ 4, "Sara" },
				{ 5, "Frank" },
				{ 6, "Dewey" },
				{ 7, "Tomas" },
				{ 8, "Joel" },
				{ 9, "Lukas" },
				{ 10, "Carlos" },
			};

			return GenerateName(firstNames);
		}

		private string GenerateLastName()
		{
			var lastNames = new Dictionary<int, string>
			{
				{ 1, "Liberty" },
				{ 2, "Ray" },
				{ 3, "Harrison" },
				{ 4, "Ronan" },
				{ 5, "Drew" },
				{ 6, "Powell" },
				{ 7, "Larsen" },
				{ 8, "Chan" },
				{ 9, "Anderson" },
				{ 10, "Lane" },
			};

			return GenerateName(lastNames);
		}

		private string GenerateName(Dictionary<int, string> names)
		{
			return names.GetValueOrDefault(random.Next(1, 10))!;
		}
	}
}
