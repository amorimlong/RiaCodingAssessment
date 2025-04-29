// See https://aka.ms/new-console-template for more information
using Customers.Api.Contracts.Requests;
using CustomerSimulator.ApiClients;
using CustomerSimulator.Services;

Console.WriteLine("Welcome to the Customers Simulator");

CustomerApiClient apiClient = new CustomerApiClient();
RandomRequestService randomRequestService = new RandomRequestService();

var random = new Random();

Task.WaitAll(
	Task.Run(() => GetCustomers()),
	Task.Run(() => SendMessage())
);

await Task.Run(async () => PrintCustomersResponse(await apiClient.GetResponseAsync()));

Console.WriteLine("Good Bye :)");

async Task SendMessage()
{
	var qtdMaxMessageSending = random.Next(5, 10);

	for (int i = 0 ; i < qtdMaxMessageSending; i++)
	{
		Thread.Sleep(1000);


		var requests = await randomRequestService.CreateCustomerRequestsAsync();
		var response = await apiClient.SendRequestAsync(requests);

		if (!response.IsSuccessStatusCode)
		{
			Console.WriteLine(await response.Content.ReadAsStringAsync());
		}
	}
}

async Task GetCustomers()
{
	var maxQtdToGetCustomers = random.Next(2, 5);
	for (int i = 0; i < maxQtdToGetCustomers; i++)
	{
		Thread.Sleep(2000);
		var customersResult = await apiClient.GetResponseAsync();
		PrintCustomersResponse(customersResult);
	}
}

void PrintCustomersResponse(List<CustomerResponse>? customerResponses)
{
	Console.WriteLine("---------------------------------------------------------------");

	if(customerResponses is null || customerResponses.Count == 0)
	{
		Console.WriteLine("There is no customer to be printed");
		return;
	}

	foreach (var customerResponse in customerResponses)
	{
		Console.WriteLine(@$"Id: {customerResponse.Id} LastName: {customerResponse.LastName} FirstName: {customerResponse.FirstName} Age: {customerResponse.Age}");
	}

	Console.WriteLine("---------------------------------------------------------------");
}