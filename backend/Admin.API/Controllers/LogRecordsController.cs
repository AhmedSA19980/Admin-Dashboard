using Admin.Application;
using Admin.Core.DTOs.audit_login;
using Admin.Core.DTOs.UserRoles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogRecordsController : ControllerBase
    {


        private readonly AuditLogsService _auditLogsService;
        public LogRecordsController(AuditLogsService auditLogsService) { 
            
            _auditLogsService = auditLogsService;   
        
        }

        [HttpGet("GetLoginRecordById", Name = "GetLoginRecordById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<auditLogin>> GetLoginRecordByIdAsync(int Id)
        {

            if (Id < 0)
            {
                return BadRequest($"Id cannot accept negative numeric");
            }

            
            auditLogin logRecord = await _auditLogsService.GetLoginRecordByIdAsync(Id);

            if (logRecord == null)
            {
                return NotFound($"log record data is not found.");
            }
            return Ok(logRecord);

        }


        [HttpGet("GetLogs", Name = "GetLogs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<auditLogin>>> GetLogsAsync()
        {

            List<auditLogin> logRecords = await _auditLogsService.GetLogsAsync();

            if (logRecords == null)
            {
                return NotFound($"log records data are null.");
            }
            return Ok(logRecords);

        }


        [HttpGet("GetLogsRecordsBetweenSpecificDate", Name = "GetLogsRecordsBetweenSpecificDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<auditLogin>>> GetLogsRecordsBetweenSpecificDateAsync(DateTime stDate , DateTime endDate)
        {

            List<auditLogin> logRecords = await _auditLogsService.GetLogsBetweenSpecificDateAsync(stDate ,  endDate);

            if (logRecords == null)
            {
                return NotFound($"log records data are null.");
            }
            return Ok(logRecords);

        }
    }
}
