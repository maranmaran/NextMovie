using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Provides a way to sign in and manipulate resources
    /// </summary>
    public class AuthenticationController : ControllerBase
    {
        //private readonly IAuthenticationService _authenticationService;

        //public AuthenticationController(IAuthenticationService authenticationService)
        //{
        //    _authenticationService = authenticationService;
        //}

        ///// <summary>
        ///// Sign in as admin
        ///// </summary>
        //[AllowAnonymous]
        //[HttpGet("{username}/{password}")]
        //public async Task<IActionResult> SignIn(string username, string password, CancellationToken cancellationToken = default)
        //{
        //    var token = await _authenticationService.SignIn(username, password, cancellationToken);

        //    Response.Cookies.Append(JwtBearerDefaults.AuthenticationScheme, token);

        //    return Accepted();
        //}
    }
}
