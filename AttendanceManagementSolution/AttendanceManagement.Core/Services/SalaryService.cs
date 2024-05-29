using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.Domain.RepositoryContracts;
using AttendanceManagement.Core.DTO.SalaryDTO;
using AttendanceManagement.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly ISalariesRepository _salariesRepository;
        public SalaryService(ISalariesRepository salariesRepository)
        {
            _salariesRepository = salariesRepository;
        }

        public async Task<List<SalaryResponseDTO>> GetSalaries()
        {
            var Salaries = await _salariesRepository.GetSalaries();
            return Salaries.Select(s => s.ToSalaryResponseDTO()).ToList();
        }

        public async Task<SalaryResponseDTO> UpdateSalary(Salary salaryUpdate)
        {
            var salary = await _salariesRepository.UpdateSalary(salaryUpdate);
            return salary.ToSalaryResponseDTO();
        }
    }
}
