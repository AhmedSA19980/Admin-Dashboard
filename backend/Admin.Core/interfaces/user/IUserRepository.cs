using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.interfaces.user
{
    public interface IUserRepository<T>   : IReadRepository<T>, IWriteRepository<T>, IUpdateRepository<T>  where T : class
    {
        Task<bool> ChangePasswordAsync(int userId , string newPassword ) ;
        Task<List<T>> ListAllUserAsync();
        Task<T> FindUserByUsernameAsync( string userName );
        Task<T> FindUserByEmailAsync(string email);

    }
}
