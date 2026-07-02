using Admin.Application;
using Admin.Core.DTOs.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.RateLimiting;
using Admin.Core.models;
using Admin.Core.DTOs.token;
using Admin.Core.DTOs.login;

namespace Admin.API.Controllers
{
    [Route("api/[controller]")]
    [EnableRateLimiting("AuthLimiter")]
    [ApiController]
    public class AuthController : ControllerBase
    {

       
        private readonly AuthService _authService;
        private readonly UserService _userService;

        public AuthController(AuthService authService)
        {
          
            _authService = authService; 
        }

        [HttpPost("logout")]
        public async Task< IActionResult> Logout([FromBody] LogoutRequest request)
        {
            var User = await _userService.GetUserByEmail(request.Email);
            if (User == null)
            {
                if (User == null)
                    return Unauthorized("Invalid refresh request");
            }

            var refreshValidToken = BCrypt.Net.BCrypt.Verify(request.RefreshToken, User.RefreshToken);
            if (!refreshValidToken)
            {
                return Unauthorized("Invalid refresh request");
            }
            if (User == null)
                return Ok(); // Do not reveal if user exists

            bool refreshValid = BCrypt.Net.BCrypt.Verify(request.RefreshToken, User.RefreshToken);
            if (!refreshValid)
                return Ok();

            var IpAdd = GetIpAddress();
            string userId = User.Id != -1 ? Convert.ToString(User.Id) : "0"; 
            var logout = await _authService.RevokeRefreshTokenAsync(User.RefreshToken ,userId, IpAdd);
          
            return Ok("Logged out successfully");
        }

        [HttpPost("Login", Name = "Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {

            if (string.IsNullOrEmpty(loginDto.Login) && string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest($"UserName/Email or Password is not correct !");
            }

        

            try
            {

                var IpAdd = GetIpAddress();
                var auth = await _authService.AuthenticateAysnc(loginDto, IpAdd);
                if (auth == null)
                {
                    return Unauthorized("Invalid Credentials");
                }

                return Ok(auth);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please try again later.");
            }

        }

        [HttpPost("refresh-token", Name = "refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody] RefresTokenRequestDTO Request )
        {
           var User = await _userService.GetUserByEmail(Request.Email);
            if (User == null)
            {
                if (User == null)
                    return Unauthorized("Invalid refresh request");
            }

            var refreshValidToken = BCrypt.Net.BCrypt.Verify(Request.RefreshToken , User.RefreshToken);
            if (!refreshValidToken)
            {
                return Unauthorized("Invalid refresh request");
            }

            var IpAdd = GetIpAddress();
            var refreshToken = await _authService.RefreshAccessTokenAsync(Request.RefreshToken, IpAdd);
            if (refreshToken == null)
            {
                return BadRequest("Invalid or expired refresh token.");
            }

            return Ok(refreshToken);

        }


        [Authorize] // Requires a valid access token
        [HttpPost("revoke-token", Name = "Revoke-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> RevokeToken([FromBody] string  RefreshToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)// If not authorized to revoke
            {
                return Unauthorized("User ID not found in token.");
            }

           
            var IpAdd = GetIpAddress();
            var auth = await _authService.RevokeRefreshTokenAsync(RefreshToken, userId, IpAdd);
            if (!auth)
            {

                var refresToken = await _authService.FindAsync(RefreshToken);
                if (refresToken != null && Convert.ToString(refresToken.User) != userId)
                {
                    return Forbid("You are not authorized to revoke this token.");
                }

                return BadRequest("Invalid or already revoked refresh token, or not found.");
            }

            return Ok("Refresh token revoked successfully.");
        }


        private string GetIpAddress()
        {
            // Get IP address of the client
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "N/A";
        }
    }
}
