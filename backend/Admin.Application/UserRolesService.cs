using Admin.Application.Mappers;
using Admin.Core.DTOs.UserRoles;
using Admin.Core.DTOs.users;
using Admin.Core.interfaces.user;
using Admin.Core.interfaces.UserRoles;
using Admin.Core.models;
using BlobStorage.Core.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Application
{
    public  class UserRolesService
    {

        private readonly IUserRolesRepository<UserRole> _userRolesService;


        public UserRolesService(IUserRolesRepository<UserRole> userRolesService) {
        
            _userRolesService = userRolesService;        
        }

        public async Task<int> AddUserRoleAsync(AddUserRole userRoleDto)
        {

            bool isRoleExist = await _userRolesService.IsRoleExist(userRoleDto.RoleId , userRoleDto.UserId);
            if (isRoleExist)
            {

                throw new Exception("User already has this role.");
            }

            var newRole = new UserRole
            {
                UserId = userRoleDto.UserId,
                RoleId = userRoleDto.RoleId,
               
            };

            await _userRolesService.AddAsync(newRole);

            return newRole.Id;


        }


        public async Task<UserRoleDto> GetUserRoleById(int id)
        {

            var user = await _userRolesService.GetByIdAsync(id);

            return UserRoleMapper.ToDto(user);

        }

    }
}
