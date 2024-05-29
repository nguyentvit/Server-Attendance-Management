using AttendanceManagement.Core.DTO.DayOffDTO;
using AttendanceManagement.Core.Enums;
using AttendanceManagement.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace AttendanceManagement.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DayOffAdminController : CustomControllersAdminBase
    {
        private readonly IDayOffService _dayOffService;
        public DayOffAdminController(IDayOffService dayOffService)
        {
            _dayOffService = dayOffService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DayOffResponseAdminDTO>>> GetAllDayOffs()
        {
            var dayOffs = await _dayOffService.GetAllDayOff();
            return Ok(dayOffs.Select(d => d.ToDayOffResponseAdminDTO()).ToList());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<DayOffResponseAdminDTO>>> GetDayOffsByDate(string date)
        {
            DateTime dateValue;
            if (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
            {
                // Nếu không thành công, trả về BadRequest với thông báo lỗi
                return BadRequest(new { error = "Invalid date format. Date must be in 'yyyy-MM-dd' format." });
            }
            var dayOffs = await _dayOffService.GetAllDayOffByDate(dateValue);
            return Ok(dayOffs.Select(d => d.ToDayOffResponseAdminDTO()).ToList());
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<DayOffResponseAdminDTO>>> GetDayOffsByUser(Guid userId)
        {
            var dayOffs = await _dayOffService.GetAllDayOffByUserId(userId);
            return Ok(dayOffs.Select(d => d.ToDayOffResponseAdminDTO()).ToList());
        }
        [HttpGet("{date}/{userId}")]
        public async Task<ActionResult<DayOffAddResponseDTO>> GetDayOffByDateAndUserId(string date, Guid userId)
        {
            DateTime dateValue;
            if (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
            {
                // Nếu không thành công, trả về BadRequest với thông báo lỗi
                return BadRequest(new { error = "Invalid date format. Date must be in 'yyyy-MM-dd' format." });
            }
            var dayOff = await _dayOffService.GetDayOffByDate(dateValue, userId);
            if (dayOff == null)
            {
                return Ok();
            }
            return Ok(dayOff.ToDayOffAddResponseDTO());
        }
        [HttpPut(("{date}/{userId}"))]
        public async Task<ActionResult<DayOffAddResponseDTO>> UpdateDayOff(string date, Guid userId, DayOffUpdateAdminDTO dayOffUpdateAdminDTO)
        {
            string status = dayOffUpdateAdminDTO.status;
            DateTime dateValue;
            if (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
            {
                // Nếu không thành công, trả về BadRequest với thông báo lỗi
                return BadRequest(new { error = "Invalid date format. Date must be in 'yyyy-MM-dd' format." });
            }
            DateTime currentTime = DateTime.Now;
            int compared = currentTime.CompareTo(dateValue);
            if (compared >= 0)
            {
                return BadRequest(new {error = "You can only apporve requests after the current date"});
            }

            if (!Enum.IsDefined(typeof(DayOffOptions), status))
            {
                return BadRequest(new { error = "Status can only on the list {'Waiting', 'Allow', 'Disallow'}" });
            }
            DayOffUpdateDTO dayOffUpdate = new DayOffUpdateDTO()
            {
                date = dateValue,
                userId = userId,
                status = status,
            };

            var dayOffUpdated = await _dayOffService.UpdateDayOff(dayOffUpdate);
            if (dayOffUpdated == null)
            {
                return BadRequest();
            }
            return Ok(dayOffUpdated.ToDayOffAddResponseDTO());
        }
    }
}
