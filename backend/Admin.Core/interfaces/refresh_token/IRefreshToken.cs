using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.interfaces.refresh_token
{
    public  interface IRefreshToken<T> : IWriteRepository<T> where T : class
    {
    }
}
