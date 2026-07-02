using Admin.Application;
using Admin.Core.DTOs.users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Admin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserService _userService;
        public UserController(UserService userService) { 
        
            _userService = userService; 
        }

        [HttpGet("getUserByEmail", Name = "getUserByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {

            if (string.IsNullOrEmpty(email))
            {
                return BadRequest($"Empty email Field is not accepted");
            }

            UserDto user = await _userService.GetUserByEmail(email);

            if (user == null)
            {
                return NotFound($"user data is null.");
            }
            return Ok(user);

        }

        [HttpPost("addUser", Name = "addUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> AddUser(AddUserDto user)
        {
            if (user== null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.UserName) ||
                string.IsNullOrEmpty(user.First_Name) || string.IsNullOrEmpty(user.Last_Name) )
            {
                return BadRequest($"All field are required");
            }


            int newUser = await _userService.AddUserAsync(user);

            return CreatedAtRoute(
                     "AddUser",
                     new { Id = newUser } // used for Location header
                    
                 );

        }

        [HttpPatch("updateUser", Name = "updateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> UpdateUser(UpdateUserDto user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.UserName) ||
                string.IsNullOrEmpty(user.First_Name) || string.IsNullOrEmpty(user.Last_Name))
            {
                return BadRequest($"All field are required");
            }


            bool newUser = await _userService.UpdateUserAsync(user);

            return newUser == true ? Ok($"{user.Id} user is Update"): Ok($"{user.Id} user is Failed");

        }

        [HttpPatch("changePassword", Name = "changePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Change(ChangePassDto password)
        {
            if (password == null || string.IsNullOrEmpty(password.Password) || password.ID < 0 
               )
            {
                return BadRequest($"All fields are required / Non negative numeric is accpeted for Id");
            }


            bool newUser = await _userService.ChangePasswordAsync(password);

            return newUser == true ? Ok($"{password.ID} user password is Update") : Ok($"{password.ID} user password is Failed to update");

        }

        

        [HttpGet("getUserByUsername", Name = "getUserByUsername")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetUserByusername(string username)
        {

            if (string.IsNullOrEmpty(username))
            {
                return BadRequest($"Empty username Field is not accepted");
            }

            UserDto user = await _userService.GetUserByUserName(username);

            if (user == null)
            {
                return NotFound($"user data is null.");
            }
            return Ok(user);

        }

        [HttpGet("getUserByUserId", Name = "getUserByUserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetUserByusername(int userId)
        {

            if (userId < 0)
            {
                return BadRequest($"Id cannot accept negative numeric");
            }

            UserDto user = await _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound($"user data is null.");
            }
            return Ok(user);

        }



        [HttpGet("allUser", Name = "allUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserDto>>> AllUser()
        {
            List<UserDto> users = await _userService.AllUsers();

            if (users == null)
            {
                return NotFound($"users data are null.");
            }
            return Ok(users);

        }

        [HttpGet("userRoles", Name = "userRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<string>>> userRoles(int userInt)
        {
            List<string> userRoles = await _userService.GetUserRoles(userInt);

            if (userRoles == null)
            {
                return NotFound($"users data are null.");
            }
            return Ok(userRoles);
        }

    }
}
