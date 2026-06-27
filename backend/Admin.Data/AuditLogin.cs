

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

        public async Task<List<AuditLogs>> GetAuditLogsAsync()
        {
            return await _context.AuditLogs.ToListAsync();
        }

        public async Task<List<AuditLogs>> getAuditLogsBetweenSpecificDateAsync(DateTime stDate, DateTime endate)
        {
            var specificDateLogs = await _context.AuditLogs.Where(al => al.LoggedDate == stDate && al.LoggedDate <= endate).ToListAsync();
            return  specificDateLogs;

        }
    }
}
  