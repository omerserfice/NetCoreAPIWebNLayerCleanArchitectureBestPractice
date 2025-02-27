using App.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
		[NonAction] // endpoint olarak algılamaması için
        public IActionResult CreateActionResult<T>(ServiceResult<T> result) // generic metot
		{
			if (result.Status == System.Net.HttpStatusCode.NoContent)
			{
				return new ObjectResult(null) { StatusCode = result.Status.GetHashCode() };
			}

			return new ObjectResult(result) { StatusCode = result.Status.GetHashCode() };

		}
		[NonAction]
		public IActionResult CreateActionResult(ServiceResult result)  // generic olmayan metot
		{
			if (result.Status == System.Net.HttpStatusCode.NoContent)
			{
				return new ObjectResult(null) { StatusCode = result.Status.GetHashCode() };
			}

			return new ObjectResult(result) { StatusCode = result.Status.GetHashCode() };

		}
	}
}
