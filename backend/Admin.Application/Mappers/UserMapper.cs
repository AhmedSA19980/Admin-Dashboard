using Admin.Core.DTOs.users;
using Admin.Core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Application.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                Email = user.Email,
                Roles = user.UserRoles.Select(r => r.Role.Name).ToList(),
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                JoinedAt = user.CreatedAt
            };

        }

      
    }


   
}
