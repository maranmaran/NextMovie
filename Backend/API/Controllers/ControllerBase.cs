using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Base controller implementation
    /// </summary>
    [ApiController]
    //[Authorize]
    [Route("[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
    }
}
