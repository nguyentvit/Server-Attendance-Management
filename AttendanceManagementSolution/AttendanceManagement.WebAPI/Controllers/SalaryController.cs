using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.DTO.SalaryDTO;
using AttendanceManagement.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SalaryController : CustomControllersAdminBase
    {
        private readonly ISalaryService _salaryService;
        public SalaryController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaryResponseDTO>>> GetSalaries()
        {
            return await _salaryService.GetSalaries();
        }
        [HttpPut("{salaryId}")]
        public async Task<ActionResult<SalaryResponseDTO>> UpdateSalary(Guid salaryId ,SalaryUpdateDTO salaryUpdate)
        {
            Salary salary = new Salary()
            {
                SalaryId = salaryId,
                SalaryPerHour = salaryUpdate.SalaryPerHour
            };
            var salaryUpdated = await _salaryService.UpdateSalary(salary);
            return Ok(salaryUpdated);
        }
    }
}
