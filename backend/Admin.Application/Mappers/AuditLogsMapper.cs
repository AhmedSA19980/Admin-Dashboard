using Admin.Core.DTOs.audit_login;
using Admin.Core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Application.Mappers
{
    public static class AuditLogsMapper
    {

        public static AuditLogs ToEntity(this auditLogin auditLogs)
        {
            return new AuditLogs
            {
                UserId = auditLogs.UserId,
                Email_Username = auditLogs.Email_Username,
                IpAddress = auditLogs.IpAddress,
                LoggedDate = auditLogs.LoggedDate,
                LogginStatus = auditLogs.LogginStatus,
                RefreshToken = auditLogs.RefreshToken
            };
        }
        public static  auditLogin  ToDto(this AuditLogs auditLogs)
        {
            return new auditLogin
            {
                UserId = auditLogs.UserId,
                Email_Username = auditLogs.Email_Username,
                IpAddress = auditLogs.IpAddress,
                LoggedDate = auditLogs.LoggedDate,
                LogginStatus = auditLogs.LogginStatus,
                RefreshToken = auditLogs.RefreshToken
            };
        }

        public static List<auditLogin> ToDtoList(this IEnumerable<AuditLogs> logs) { 
            return logs.Select(log => log.ToDto()).ToList();
        }
    }
}
