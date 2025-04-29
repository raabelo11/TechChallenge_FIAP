using FCG.Application.Interfaces;
using FCG.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var response = await _authService.Login(login);
            return response.Ok ? Ok(response) : Unauthorized(response);
        }        
    }
}
