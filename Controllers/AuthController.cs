
using ASP.NET_Core_REST_API.DTOs;
using ASP.NET_Core_REST_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ASP.NET_Core_REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDTO>> Register(RegisterDTO dto)
        {
            var response = await _authService.RegisterAsync(dto);
            return CreatedAtAction(nameof(Register), response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Register(LoginDTO dto)
        {
            var response = await _authService.LoginAsync(dto);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            
            var firstName = User.FindFirst(ClaimTypes.GivenName)?.Value;
            
            return Ok(new { userId, email, firstName });
        }
    }
}