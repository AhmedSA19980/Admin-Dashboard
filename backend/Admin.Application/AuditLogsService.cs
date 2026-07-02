using Admin.Core.DTOs.audit_login;
using Admin.Core.DTOs.users;
using Admin.Core.Global;
using Admin.Core.interfaces.audit_login;
using Admin.Core.interfaces.user;
using Admin.Core.models;
using Admin.Application.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Admin.Application
{
    public   class AuditLogsService
    {

        private readonly IAuditLogin<AuditLogs> _auditLogsRepository;
        private readonly IConfiguration _configuration;

        public AuditLogsService(IAuditLogin<AuditLogs> auditLogsRepository, IConfiguration configuration)
        {
            _auditLogsRepository = auditLogsRepository;
            _configuration = configuration;
        }

        private async Task<int> AddLogAsync(auditLogin loginRecord)
        {
            var newLog = new AuditLogs 
            {
                UserId = loginRecord.UserId,
                Email_Username = loginRecord.Email_Username,
                LoggedDate = DateTime.UtcNow,
                LogginStatus = loginRecord.LogginStatus,
                IpAddress = loginRecord.IpAddress
            };

            await _auditLogsRepository.AddAsync(newLog);

            return newLog.Id ;
        }

      

        public  async Task<bool> AuditLogAsync(auditLogin auditRecord)
        {
          return await AddLogAsync(auditRecord) > 0 ? true : false;
        } 

        public async Task<auditLogin> GetLoginRecordByIdAsync(int id)
        {
            var loginRecord = await _auditLogsRepository.GetByIdAsync(id);


            return AuditLogsMapper.ToDto(loginRecord);
        }

       

        public async Task<List<auditLogin>> GetLogsAsync()
        {
            var listLogs = await _auditLogsRepository.GetAuditLogsAsync();
            return  AuditLogsMapper.ToDtoList(listLogs);
        }

        public async Task<List<auditLogin>> GetLogsBetweenSpecificDateAsync(DateTime stDate , DateTime enDate)
        {
            var listLogs = await _auditLogsRepository.getAuditLogsBetweenSpecificDateAsync(stDate , enDate);
            return AuditLogsMapper.ToDtoList(listLogs);
        }


    }
}
