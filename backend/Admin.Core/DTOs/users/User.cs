using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.DTOs.users
{
    public class User
    {
        int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string UserName { get; set; }
       
        public string Email { get; set; }


       
        public string password { get; set; }

        public int Role {  get; set; }



    }
}
