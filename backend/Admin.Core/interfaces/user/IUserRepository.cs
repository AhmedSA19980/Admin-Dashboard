using Admin.Core.DTOs.Login;
using Admin.Core.DTOs.users;
using Admin.Core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.interfaces.user
{
    public interface IUserRepository<T>   : IReadRepository<T>, IWriteRepository<T>, IUpdateRepository<T>  where T : class
    {
        Task<bool> ChangePasswordAsync(int userId , string password ) ;
        Task<List<T>> ListAllUserAsync();
        Task<T> FindUserByUsernameAsync( string userName );
        Task<T> FindUserByEmailAsync(string email);
        Task<T> FindUserByUserNameAndPasswordAsync(LoginDto login);
        Task<List<string>> GetUserRolesById(int userId);
        Task<bool> IsEmailTakenAsync(string Email);
        Task<bool> IsUserNameTakenAsync(string Email);
    }
}
