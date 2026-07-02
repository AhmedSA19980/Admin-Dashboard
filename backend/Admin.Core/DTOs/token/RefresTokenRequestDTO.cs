using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.DTOs.token
{
    public class RefresTokenRequestDTO
    {
       
        public string RefreshToken { get; set; }

        public string Email { get; set; }

    }
}
