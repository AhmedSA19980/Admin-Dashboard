using Admin.Application;
using Admin.Core.DTOs.UserRoles;
using Admin.Core.DTOs.users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly UserRolesService _userRolesService;

        public UserRolesController(UserRolesService userRolesService)
        {
            _userRolesService = userRolesService;
        }


        [HttpPost("addUserRole", Name = "addUserRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> addUserRole(AddUserRole userRole)
        {
            if (userRole == null || int.IsNegative(userRole.UserId) || userRole.UserId == 0 )
            {
                return BadRequest($"All field are required");
            }


            int newUserRoles = await _userRolesService.AddUserRoleAsync(userRole);

            return CreatedAtRoute(
                     "AddUserRole",
                     new { Id = newUserRoles } 

            );
        }


        [HttpGet("GetUserRoleById", Name = "GetUserRoleById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserRoleDto>> GetUserRoleById(int Id)
        {

            if (Id < 0)
            {
                return BadRequest($"Id cannot accept negative numeric");
            }

            UserRoleDto user = await _userRolesService.GetUserRoleById(Id);

            if (user == null)
            {
                return NotFound($"user roles data is null.");
            }
            return Ok(user);

        }
    }
}
