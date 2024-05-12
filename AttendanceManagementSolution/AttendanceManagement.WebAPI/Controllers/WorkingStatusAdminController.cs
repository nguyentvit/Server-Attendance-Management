using AttendanceManagement.Core.DTO.WorkingStatusDTO;
using AttendanceManagement.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WorkingStatusAdminController : CustomControllersAdminBase
    {
        private readonly IWorkingStatusService _workingStatusService;
        public WorkingStatusAdminController(IWorkingStatusService workingStatusService)
        {
            _workingStatusService = workingStatusService;
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<WorkingStatusReponseDTO>> GetWorkingStatusUser(Guid userId)
        {
            DateTime date = DateTime.UtcNow.ToLocalTime();
            var result = await _workingStatusService.WorkingStatusUser(userId, date);
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkingStatusReponseDTO>>> GetWorkingStatusAllUsers()
        {
            DateTime date = DateTime.UtcNow.ToLocalTime();
            var result = await _workingStatusService.WorkingStatusAllUsers(date);
            return Ok(result);
        }
    }
}
