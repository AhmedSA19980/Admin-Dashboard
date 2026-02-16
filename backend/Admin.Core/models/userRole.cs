
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




namespace Admin.Core.models
{
    public  class userRole
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }    

        public User User { get; set; }


        [Required]
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        
        public Role Role { get; set; }


    }
}
