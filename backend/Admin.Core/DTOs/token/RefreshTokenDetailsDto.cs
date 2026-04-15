using Admin.Core.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.DTOs.token
{
    public  class RefreshTokenDetailsDto
    {
       
        public int Id { get; set; }

        public int UserId { get; set; }

        public string RefreshTok { get; set; } = null!;
        public DateTime CreatedAt { get;  set; }

        public DateTime ExpiresAt { get; set; }

        public DateTime? Revoked { get; set; }
        public string? RevokedByIP { get; set; }
        public string? ReplaceByToken { get; set; }

        public string? ReasonRevoked { get; set; }
        public bool IsExpired { get;  }
        public bool IsRevoked { get;  }
        public bool IsActive { get; }


    }
}
