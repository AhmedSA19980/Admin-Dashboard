using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Core.DTOs.token;

namespace  Admin.Core.interfaces.refresh_token
{
    public  interface IRefreshToken<T> : IWriteRepository<T>  where T : class
    {

        Task<T> GetRefreshTokenByTokenAsync(string refreshToken);
        Task<bool> RefreshTokenAsync (ReqRefreshTokenDTO Token);
        Task<bool> RevokeAllRefreshTokensForUserAsync(RevokeAllRefreshTokensForUserDTO Token);
    }
}
