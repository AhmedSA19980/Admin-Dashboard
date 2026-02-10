using Admin.Core.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.models
{
    public class User
    {

        [Key]
        public int Id { get; set; }
        [Required, MinLength(5)]
        public string First_Name { get; set; }
        [Required, MinLength(5)]
        public string Last_Name { get; set; }

        [Required, MinLength(5)]
        public string UserName { get; set; }
     
        [Required, MaxLength(256)]
        public string Email { get; set; }


       
        [Required]
        public string Password { get; set; }

        public bool isActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsUserDeleted { get; set; }
        public ICollection<userRole> UserRoles { get; set; } = new List<userRole>();
        public ICollection<RefreshToken> RefreshToken { get; set; } = new List<RefreshToken>();

    }
}
