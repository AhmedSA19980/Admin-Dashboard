using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.interfaces.role
{
    public interface IRoleRepository<T> : IReadRepository <T> where T : class
    {

    }
}
