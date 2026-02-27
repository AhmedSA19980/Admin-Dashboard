using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.interfaces.audit_login
{
    public interface IAuditLogin<T> : IWriteRepository<T> where T : class
    {

    }
}
