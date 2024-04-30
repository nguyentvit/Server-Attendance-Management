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
    }
}
