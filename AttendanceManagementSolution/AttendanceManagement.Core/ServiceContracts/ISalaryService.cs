using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.DTO.SalaryDTO;
using System;

namespace AttendanceManagement.Core.ServiceContracts
{
    public interface ISalaryService
    {
        Task<List<SalaryResponseDTO>> GetSalaries();
        Task<SalaryResponseDTO> UpdateSalary(Salary salaryUpdate);
    }
}
