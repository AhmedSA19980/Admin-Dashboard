using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.interfaces.user
{
    public interface IUserRepository<T>   : IReadRepository<T>, IWriteRepository<T>, IUpdateRepository<T>  where T : class
    {
        Task<bool> ChangePassword(int userId , string newPassword ) ;
        Task<List<T>> ListAllUser();
        Task<T> FindUserByUsername( string userName );
        Task<T> FindUserByEmail(string email);

    }
}
