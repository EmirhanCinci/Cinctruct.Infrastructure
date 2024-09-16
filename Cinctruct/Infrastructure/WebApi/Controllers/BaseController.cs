using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebApi.Controllers
{
	/// <summary>
	/// Base controller that provides common functionalities for API controllers.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class BaseController : ControllerBase
	{
		/// <summary>
		/// Sends a standardized API response with a custom status code.
		/// </summary>
		/// <typeparam name="T">The type of the data in the API response..</typeparam>
		/// <param name="response">Custom API response containing the data and status code.</param>
		/// <returns>ObjectResult containing the response and status code.</returns>
		/// <remarks>
		/// This method cannot be called directly as an API action due to the [NonAction] attribute.
		/// </remarks>
		[NonAction]
		public IActionResult SendResponse<T>(CustomApiResponse<T> response) where T : class
		{
			return new ObjectResult(response) { StatusCode = response.StatusCode };
		}
	}
}
