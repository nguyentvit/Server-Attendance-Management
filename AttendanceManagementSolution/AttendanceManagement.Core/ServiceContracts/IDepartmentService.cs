using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.DTO;
using AttendanceManagement.Core.Identity;
using System;

namespace AttendanceManagement.Core.ServiceContracts
{
    public interface IDepartmentService
    {
        Task<List<DepartmentResponse>> GetAllDepartments();
        Task<DepartmentResponse?> GetDepartment(Guid Id);
        Task<DepartmentResponse> AddDepartment(Department Department);
        Task<DepartmentResponse?> UpdateDepartment(Department Department);
        Task<DepartmentResponse?> DeleteDepartment(Guid Id);
    }
}
