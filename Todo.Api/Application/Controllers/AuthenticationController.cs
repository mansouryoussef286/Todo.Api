using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain.Services;

namespace Todo.Api.Application.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;
        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet("{code}")]
        public string ValidateCodeAndGetAccessToken(string code)
        {
            return "value";
        }
    }
}
