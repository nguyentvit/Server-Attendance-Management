using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.DTO.DepartmentDTO;
using AttendanceManagement.Core.Identity;
using System;

namespace AttendanceManagement.Core.ServiceContracts
{
    public interface IDepartmentService
    {
        Task<List<DepartmentResponseDTO>> GetAllDepartments();
        Task<DepartmentResponseDTO?> GetDepartment(Guid Id);
        Task<DepartmentResponseDTO> AddDepartment(DepartmentAddDTO Department);
        Task<DepartmentResponseDTO?> UpdateDepartment(Department Department);
        Task<DepartmentResponseDTO?> DeleteDepartment(Guid Id);
    }
}
