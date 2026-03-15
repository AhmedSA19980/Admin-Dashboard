using Admin.Core.DTOs.users;
using Admin.Core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Application.Mappers
{
    public static class UsersMapper
    {
        public static List<UserDto> ToDtoList(this IEnumerable<User> users)
        {
            return users.Select(u=> u.ToDto()).ToList();    
        }
    }
}
