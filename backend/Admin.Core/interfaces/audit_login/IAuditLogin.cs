using Admin.Core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Admin.Core.interfaces.audit_login
{
    public interface IAuditLogin<T> : IWriteRepository<T> , IReadRepository<T> where T : class
    {
        public  Task<List<T>> GetAuditLogsAsync();
        public Task<List<T>> getAuditLogsBetweenSpecificDateAsync(DateTime stDate, DateTime endate);
    }
}
