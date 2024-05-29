using AttendanceManagement.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Core.Domain.RepositoryContracts
{
    public interface ISalaryPaysRepository
    {
        Task<List<SalaryPay>> GetSalaryPays();
        Task<List<SalaryPay>> GetSalaryPayByUserId(Guid userId);
        Task<List<SalaryPay>> GetSalaryPayByTime(DateTime time);
        Task<SalaryPay?> GetSalaryPayByUserIdAndTime(Guid userId, DateTime time);
        Task<SalaryPay?> AddSalaryPay(SalaryPay salaryPay);
        Task<SalaryPay> UpdateSalaryPay(SalaryPay salaryPay);

    }
}
