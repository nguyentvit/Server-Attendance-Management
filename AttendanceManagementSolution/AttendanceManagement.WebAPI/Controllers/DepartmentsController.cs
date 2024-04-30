using AttendanceManagement.Core.DTO;
using AttendanceManagement.Core.Enums;
using AttendanceManagement.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class DepartmentsController : CustomControllersBase
    {
        private readonly IDepartmentService _departmentsService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentsService"></param>
        public DepartmentsController(IDepartmentService departmentsService)
        {
            _departmentsService = departmentsService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentResponse>>> GetAllDepartments()
        {
            var departments = await _departmentsService.GetAllDepartments();
            return Ok(departments);
        }
    }
}
