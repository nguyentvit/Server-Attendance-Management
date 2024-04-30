using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.Domain.RepositoryContracts;
using AttendanceManagement.Core.DTO;
using AttendanceManagement.Core.ServiceContracts;
using System;

namespace AttendanceManagement.Core.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentsRepository _departmentsRepository;
        public DepartmentService(IDepartmentsRepository departmentsRepository)
        {
            _departmentsRepository = departmentsRepository;
        }

        public async Task<List<DepartmentResponse>> GetAllDepartments()
        {
            List<Department> departments = await _departmentsRepository.GetAllDepartments();
            return departments.Select(department => department.ToDepartmentResponse()).ToList();
        }

        public async Task<DepartmentResponse?> GetDepartment(Guid Id)
        {
            Department? department = await _departmentsRepository.GetDepartment(Id);
            if (department == null)
            {
                return null;
            }
            return department.ToDepartmentResponse();
        }

        public async Task<DepartmentResponse> AddDepartment(Department department)
        {
            await _departmentsRepository.AddDepartment(department);
            return department.ToDepartmentResponse();
        }

        public async Task<DepartmentResponse?> UpdateDepartment(Department Department)
        {
            var result = await _departmentsRepository.UpdateDepartment(Department);
            if (result == null) 
            {
                return null;
            }
            return result.ToDepartmentResponse();
        }

        public async Task<DepartmentResponse?> DeleteDepartment(Guid Id)
        {
            var result = await _departmentsRepository.DeleteDepartment(Id);
            if (result == null)
            {
                return null;
            }
            return result.ToDepartmentResponse();
        }
    }
}
