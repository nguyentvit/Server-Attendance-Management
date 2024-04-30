using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.DTO;
using System;

namespace AttendanceManagement.Core.ServiceContracts
{
    public interface IDepartmentService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<DepartmentResponse>> GetAllDepartments();
        Task<DepartmentResponse?> GetDepartment(Guid Id);
        Task<DepartmentResponse> AddDepartment(Department Department);
        Task<DepartmentResponse?> UpdateDepartment(Department Department);
        Task<DepartmentResponse?> DeleteDepartment(Guid Id);
    }
}
