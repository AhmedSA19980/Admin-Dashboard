using Admin.Core.DTOs.token;
using Admin.Core.DTOs.users;
using Admin.Core.interfaces.refresh_token;
using Admin.Core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Data
{
    public class RefreshTokenRep : IRefreshToken<RefreshToken>
    {
        private readonly AppDbContext _context;

        public RefreshTokenRep(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> AddAsync(RefreshToken enRefreshToken)
        {
            await _context.RefreshTokens.AddAsync(enRefreshToken);
            await _context.SaveChangesAsync();
            return enRefreshToken;
        }

        public async Task<RefreshToken> GetRefreshTokenByTokenAsync(string refreshToken)
        {
            return await _context.RefreshTokens.FindAsync(refreshToken);
        }

      

        public async Task<bool> RefreshTokenAsync(ReqRefreshTokenDTO token)
        {
            var refreshToken = new RefreshToken { 
                Id = token.Id,
                Revoked = DateTime.UtcNow,
                RevokedByIP = token.RevokedByIp,
                ReplaceByToken = token.ReplacedByToken,
                ReasonRevoked = token.ReasonRevoked,
               
 
            };

            _context.RefreshTokens.Attach(refreshToken);
            _context.Entry(refreshToken).Property(t => t.Revoked).IsModified = true;
            _context.Entry(refreshToken).Property(t => t.RevokedByIP).IsModified = true;
            _context.Entry(refreshToken).Property(t => t.ReplaceByToken).IsModified = true;
           _context.Entry(refreshToken).Property(t => t.ReasonRevoked).IsModified = true;       


            return await _context.SaveChangesAsync() > 0 ;

        }

        public  async Task<bool> RevokeAllRefreshTokensForUserAsync(RevokeAllRefreshTokensForUserDTO token)
        {
            var userToken = new RefreshToken { 
                UserId = token.userId,
                RevokedByIP = token.revokedByIp,
                Revoked = DateTime.UtcNow,
                ReasonRevoked = token?.ReasonRevoked
                    
            };

            _context.RefreshTokens.Attach(userToken);

            _context.Entry(userToken).Property(t => t.Revoked).IsModified = true;
            _context.Entry(userToken).Property(t => t.RevokedByIP).IsModified =true;
            _context.Entry(userToken).Property(t => t.ReasonRevoked).IsModified=true;


            return await _context.SaveChangesAsync() >  0;   
        }
    }
}
