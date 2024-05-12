using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.DTO.DepartmentDTO;
using AttendanceManagement.Core.Enums;
using AttendanceManagement.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace AttendanceManagement.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class DepartmentsController : CustomControllersAdminBase
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
        public async Task<ActionResult<IEnumerable<DepartmentResponseDTO>>> GetAllDepartments()
        {
            var departments = await _departmentsService.GetAllDepartments();
            return Ok(departments);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpGet("{departmentId}")]
        public async Task<ActionResult<DepartmentResponseDTO>> GetDepartment(Guid departmentId)
        {
            var department = await _departmentsService.GetDepartment(departmentId);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<DepartmentResponseDTO>> AddDepartment(DepartmentAddDTO departmentAddDTO)
        {
            var departmentAdder = await _departmentsService.AddDepartment(departmentAddDTO);
            return CreatedAtAction("GetDepartment", new {departmentId = departmentAdder.DepartmentId}, departmentAdder);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="departmentUpdate"></param>
        /// <returns></returns>
        [HttpPut("{departmentId}")]
        public async Task<ActionResult<DepartmentResponseDTO>> UpdateDepartment(Guid departmentId, [Bind(nameof(Department.DepartmentName))] DepartmentUpdateDTO departmentUpdate)
        {
            Department department = new Department()
            {
                DepartmentId = departmentId,
                DepartmentName = departmentUpdate.DepartmentName
            };

            var result = await _departmentsService.UpdateDepartment(department);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpDelete("{departmentId}")]
        public async Task<ActionResult<DepartmentResponseDTO>> DeleteDeparment(Guid departmentId)
        {
            var result = await _departmentsService.DeleteDepartment(departmentId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
