using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.DTOs.Login
{
    public class LoginDto
    {

        [Required]
        public string Login { get; set; } // username or email
        [Required]
        public string Password { get; set; }


    }
}
