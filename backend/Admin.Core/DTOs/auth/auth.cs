using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.DTOs.auth
{
    public class auth
    {
        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}
