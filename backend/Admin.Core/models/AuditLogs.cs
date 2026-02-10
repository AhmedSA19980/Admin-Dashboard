using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.models
{
    public class AuditLogs
    {

        [Key]
        public int Id { get; set; }


        public int UserId { get; set; }
     
        public string Email_Username { get; set; }
        public int Role { get; set; }

        [Required , MaxLength(45)]
        public string IpAddress {  get; set; }

        public DateTime LoggedDate { get; set; } = DateTime.UtcNow;  //default current date



        public bool LogginStatus { get; set; } // logedin or loggedout  0=> loggedout 1=>loggedin

        [Required]
        public string RefreshToken { get; set; }

    }
}
