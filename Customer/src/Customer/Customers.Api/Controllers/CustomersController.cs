using Customers.Api.Contracts.Requests;
using Customers.Api.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class CustomersController : ControllerBase
	{
		private readonly ICustomerService _customerService;

		public CustomersController(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _customerService.GetAll();
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] List<CreateCustomerRequest> createCustomerRequests)
		{
			await _customerService.Add(createCustomerRequests);
			return Ok();
		}
	}
}
