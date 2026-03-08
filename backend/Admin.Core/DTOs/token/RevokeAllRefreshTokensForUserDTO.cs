using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.DTOs.token
{
    public class RevokeAllRefreshTokensForUserDTO
    {
      public int userId { set; get; }
      public string revokedByIp { set; get; }
        public DateTime Revoked { get; set; } 
        public string? ReasonRevoked {  set; get; }


    }
}
