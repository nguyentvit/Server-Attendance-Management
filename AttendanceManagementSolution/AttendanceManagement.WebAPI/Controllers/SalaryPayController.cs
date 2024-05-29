using AttendanceManagement.Core.DTO.SalaryPayDTO;
using AttendanceManagement.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace AttendanceManagement.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SalaryPayController : CustomControllersAdminBase
    {
        private readonly ISalaryPayService _salaryPayService;
        public SalaryPayController(ISalaryPayService salaryPayService)
        {
            _salaryPayService = salaryPayService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaryPayResponseDTO>>> GetSalaryPays()
        {
            var salaryPays = await _salaryPayService.GetSalaryPays();
            return Ok(salaryPays);
        }
        [HttpGet("{date}")]
        public async Task<ActionResult<IEnumerable<SalaryPayResponseDTO>>> GetSalaryPaysByDate(string date)
        {
            DateTime dateValue;
            if (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
            {
                // Nếu không thành công, trả về BadRequest với thông báo lỗi
                return BadRequest(new { error = "Invalid date format. Date must be in 'yyyy-MM-dd' format." });
            }
            var salaryPays = await _salaryPayService.GetSalaryPaysByDate(dateValue);
            return Ok(salaryPays);
        }
    }
}
