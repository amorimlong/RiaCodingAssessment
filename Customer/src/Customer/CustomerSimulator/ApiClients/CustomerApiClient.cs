using Customers.Api.Contracts.Requests;
using System.Net.Http.Json;
using System.Text.Json;

namespace CustomerSimulator.ApiClients
{
	public class CustomerApiClient
	{
		const string ApiBaseUrl = "http://localhost:8080/Customers";

		private HttpClient client;
		private List<CustomerResponse>? cache = null;

		public CustomerApiClient()
		{
			client = new HttpClient();
		}

		public async Task<HttpResponseMessage> SendRequestAsync(List<CreateCustomerRequest> createCustomerRequests)
		{
			var requests = createCustomerRequests;
			cache = null;
			return await client.PostAsJsonAsync(ApiBaseUrl, requests);
		}

		public async Task<List<CustomerResponse>?> GetResponseAsync()
		{
			if (cache is not null) 
			{
				return cache;
			}

			using HttpClient client = new HttpClient();
			var response = await client.GetAsync(ApiBaseUrl);

			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStreamAsync();
				var customerResponses = await JsonSerializer
					.DeserializeAsync<List<CustomerResponse>>(jsonResponse,
					new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true
					});

				cache = customerResponses;
				return customerResponses;
			}
			throw new Exception($"Error tryng to get customers from api CODE: {response.StatusCode}");
		}
	}
}
