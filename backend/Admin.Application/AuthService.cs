using Admin.Core.DTOs.Login;
using Admin.Core.interfaces.refresh_token;
using Admin.Core.interfaces.user;
using Admin.Core.interfaces.UserRoles;
using Admin.Core.models;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Admin.Core.DTOs.auth;
using Admin.Core.DTOs.token;
using Admin.Core.Global;
using System.Security.Cryptography;
using Admin.Application.Mappers;
using Admin.Core.DTOs.users;
using Admin.Core.DTOs.audit_login;
using System.Net;
namespace Admin.Application
{
    public  class AuthService
    {

        private readonly IRefreshToken<RefreshToken> _refreshTokenRepository;
       private readonly IConfiguration _configuration;
        private readonly IUserRepository<User> _userRepository;

        private readonly AuditLogsService _auditLogsService;

        public AuthService(IRefreshToken<RefreshToken> refreshTokenService, IConfiguration configuration )
        {

            _refreshTokenRepository = refreshTokenService;
            _configuration = configuration;
        }


        private async Task<List<Claim>> GetUserClaims(string userId, string userName, List<int> Role)
        {
            var roles = new List<string> { "User", Convert.ToString(Role) };


            var claims = new List<Claim>
            {

                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString( userId)),
                    new Claim(ClaimTypes.Name, userName)
            };
          

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); // Add roles as claims
            }


            return claims;
        }
        private string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var accessExpirationMinutes = _configuration.GetValue<int>("Jwt:AccessTokenExpirationMinutes");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(accessExpirationMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<RefreshToken> FindAsync(string refreshToken)
        {
            return await _refreshTokenRepository.GetRefreshTokenByTokenAsync(refreshToken);
        }


        private RefreshTokenDetailsDto GenerateRefreshToken()
        {
            var refreshTokenExpirationDays = _configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays");

            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return new RefreshTokenDetailsDto
            {

                RefreshTok = Convert.ToBase64String(bytes),
                ExpiresAt = DateTime.UtcNow.AddDays(refreshTokenExpirationDays),
 

            };
        }
       
        private List<int> GetUserRoleIds(User user)
        {
            List<int> userRoles = user?.UserRoles?.Select(x => x.Id).ToList() ?? new List<int>();
            return userRoles;

        }

       
        public async Task<RefreshTokenResponseDto> AuthenticateAysnc(LoginDto loginDto, string IpAddress)
        {
            try
            {

                string hashPass =Hash.hashPassword(loginDto.Password);

               
                loginDto.Password = hashPass;
                var  user =await _userRepository.FindUserByUserNameAndPasswordAsync(loginDto); 
                if (user == null)
                {
                    return null;
                }

                string clientId = Convert.ToString(user.Id);
                string username = user.UserName;

                List <int> userRoles = GetUserRoleIds(user);
                var claims = await GetUserClaims(Convert.ToString(clientId), username,userRoles);


                var accessToken = GenerateAccessToken(claims);
                var refreshToken = GenerateRefreshToken();

                refreshToken.RefreshTok = Hash.hashPassword(refreshToken.RefreshTok); // bcrypt token
                refreshToken.UserId = Convert.ToInt32(clientId); // Assign the actual user ID
                refreshToken.RevokedByIP = IpAddress;

                var token = RefreshTokenMapper.ToEntity(refreshToken);
               
                await _refreshTokenRepository.AddAsync(token);

                /*********dto just tranform and reshape  data, fill dto with data and sending them is fine ********/
                var auditLogin = new auditLogin
                {
                    UserId = refreshToken.UserId,
                    Email_Username = username,
                    IpAddress = IpAddress,
                    LogginStatus = true,
                 
                };

               await _auditLogsService.AuditLogAsync(auditLogin);



                return new RefreshTokenResponseDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken.RefreshTok,
                    ExpiresIn = _configuration.GetValue<int>("Jwt:AccessTokenExpirationMinutes") * 60
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            };

        }


        public async Task<RefreshTokenResponseDto> RefreshAccessTokenAsync(string oldRefreshTokenString, string ipAddress)
        {

            var existingRefreshToken = await _refreshTokenRepository.GetRefreshTokenByTokenAsync(oldRefreshTokenString);

            if (existingRefreshToken == null || existingRefreshToken.IsRevoked || existingRefreshToken.IsExpired)
            {
                if (existingRefreshToken != null)
                {
                    existingRefreshToken.ReasonRevoked = "Attempted use of invalid/revoked refresh token.";
                    string reasonRevoked = existingRefreshToken.ReasonRevoked;
                    await _refreshTokenRepository.RevokeAllRefreshTokensForUserAsync(existingRefreshToken.UserId, Convert.ToString(existingRefreshToken.UserId) ,reasonRevoked);
                   
                }
                return null;
            }


            existingRefreshToken.Revoked = DateTime.UtcNow;
            existingRefreshToken.RevokedByIP = ipAddress;
            existingRefreshToken.ReasonRevoked = "used for refresh";

            var client =await _userRepository.GetByIdAsync(existingRefreshToken.UserId);


            List<int> userRoles = GetUserRoleIds(existingRefreshToken.User);
            string userIdinstr = Convert.ToString(existingRefreshToken.UserId);
            var claims = await GetUserClaims(userIdinstr, client.UserName, userRoles);
            var newAccessToken = GenerateAccessToken(claims);
            var newRefreshToken = GenerateRefreshToken();


            newRefreshToken.UserId = existingRefreshToken.UserId;
         //   newRefreshToken.CreatedByIp = ipAddress; after adding create by ip property to models and dts :  remove the following comment 

            existingRefreshToken.ReplaceByToken = newRefreshToken.RefreshTok; // Link old to new


            await _refreshTokenRepository.RefreshTokenAsync(existingRefreshToken.Id , existingRefreshToken.RevokedByIP ,newAccessToken ,existingRefreshToken.ReasonRevoked);

            var token = RefreshTokenMapper.ToEntity(newRefreshToken);

            await _refreshTokenRepository.AddAsync(token);


            return new RefreshTokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.RefreshTok,
                ExpiresIn = _configuration.GetValue<int>("Jwt:AccessTokenExpirationMinutes") * 60
            };


        }

        public async Task<bool> RevokeRefreshTokenAsync(string refreshTokenString, string clientIdFromAccessToken, string IpAddress)
        {
            var token = await _refreshTokenRepository.GetRefreshTokenByTokenAsync(refreshTokenString) ; 
            if (token == null || token.IsRevoked || token.IsExpired)
            {
                return false;
            }
            if (Convert.ToString(token.UserId) != clientIdFromAccessToken)
            {
                return false;
            }


            token.Revoked = DateTime.UtcNow;
            token.RevokedByIP = IpAddress;
            token.ReasonRevoked = "Manual Revocation by User";



            await _refreshTokenRepository.RefreshTokenAsync(token.Id,token.RevokedByIP , token.ReplaceByToken ,token.ReasonRevoked); 
            /*********dto just tranform and reshape  data, fill dto with data and sending them is fine ********/
            var auditLogin = new auditLogin
            {
                UserId = token.UserId,
                Email_Username =token.User.UserName,
                IpAddress = IpAddress,
                LogginStatus = false,
            };

            await _auditLogsService.AuditLogAsync(auditLogin);


            return true;




        }




    }

}

