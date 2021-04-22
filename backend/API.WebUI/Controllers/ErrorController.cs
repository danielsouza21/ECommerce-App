using API.WebUI.ErrorHandlers;
using Microsoft.AspNetCore.Mvc;

namespace API.WebUI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("errors/{code}")]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
