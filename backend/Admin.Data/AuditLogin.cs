

using Admin.Core.interfaces.audit_login;
using Admin.Core.models;
using Microsoft.EntityFrameworkCore;

namespace Admin.Data
{
    public class AuditLoginRep : IAuditLogin<AuditLogs>
    {
        private readonly AppDbContext _context;

        public AuditLoginRep(AppDbContext context)
        {
            _context = context;
        }
        public async Task<AuditLogs> AddAsync(AuditLogs enAuditLog)
        {
            await _context.AuditLogs.AddAsync(enAuditLog);
            await _context.SaveChangesAsync();
            return enAuditLog;
        }

        public Task<AuditLogs> GetByIdAsync(int Id)
        {
            return _context.AuditLogs.FirstOrDefaultAsync(al => al.Id == Id);
        }

    }

}
  