using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.DTO.DayOffDTO;
using AttendanceManagement.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace AttendanceManagement.WebAPI.Controllers
{
    [Authorize(Roles = "User")]
    public class DayOffUserController : CustomControllersUserBase
    {
        private readonly IDayOffService _dayOffService;
        public DayOffUserController(IDayOffService dayOffService)
        {
            _dayOffService = dayOffService;
        }
        [HttpPost]
        public async Task<ActionResult<DayOffAddResponseDTO>> AddDayOff(DayOffAddDTO dayOffAddDTO)
        {
            DateTime currentDate = DateTime.Today;
            int result = currentDate.CompareTo(dayOffAddDTO.Date);
            if (result >= 0)
            {
                return BadRequest(new {error = "The date you select must be after the current date"});
            }
            var dayOff = await _dayOffService.AddDayOff(dayOffAddDTO);
            if (dayOff == null)
            {
                return BadRequest(new {error = "You have sent this day off request to your manager"});
            }
            return Ok(dayOff.ToDayOffAddResponseDTO());
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DayOffAddResponseDTO>>> GetAllDayOffUser()
        {
            var dayOffs = await _dayOffService.GetAllDayOffUser();
            return Ok(dayOffs.Select(d => d.ToDayOffAddResponseDTO()).ToList());
        }
        [HttpGet("{date}")]
        public async Task<ActionResult<DayOffAddResponseDTO>> GetDayOffByDate(string date)
        {
            DateTime dateValue;
            if (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
            {
                // Nếu không thành công, trả về BadRequest với thông báo lỗi
                return BadRequest(new {error = "Invalid date format. Date must be in 'yyyy-MM-dd' format."});
            }
            var dayOff = await _dayOffService.GetDayOffByDate(dateValue, Guid.Empty);
            if (dayOff == null)
            {
                return Ok();
            }
            return Ok(dayOff.ToDayOffAddResponseDTO());
        }
        [HttpPut("{date}")]
        public async Task<ActionResult<DayOffAddResponseDTO>> UpdateDayOff(string date, DayOffUpdateUserDTO dayOffUpdateUserDTO)
        {
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
                return BadRequest(new { error = "You can only apporve requests after the current date" });
            }

            DayOffUpdateDTO dayOffUpdate = new DayOffUpdateDTO()
            {
                date = dateValue,
                reason = dayOffUpdateUserDTO.reason
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
