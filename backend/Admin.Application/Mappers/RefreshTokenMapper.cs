using Admin.Core.DTOs.token;
using Admin.Core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Application.Mappers
{
    public static class RefreshTokenMapper
    {

        public static RefreshToken ToEntity (this RefreshTokenDetailsDto refreshDto)
        {

            return new RefreshToken
            {  //* check for all properties must be filled
                UserId = refreshDto.UserId,
                RefreshTok = refreshDto.RefreshTok,
                ExpiresAt = refreshDto.ExpiresAt,
                RevokedByIP = refreshDto.RevokedByIP,


            };
        }
        public static RefreshTokenDetailsDto ToDto(this RefreshToken refreshToken)
        {
            return new RefreshTokenDetailsDto
            {
                RefreshTok = refreshToken.RefreshTok,
                CreatedAt = refreshToken.CreatedAt,
                ExpiresAt = refreshToken.ExpiresAt,
                ReasonRevoked = refreshToken.ReasonRevoked,
                RevokedByIP = refreshToken.RevokedByIP,
                ReplaceByToken = refreshToken.ReplaceByToken,

            };
        }

    }
}
