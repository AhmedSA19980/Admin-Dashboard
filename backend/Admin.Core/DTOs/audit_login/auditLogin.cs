using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.DTOs.audit_login
{
    public class auditLogin
    {

        public int Id { get; set; } 
        public string Email_Username { get; set; }
        public int Role { get; set; }
        public DateTime LoggedDate { get; set; } //default current date

        public bool LogginStatus { get; set; } // logedin or loggedout  0=> loggedout 1=>loggedin

        public string RefreshToken { get; set; }



    }
}
