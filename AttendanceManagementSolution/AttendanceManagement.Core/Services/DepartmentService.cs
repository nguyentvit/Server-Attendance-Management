using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.Domain.RepositoryContracts;
using AttendanceManagement.Core.DTO.DepartmentDTO;
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

        public async Task<List<DepartmentResponseDTO>> GetAllDepartments()
        {
            List<Department> departments = await _departmentsRepository.GetAllDepartments();

            return departments.Select(department => department.ToDepartmentResponse()).ToList();
        }

        public async Task<DepartmentResponseDTO?> GetDepartment(Guid Id)
        {
            Department? department = await _departmentsRepository.GetDepartment(Id);
            if (department == null)
            {
                return null;
            }
            return department.ToDepartmentResponse();
        }

        public async Task<DepartmentResponseDTO> AddDepartment(DepartmentAddDTO departmentAddDTO)
        {
            Department department = new Department()
            {
                DepartmentName = departmentAddDTO.DepartmentName
            };
            department = await _departmentsRepository.AddDepartment(department);
            return department.ToDepartmentResponse();
        }

        public async Task<DepartmentResponseDTO?> UpdateDepartment(Department department)
        {
            var result = await _departmentsRepository.UpdateDepartment(department);
            if (result == null) 
            {
                return null;
            }
            return result.ToDepartmentResponse();
        }

        public async Task<DepartmentResponseDTO?> DeleteDepartment(Guid Id)
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
