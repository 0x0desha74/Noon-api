using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Noon.API.Errors;

namespace Noon.API.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            return NotFound(new ApiResponse(code));
        }
    }
}
