using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult SendResponse<T>(CustomApiResponse<T> response) where T : class
        {
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
