using BusinessObjects.Dtos.Users;
using Helper;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace ProductManagementAPI.Controllers
{
    [ApiController]
    [Route("api/2024-09-21/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginModel)
        {
            var token = await _authService.Authenticate(loginModel.Username, loginModel.Password);

            if (token == null)
                return Unauthorized(new ResponseModel<string>
                {
                    Success = false,
                    Error = "Invalid username or password",
                    ErrorCode = 401
                });

            return Ok(new ResponseModel<string>
            {
                Success = true,
                Data = token
            });
        }
    }
}
