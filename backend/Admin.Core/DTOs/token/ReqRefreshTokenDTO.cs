using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.DTOs.token
{
    public class ReqRefreshTokenDTO
    {
        public int Id { get; set; }
        public DateTime Revoked { get; set; } 
        public string RevokedByIp { get; set; }
        public string ReplacedByToken { get; set; }
        public string ReasonRevoked { get; set; }
     
    }
}
