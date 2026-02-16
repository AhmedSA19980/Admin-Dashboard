using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace  Admin.Core.models
{
    public  class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<userRole> UserRoles { get; set; } = new HashSet<userRole>();
    }
}
