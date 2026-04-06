using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.interfaces.refresh_token
{
    public  interface IRefreshToken<T> : IWriteRepository<T>  where T : class
    {

        Task<T> GetRefreshTokenByTokenAsync(string refreshToken);
        Task<bool> RefreshTokenAsync (int id, string revokedByIp, string replacedByToken, string reasonRevoked);
        Task<bool> RevokeAllRefreshTokensForUserAsync(int userId, string revokedByIp, string? reasonRevoked);
    }
}
