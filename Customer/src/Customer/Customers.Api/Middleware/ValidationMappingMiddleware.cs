using Customers.Api.CustomExceptions;

namespace Customers.Api.Middleware
{
	public class ValidationMappingMiddleware
	{
		private readonly RequestDelegate _next;

		public ValidationMappingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (InvalidRequestException ex)
			{
				context.Response.StatusCode = 400;
				await context.Response.WriteAsJsonAsync(ex.ErrorResponse);
			}
		}
	}
}
