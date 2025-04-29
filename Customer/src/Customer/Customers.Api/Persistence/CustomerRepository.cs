using Customers.Api.Domain.Entities;
using Customers.Api.Domain.Interfaces;
using System.Text.Json;

namespace Customers.Api.Persistence
{
	public class CustomerRepository : ICustomerRepository
	{
		private List<Customer> _customers;
		public CustomerRepository()
		{
			try
			{
				var jsonString = File.ReadAllText("Customers.Json");
				_customers = JsonSerializer.Deserialize<List<Customer>>(jsonString);
			}
			catch
			{
				_customers = new();

			}
		}

		public async Task AddAsync(Customer customer)
		{
			await Task.Run(() =>
			{
				if (_customers.Count == 0)
				{
					_customers.Add(customer);
					return;
				}

				AddCustomerOrdered(customer);
			});

			string jsonString = JsonSerializer.Serialize(_customers);
			File.WriteAllText("Customers.Json", jsonString);
		}

		//private void AddCustomerOrdered(Customer customer)
		//{
		//	for (int i = 0; i < _customers.Count; i++)
		//	{
		//		var item = _customers[i];

		//		if (item.FullName().ToLower().CompareTo(customer.FullName().ToLower()) > 0)
		//		{
		//			_customers.Insert(i, customer);
		//			return;

		//		}
		//	}
		//	_customers.Add(customer);
		//}

		private void AddCustomerOrdered(Customer customer)
		{
			int minIndex = 0;
			int maxIndex = _customers.Count;

			while (minIndex < maxIndex)
			{
				int middle = minIndex + ((maxIndex - minIndex) / 2);

				var item = _customers[middle];
				var compareResult = StringComparer.OrdinalIgnoreCase.Compare(item.FullName(), customer.FullName());

				if (compareResult > 0)
				{
					maxIndex = middle;
				}
				if (compareResult < 0)
				{
					minIndex = middle + 1;
				}
				if(compareResult == 0)
				{
					_customers.Insert(middle, customer);
					return;
				}

				if (minIndex >= maxIndex)
				{
					var index = middle;

					if (compareResult < 0)
					{
						index = ++index;
					}

					if (index >= _customers.Count)
					{
						_customers.Add(customer);
						return;
					}

					_customers.Insert(index, customer);
					return;
				}
			}
		}

		public async Task<List<Customer>> GetAllAsync()
		{
			return await Task.FromResult(_customers.ToList());
		}

		public async Task<Customer?> GetByIdAsync(int id)
		{
			return await Task.Run(() => _customers.FirstOrDefault(c => c.Id == id));
		}
	}
}
