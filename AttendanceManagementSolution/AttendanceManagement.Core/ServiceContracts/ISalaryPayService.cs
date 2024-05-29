using AttendanceManagement.Core.Domain.Entities;
using AttendanceManagement.Core.DTO.SalaryPayDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.ServiceContracts
{
    public interface ISalaryPayService
    {
        Task<List<SalaryPayResponseDTO>> GetSalaryPays();
        Task<List<SalaryPayResponseDTO>> GetSalaryPaysByDate(DateTime date);
        Task<List<SalaryPayResponseDTO>> GetSalaryPayByUserId(Guid userId);
        Task<SalaryPayResponseDTO?> GetSalaryPayByUserIdAndTime(Guid userId, DateTime time);
        Task<SalaryPayResponseDTO?> AddSalaryPay(SalaryPay salaryPay);
        Task<SalaryPayResponseDTO> UpdateSalaryPay(SalaryPay salaryPay);
    }
}
