using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.interfaces.UserRoles
{
    public interface IUserRolesRepository<T> : IWriteRepository<T>, IReadRepository<T> where T : class
    {

        Task<bool> IsRoleExist(int roleId , int userId);
    }
}
