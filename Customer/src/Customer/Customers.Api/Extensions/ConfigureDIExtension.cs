using Customers.Api.Domain.Interfaces;
using Customers.Api.Domain.Services;
using Customers.Api.Persistence;

namespace Customers.Api.Extensions
{
	public static class ConfigureDIExtension
	{
		public static IServiceCollection AddApiDependecies(this IServiceCollection services) 
		{
			services.AddTransient<ICustomerService, CustomerService>();
			services.AddSingleton<ICustomerRepository, CustomerRepository>();
			services.AddMemoryCache();

			return services;
		}
	}
}
