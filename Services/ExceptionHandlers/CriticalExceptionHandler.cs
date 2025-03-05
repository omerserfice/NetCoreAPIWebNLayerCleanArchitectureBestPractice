using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.ExceptionHandlers
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
