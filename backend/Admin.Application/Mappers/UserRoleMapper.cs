using Admin.Core.DTOs.UserRoles;
using Admin.Core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Application.Mappers
{
    public static  class UserRoleMapper
    {

        public static  UserRoleDto ToDto(this UserRole UserRole)
        {
            return new UserRoleDto
            {
                Id = UserRole.Id,
                RoleId = UserRole.Id,
                UserId = UserRole.UserId,
                RoleName = UserRole.Role.Name,
                UserName = UserRole.User.UserName,   
            };

        }
    }
}
