using App.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
		[NonAction] // endpoint olarak algılamaması için
        public IActionResult CreateActionResult<T>(ServiceResult<T> result) // generic metot
		{
			return result.Status switch
			{
				HttpStatusCode.NoContent => NoContent(),
				HttpStatusCode.Created => Created(result.UrlAsCreated, result.Data),
				_ => new ObjectResult(result) { StatusCode = result.Status.GetHashCode() }
			};

		}
		[NonAction]
		public IActionResult CreateActionResult(ServiceResult result)  // generic olmayan metot
		{
			return result.Status switch
			{
				HttpStatusCode.NoContent => new ObjectResult(null) { StatusCode = result.Status.GetHashCode() },
				_ => new ObjectResult(result) { StatusCode = result.Status.GetHashCode() }
			};

		}
	}
}
