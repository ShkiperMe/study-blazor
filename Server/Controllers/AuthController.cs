using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Fridge.Server.Services.AuthService;
using Fridge.Shared;

namespace Fridge.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetUsers()
        {
            var result = await _authService.GetUsersAsync();
            return Ok(result);
        }

        [HttpGet("{userId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<User>>> GetUser(int userId)
        {
            var result = await _authService.GetUserAsync(userId);
            return Ok(result);
        }

        [HttpPost("register"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegister request)
        {
            var response = await _authService.Register(
                new User
                {
                    Email = request.Email
                },
                request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin request)
        {
            var response = await _authService.Login(request.Email, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("change-password"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword(UserChangePassword request)
        {
            var response = await _authService.ChangePassword(request.Id, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
