using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain.Models;
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
        public async Task<AuthenticationResModel> ValidateCodeAndGetAccessToken(string code)
        {
            return await _authenticationService.Authenticate(code);
        }

        //[HttpPost("refresh")]
        //public async Task<AuthenticationResModel> RefreshAccessToken(string code)
        //{
        //    //jkgkhkil,jk/  
        //    return await _authenticationService.Authenticate(code);
        //}
    }
}
