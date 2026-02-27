
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Admin.Core.models
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


        public DateTime CreatedAt { get; set; }

        public bool IsUserDeleted { get; set; } // 0 =>  false , 1 => true
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<RefreshToken> RefreshToken { get; set; } = new HashSet<RefreshToken>();

    }
}
