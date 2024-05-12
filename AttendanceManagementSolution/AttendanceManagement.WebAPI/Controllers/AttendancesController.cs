using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.DTO.AttendanceDTO;
using AttendanceManagement.Core.ServiceContracts;
using AttendanceManagement.WebAPI.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Globalization;

namespace AttendanceManagement.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AttendancesController : CustomControllersAdminBase
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IHubContext<AttendanceHub> _hubContext;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attendanceService"></param>
        public AttendancesController(IAttendanceService attendanceService, IHubContext<AttendanceHub> hubContext)
        {
            _attendanceService = attendanceService;
            _hubContext = hubContext;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceResponseDTO>>> GetAllAttendance()
        {
            var attendances = await _attendanceService.GetAllAttendances();
            return Ok(attendances);
        }
        /// <summary>
        /// return departmentName, List users, List shift
        /// </summary>
        /// <param name="attendanceId"></param>
        /// <returns></returns>
        [HttpGet("{attendanceId}")]
        public async Task<ActionResult<AttendanceResponseDTO>> GetAttendance(Guid attendanceId)
        {
            var attendance = await _attendanceService.GetAttendance(attendanceId);
            if (attendance == null)
            {
                return NotFound();
            }
            return Ok(attendance);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attendanceAddDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<AttendanceResponseDTO>> AddAttendance(AttendanceAddDTO attendanceAddDTO)
        {
            var attendanceAdder = await _attendanceService.AddAttendance(attendanceAddDTO);
            await _hubContext.Clients.All.SendAsync("ReceiveAttendance", new 
            { 
                UserId = attendanceAdder.UserId,
                Time = attendanceAdder.Time,
                Status = (attendanceAdder.Status) ? "In" : "Out"
            });

            return CreatedAtAction("GetAttendance", new { attendanceId = attendanceAdder.AttendanceId }, attendanceAdder);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<AttendanceResponseDTO>>> GetHeHe(string date)
        {
            DateTime dateTime;
            if (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                return BadRequest();
            }

            var attendances = await _attendanceService.GetAttendancesByDate(dateTime);
            return Ok(attendances);
        }
            
    }
}
