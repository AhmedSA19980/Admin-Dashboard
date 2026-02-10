using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.DTOs.users
{
    public class updateUser
    {
        int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }



        [Required]
        [MinLength(6)]
        public string password { get; set; }

       

    }
}
