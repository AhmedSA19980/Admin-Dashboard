using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.models
{
    public class RefreshToken
    {

        [Key]
        public int Id { get; set; }
        
       

        [Required]
        public int UserId { get; set; }


        [Required]
        public string RefreshTok { get; set; } = null!;
        public DateTime CreatedAt { get; private set; }

        public DateTime ExpiredAt { get; set; }

        public DateTime? Revoked {  get; set; } // allow null
        public string? RevokedByIP { get; set; } // same
        public string? ReplaceByToken { get; set; } //same

        public string? ReasonRevoked { get; set; }  //same

        public User User { get; set; } = null!;
    }
}
