using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.DTOs.Login
{
    public class Login
    {

        [Required]
        public string Email_Username { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
