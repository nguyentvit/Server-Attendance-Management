using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.DTO;
using AttendanceManagement.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace AttendanceManagement.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AttendancesController : CustomControllersBase
    {
        private readonly IAttendanceService _attendanceService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attendanceService"></param>
        public AttendancesController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceResponse>>> GetAllAttendance()
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
        public async Task<ActionResult<AttendanceResponse>> GetAttendance(Guid attendanceId)
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
        /// <param name="attendanceDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<AttendanceResponse>> AddAttendance(AttendanceDTO attendanceDTO)
        {
            Attendance attendance = new Attendance()
            {
                Time = DateTime.UtcNow.ToLocalTime(),
                Status = attendanceDTO.Status,
                UserId = attendanceDTO.UserId
            };
            var attendanceAdder = await _attendanceService.AddAttendance(attendance);
            return CreatedAtAction("GetAttendance", new { attendanceId = attendanceAdder.AttendanceId }, attendanceAdder);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<AttendanceResponse>>> GetHeHe(string date)
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
