using App.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;


namespace CleanApp.API.ExceptionHandler
{
	public class CriticalExceptionHandler() : IExceptionHandler
	{
		public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
		{
			if (exception is CriticalException)
			{
				Console.WriteLine("Sms gönderildi.");
			}

			// business logic
			return ValueTask.FromResult(false);
		}
	}
}
